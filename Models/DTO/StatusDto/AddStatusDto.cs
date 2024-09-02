namespace VirtualShoppingStore.Models.DTO.StatusDto
{
    /// <summary>
    /// 
    /// </summary>
    public class AddStatusDto
    {
        /// <summary>
        /// 
        /// </summary>
        public int StatusId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string StatusName { get; set; } = null!;

        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
