using System;
using System.Collections.Generic;

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
    public int Quantity { get; set; }

    /// <summary>
    /// 
    /// </summary>
    public DateTime? CreatedAt { get; set; }



    /// <summary>
    /// 
    /// </summary>
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
