using System.ComponentModel.DataAnnotations;

namespace VirtualShoppingStore.Models.DTO.OrderDto
{
    /// <summary>
    /// 
    /// </summary>
    public class ResponseOrderDto
    {
        /// <summary>
        /// 
        /// </summary>

        [Key]
        public int OrderId { get; set; }

        /// <summary>
        /// 
        /// </summary>

        public int? UserId { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// 
        [Required]
        public DateTime OrderDate { get; set; }


        /// <summary>
        /// 
        /// </summary>

        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Total amount must be greater than zero.")]
        public decimal TotalAmount { get; set; }


        /// <summary>
        /// 
        /// </summary>

        public int? StatusId { get; set; }

    }
}
