namespace MBM.DL.Models
{
    /// <summary>
    /// Data class to be used bu Models
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public class DataObject<TKey>
    {
        /// <summary>
        /// Gets or Sets the Id
        /// </summary>
        public TKey Id { get; set; }
    }
}