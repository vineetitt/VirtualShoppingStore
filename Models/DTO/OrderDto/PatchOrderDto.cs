using VirtualShoppingStore.Models.DTO.OrderItemDto;
namespace VirtualShoppingStore.Models.DTO.OrderDto
{
    /// <summary>
    /// 
    /// </summary>
    public class PatchOrderDto
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
        public List<PatchOrderItemDto> Orderitems { get; set; }
    }
}
