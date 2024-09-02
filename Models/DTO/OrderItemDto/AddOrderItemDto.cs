namespace VirtualShoppingStore.Models.DTO.OrderItemDto
{

    /// <summary>
    /// 
    /// </summary>
    public class AddOrderItemDto
    {

        /// <summary>
        /// 
        /// </summary>
        public int OrderID { get; set; }


        /// <summary>
        /// 
        ///
        public int ProductID { get; set; }


        /// <summary>
        /// 
        ///
        public int Quantity { get; set; }



        /// <summary>
        /// 
        ///
        public decimal Price { get; set; }
    }
}
