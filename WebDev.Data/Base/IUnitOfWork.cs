namespace WebDev.Data.Base
{
    /// <summary>
    /// Encapsulates a unit of work
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Saves changes to all objects that have changed within the unit of work.
        /// </summary>
        void SaveChanges();
    }
}
