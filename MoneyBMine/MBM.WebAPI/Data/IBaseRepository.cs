namespace MBM.WebAPI.Data
{
    /// <summary>
    /// Interface for the base repository
    /// </summary>
    public interface IBaseRepository
    {
        /// <summary>
        /// Gets or sets the table name for the repository to use.
        /// </summary>
        string TableName { get; set; }
    }
}