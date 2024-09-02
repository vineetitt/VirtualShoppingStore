using System.ComponentModel.DataAnnotations;
using VirtualShoppingStore.Models.DTO.OrderItemDto;

namespace VirtualShoppingStore.Models.DTO.OrderDto
{




    /// <summary>
    /// 
    /// </summary>
    public class AddOrderDto
    {
        /// <summary>
        /// 
        /// </summary>

        [Required]
        public int UserId { get; set; }


        /// <summary>
        /// 
        /// </summary>
        [Required]
        public DateTime OrderDate { get; set; }




        /// <summary>
        /// 
        /// </summary>
       // [Required]
        //public decimal TotalAmount { get; set; }



        /// <summary>
        /// 
        /// </summary>
        [Required]
        public List<AddOrderItemDto> Orderitems { get; set; }
    }
}
