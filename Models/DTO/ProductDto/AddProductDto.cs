using System.ComponentModel.DataAnnotations;

namespace VirtualShoppingStore.Models.DTO.ProductDto
{
    /// <summary>
    /// class AddProductDto
    /// </summary>
    public class AddProductDto
    {
        /// <summary>
        /// ProductName
        /// </summary>

        [Required(ErrorMessage = "Product name is required")]
        [StringLength(100, ErrorMessage = "Product name can't be longer than 100 characters")]
        public string ProductName { get; set; } = null!;

        /// <summary>
        /// Description
        /// </summary>

        [StringLength(10000, ErrorMessage = "Description can't be longer than 10000 characters")]
        public string? Description { get; set; }

        /// <summary>
        /// Price
        /// </summary>

        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
        public decimal Price { get; set; }

        /// <summary>
        /// StockQuantity
        /// </summary>

        [Required(ErrorMessage = "Stock quantity is required")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock quantity must be non-negative")]
        public int StockQuantity { get; set; }

        /// <summary>
        /// CategoryId
        /// </summary>

        public int? CategoryId { get; set; }
    }
}
