using System.ComponentModel.DataAnnotations;

namespace VirtualShoppingStore.Models.DTO.OrderItemDto
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateOrderItemDto
    {
        /// <summary>
        /// 
        /// </summary>

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]
        public int ?Quantity { get; set; }


        /// <summary>
        /// 
        /// </summary>
        
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal ?Price { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public int? ProductID { get; set; }



        /// <summary>
        /// 
        /// </summary>
        public int OrderItemId { get; set; }
    }
}
