namespace VirtualShoppingStore.Models.DTO.OrderItemDto
{
    /// <summary>
    /// 
    /// </summary>
    public class PatchOrderItemDto
    {
         /// <summary>
        /// 
        /// </summary>
        public int ?Quantity { get; set; }


        /// <summary>
        /// 
        /// </summary>
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
