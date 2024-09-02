namespace VirtualShoppingStore.Models.DTO.CategoryDto
{
    /// <summary>
    /// 
    /// </summary>
    internal class ResponseCategoryDto
    {
        /// <summary>
        /// 
        /// </summary>
        public int CategoryId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string CategoryName { get; set; } = null!;
        /// <summary>
        /// 
        /// </summary>
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
