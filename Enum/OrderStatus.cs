namespace VirtualShoppingStore.Enum
{
    /// <summary>
    /// Represents the possible statuses for an order in the Virtual Shopping Store.
    /// </summary>
    public enum OrderStatus
    {

        /// <summary>
        /// The order is pending and has not yet been processed.
        /// </summary>
        Pending = 1,

        /// <summary>
        /// The order is currently being processed.
        /// </summary>
        Processing = 2,

        /// <summary>
        /// The order has been shipped to the customer.
        /// </summary>
        Shipped = 3,

        /// <summary>
        /// The order has been delivered to the customer.
        /// </summary>
        Delivered = 4,


        /// <summary>
        /// The order has been cancelled.
        /// </summary>
        Cancelled = 5

    }

}
