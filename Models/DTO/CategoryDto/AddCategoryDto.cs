using System.ComponentModel.DataAnnotations;

namespace VirtualShoppingStore.Models.DTO.CategoryDto
{
    /// <summary>
    /// 
    /// </summary>
    public class AddCategoryDto
    {
        /// <summary>
        /// 
        /// </summary>
        
        public int CategoryId { get; set; }
        /// <summary>
        /// 
        /// </summary>

        [StringLength(100, ErrorMessage = "Category Name cannot exceed 100 characters.")]
        [MinLength(1, ErrorMessage = "Category Name must have at least 1 character.")]
        public string CategoryName { get; set; } = null!;
    }
}
