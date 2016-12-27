using System;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using WebDev.Data.Base;


namespace WebDev.Data.SqlCe.Initializers
{
    /// <summary>
    /// A base class for initializing databases.
    /// </summary>
    /// <typeparam name="T">The concrete DbContext to use.</typeparam>
    internal abstract class SqlCeInitializer<T> : IDatabaseInitializer<T> where T : DbContext
    {
        public abstract void InitializeDatabase(T context);

        #region Seeding methods

        /// <summary>
        /// A method that should be overridden to actually add data to the context for seeding. 
        /// The default implementation does nothing.
        /// </summary>
        /// <param name="context">The context to seed.</param>
        protected virtual void Seed(T context)
        {
            ISeedDatabase seeder = context as ISeedDatabase;
            if (seeder != null)
            {
                seeder.Seed();
            }
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Returns a new DbContext with the same SqlCe connection string, but with the |DataDirectory| expanded
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        protected static DbContext ReplaceSqlCeConnection(DbContext context)
        {
            if (context.Database.Connection is SqlConnection)
            {
                SqlConnectionStringBuilder builder =
                    new SqlConnectionStringBuilder(context.Database.Connection.ConnectionString);
                if (!String.IsNullOrWhiteSpace(builder.DataSource))
                {
                    builder.DataSource = ReplaceDataDirectory(builder.DataSource);
                    return new DbContext(builder.ConnectionString);
                }
            }
            return context;
        }

        private static string ReplaceDataDirectory(string inputString)
        {
            string str = inputString.Trim();
            if (string.IsNullOrEmpty(inputString) ||
                !inputString.StartsWith("|DataDirectory|", StringComparison.InvariantCultureIgnoreCase))
            {
                return str;
            }
            string data = AppDomain.CurrentDomain.GetData("DataDirectory") as string;
            if (string.IsNullOrEmpty(data))
            {
                data = AppDomain.CurrentDomain.BaseDirectory ?? Environment.CurrentDirectory;
            }
            if (string.IsNullOrEmpty(data))
            {
                data = string.Empty;
            }
            int length = "|DataDirectory|".Length;
            if ((inputString.Length > "|DataDirectory|".Length) && ('\\' == inputString["|DataDirectory|".Length]))
            {
                length++;
            }
            return Path.Combine(data, inputString.Substring(length));
        }

        #endregion
    }
}
