namespace VirtualShoppingStore.Models.DTO.ProductDto
{
    /// <summary>
    /// Product Dto
    /// </summary>
    public class ResponseProductDto
    {
        /// <summary>
        /// 
        /// </summary>
        public int ProductId { get; set; }



        /// <summary>
        /// 
        /// </summary>
        public string ProductName { get; set; } = null!;



        /// <summary>
        /// 
        /// </summary>
        public string? Description { get; set; }



        /// <summary>
        /// 
        /// </summary>
        public decimal Price { get; set; }



        /// <summary>
        /// 
        /// </summary>
        public int StockQuantity { get; set; }



        /// <summary>
        /// 
        /// </summary>
        public int? CategoryId { get; set; }



        /// <summary>
        /// 
        /// </summary>
        public DateTime? CreatedAt { get; set; }


        public string? ImageUrl { get; set; }
        /// <summary>
        /// 
        /// </summary>
       // public DateTime? UpdatedAt { get; set; }



        /// <summary>
        /// 
        /// </summary>
        public bool? IsDeleted { get; set; }
    }
}
