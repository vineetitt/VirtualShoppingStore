using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VirtualShoppingStore.Models;
/// <summary>
/// 
/// </summary>
public partial class Order
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

    /// <summary>
    /// 
    /// </summary>

    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();

    /// <summary>
    /// 
    /// </summary>
    public virtual Status? Status { get; set; }

    /// <summary>
    /// 
    /// </summary>

    public virtual User? User { get; set; }

}
