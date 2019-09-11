namespace MBM.WPFExternal.Data
{
    /// <summary>
    /// Interface for the base repositories.
    /// </summary>
    public interface IBaseRepository
    {
        /// <summary>
        /// Gets or sets the table name for the repository to use.
        /// </summary>
        string TableName { get; set; }
    }
}