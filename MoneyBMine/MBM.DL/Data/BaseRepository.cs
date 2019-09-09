namespace MBM.DL.Data
{
    /// <summary>
    /// Base Class repository to be used.
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class BaseRepository<TModel> : IBaseRepository
    {
        /// <summary>
        /// Gets or sets the Table Name to be used
        /// </summary>
        public string TableName { get; set; }
    }
}