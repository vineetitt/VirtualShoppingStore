using System.ComponentModel.DataAnnotations;
using VirtualShoppingStore.Models.DTO.OrderItemDto;

namespace VirtualShoppingStore.Models.DTO.OrderDto
{

    /// <summary>
    /// AddOrderDto class
    /// </summary>

    public class AddOrderDto
    {
        /// <summary>
        /// UserId
        /// </summary>

        [Required]

        public int UserId { get; set; }

        /// <summary>
        /// OrderDate
        /// </summary>

        [Required]

        public DateTime OrderDate { get; set; }

        /// <summary>
        /// Orderitems
        /// </summary>

        [Required]
        public List<AddOrderItemDto> Orderitems { get; set; } 

    }

}
