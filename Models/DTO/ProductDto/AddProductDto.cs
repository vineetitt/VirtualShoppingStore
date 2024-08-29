using System.ComponentModel.DataAnnotations;

namespace VirtualShoppingStore.Models.DTO.ProductDto
{
    /// <summary>
    /// 
    /// </summary>
    public class AddProductDto
    {
        /// <summary>
        /// 
        /// </summary>
        [Required(ErrorMessage = "Product name is required")]
        [StringLength(100, ErrorMessage = "Product name can't be longer than 100 characters")]
        public string ProductName { get; set; } = null!;

        /// <summary>
        /// Description of the product
        /// </summary>
        [StringLength(10000, ErrorMessage = "Description can't be longer than 10000 characters")]
        public string? Description { get; set; }

        /// <summary>
        /// Price of the product
        /// </summary>
        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        /// <summary>
        /// Quantity of the product in stock
        /// </summary>
        [Required(ErrorMessage = "Stock quantity is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity must be a non-negative number")]
        public int StockQuantity { get; set; }

        /// <summary>
        /// ID of the category the product belongs to
        /// </summary>
        public int? CategoryId { get; set; }


        /// <summary>
        /// Flag indicating if the product is deleted
        /// </summary>
        public bool? IsDeleted { get; set; }

    }
}
