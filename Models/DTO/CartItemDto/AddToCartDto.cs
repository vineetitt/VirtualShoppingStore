namespace VirtualShoppingStore.Models.DTO.CartItemDto
{

    /// <summary>
    /// 
    /// </summary>
    public class AddToCartDto
    {
        /// <summary>
        /// 
        /// </summary>
        
        public int? UserId { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public int? ProductId { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public int Quantity { get; set; } = 1;
    }
}
