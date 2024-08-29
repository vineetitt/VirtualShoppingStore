namespace VirtualShoppingStore.Models.DTO
{
    public class OrderDto
    {
        public int OrderId { get; set; }

        public int? UserId { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public int? StatusId { get; set; }
      
    }
}
