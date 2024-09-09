using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VirtualShoppingStore.Models;


/// <summary>
/// 
/// </summary>
public partial class Orderitem
{
    /// <summary>
    /// OrderItemId
    /// </summary>
    
    public int OrderItemId { get; set; }

    /// <summary>
    /// OrderId
    /// </summary>

    public int? OrderId { get; set; }

    /// <summary>
    /// ProductId
    /// </summary>
    
    public int? ProductId { get; set; }

    /// <summary>
    /// Quantity
    /// </summary>
    
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than zero.")]
    public int Quantity { get; set; }

    /// <summary>
    /// Price
    /// </summary>

    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
    public decimal Price { get; set; }


    /// <summary>
    /// Order
    /// </summary>
    public virtual Order? Order { get; set; }

    /// <summary>
    /// Product
    /// </summary>

    public virtual Product? Product { get; set; }

}
