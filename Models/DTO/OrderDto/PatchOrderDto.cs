using VirtualShoppingStore.Models.DTO.OrderItemDto;
namespace VirtualShoppingStore.Models.DTO.OrderDto
{
    /// <summary>
    /// 
    /// </summary>
    public class UpdateOrderDto
    {
        /// <summary>
        /// 
        /// </summary>
        public DateTime? OrderDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int? StatusId { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<UpdateOrderItemDto> Orderitems { get; set; }
    }
}
