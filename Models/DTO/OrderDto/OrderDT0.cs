using VirtualShoppingStore.Models.DTO.OrderItemDto;
using VirtualShoppingStore.Models.DTO.StatusDto;
namespace VirtualShoppingStore.Models.DTO.OrderDto
{
    public class OrderDT0
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public StatusDTO Status { get; set; }
        public List<OrderItemDTO> OrderItems { get; set; }
    }
}
