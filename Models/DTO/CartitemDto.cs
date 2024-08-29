namespace VirtualShoppingStore.Models.DTO
{
    public class CartitemDto
    {
        public int CartItemId { get; set; }

        public int? UserId { get; set; }

        public int? ProductId { get; set; }

        public int Quantity { get; set; }

        public DateTime? CreatedAt { get; set; }

        public decimal? TotalAmount { get; set; }

        public bool? IsPlaced { get; set; }

    }
}
