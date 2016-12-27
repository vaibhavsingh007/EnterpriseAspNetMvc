using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebDev.Utils.ExceptionHandlers
{
    /// <summary>
    /// This class is responsible to log error into error log file. To log any error, use ExceptionFacade class
    /// </summary>
    public static class ErrorLogHelper
    {

        #region -- Private --


        #region -- Methods --

        private static string GetFileName()
        {
            string RetVal;

            RetVal = ErrorLogHelper.FileNamePrefix + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString();

            return RetVal;
        }

        /// <summary>
        /// Inserts the error in to error log file
        /// </summary>
        /// <param name="filePath">File path with directory</param>
        /// <param name="errorMessage">Error String</param>
        private static void WriteErrorInLogFile(string filePath, string errorMessage, string errorDescription)
        {
            StreamWriter Writer = null;
            try
            {
                Writer = new StreamWriter(filePath, true);
                errorMessage = DateTime.Now.ToString() + " >> " + errorMessage;
                Writer.WriteLine(errorMessage);

                errorDescription += System.Environment.NewLine;
                Writer.WriteLine(errorDescription);
                Writer.Flush();

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (Writer != null)
                {
                    Writer.Dispose();
                }
            }
        }

        #endregion

        #endregion

        #region -- internal --

        #region -- Variables --

        internal static string FileNamePrefix = string.Empty;

        #endregion

        #region -- Methods --

        /// <summary>
        /// Writes error text into a Log file under the specified path
        /// </summary>
        /// <param name="message">Error String</param>
        /// <param name="filePath">for relative directry Path, use Server.MapPath() method to get a path</param>
        internal static void WriteLog(string message, string filePath)
        {
            try
            {
                string directoryPath = filePath.Remove(filePath.LastIndexOf('\\'));
                if (!Directory.Exists(directoryPath))
                {
                    Directory.CreateDirectory(directoryPath);
                }
                try
                {
                    //If file exists then append the text , otherwise create a file and insert the text
                    if (!File.Exists(filePath))
                    {
                        File.WriteAllText(filePath, " ");
                    }
                }
                catch (Exception)
                {
                    // Throw for now. It's fatal.
                    throw;
                }

                ErrorLogHelper.WriteErrorInLogFile(filePath, message, String.Empty);

            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void WriteLog(string errorMessage, string directryPath, string errorDescription = "")
        {
            string FileName = string.Empty;
            string FilePath = string.Empty;
            try
            {
                // create directory if not exists
                try
                {
                    if (!Directory.Exists(directryPath))
                    {
                        Directory.CreateDirectory(directryPath);
                    }
                }
                catch (Exception)
                {
                    throw;
                }

                // Make file Name
                FileName = ErrorLogHelper.GetFileName();

                // Make a FilePath
                FilePath = Path.Combine(directryPath, FileName) + ".txt";

                try
                {
                    //If file exists then append the text , otherwise create a file and insert the text
                    if (!File.Exists(FilePath))
                    {
                        File.WriteAllText(FilePath, " ");
                    }
                }
                catch (Exception)
                {
                }

                ErrorLogHelper.WriteErrorInLogFile(FilePath, errorMessage, errorDescription);

            }
            catch (Exception)
            {

            }
        }

        #endregion

        #endregion
    }
}
