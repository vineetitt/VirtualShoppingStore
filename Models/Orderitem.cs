using System;
using System.Collections.Generic;

namespace VirtualShoppingStore.Models;


/// <summary>
/// 
/// </summary>
public partial class Orderitem
{
    /// <summary>
    /// 
    /// </summary>
    public int OrderItemId { get; set; }



    /// <summary>
    /// 
    /// </summary>
    public int? OrderId { get; set; }



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
    public decimal Price { get; set; }


    /// <summary>
    /// 
    /// </summary>
    public virtual Order? Order { get; set; }




    /// <summary>
    /// 
    /// </summary>
    public virtual Product? Product { get; set; }
}
