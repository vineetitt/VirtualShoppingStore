using System.ComponentModel.DataAnnotations;

namespace VirtualShoppingStore.Models.DTO.CartItemDto
{

    /// <summary>
    /// 
    /// </summary>
    public class UpdateCartDto
    {
        /// <summary>
        /// Quantity
        /// </summary>

        [Range(1, 200, ErrorMessage = "Quantity cannot be either negative or equals to 0")]

        public int Quantity { get; set; }

    }
}
