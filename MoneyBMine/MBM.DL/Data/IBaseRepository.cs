namespace MBM.DL.Data
{
    /// <summary>
    /// Base Repository Interface
    /// </summary>
    public interface IBaseRepository
    {
        /// <summary>
        /// Gets or sets the Table Name to be used.
        /// </summary>
        string TableName { get; set; }
    }
}