namespace VirtualShoppingStore.Models.DTO.ProductDto
{




    /// <summary>
    /// 
    /// </summary>
    public class PatchProductDto
    {
        /// <summary>
        /// 
        /// </summary>
        public string? ProductName { get; set; }
        


        /// <summary>
        /// 
        /// </summary>
        public string? Description { get; set; }





        /// <summary>
        /// 
        /// </summary>
        public decimal? Price { get; set; }




        /// <summary>
        /// 
        /// </summary>
        public int? StockQuantity { get; set; }




        /// <summary>
        /// 
        /// </summary>
        public int? CategoryId { get; set; }




        /// <summary>
        /// 
        /// </summary>
        public bool? IsDeleted { get; set; } 
    }
}
