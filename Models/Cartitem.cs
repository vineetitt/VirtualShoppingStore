using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VirtualShoppingStore.Models;
/// <summary>
/// 
/// </summary>
public partial class Cartitem
{
    /// <summary>
    /// 
    /// </summary>
    public int CartItemId { get; set; }

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

    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
    public int Quantity { get; set; }

    /// <summary>
    /// 
    /// </summary>
    
    public DateTime? CreatedAt { get; set; }= DateTime.Now;

    /// <summary>
    /// 
    /// </summary>

    [Range(0, double.MaxValue, ErrorMessage = "Total Amount must be a positive value")]
    public decimal? TotalAmount { get; set; }

    /// <summary>
    /// 
    /// </summary>
    
    public bool? IsPlaced { get; set; }

    /// <summary>
    /// 
    /// </summary>
    
    public virtual Product? Product { get; set; }

    /// <summary>
    /// 
    /// </summary>
    
    public virtual User? User { get; set; }

}
