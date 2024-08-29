using System;
using System.Collections.Generic;

namespace VirtualShoppingStore.Models;

public partial class Product
{
    /// <summary>
    /// 
    /// </summary>
    public int ProductId { get; set; }


    /// <summary>
    /// 
    /// </summary>
    public string ProductName { get; set; } = null!;


    /// <summary>
    /// 
    /// </summary>
    public string? Description { get; set; }



    /// <summary>
    /// 
    /// </summary>
    public decimal Price { get; set; }



    /// <summary>
    /// 
    /// </summary>
    public int StockQuantity { get; set; }



    /// <summary>
    /// 
    /// </summary>
    public int? CategoryId { get; set; }



    /// <summary>
    /// 
    /// </summary>
    public DateTime? CreatedAt { get; set; }



    /// <summary>
    /// 
    /// </summary>
    public DateTime? UpdatedAt { get; set; }



    /// <summary>
    /// 
    /// </summary>
    public bool? IsDeleted { get; set; }



    /// <summary>
    /// 
    /// </summary>
    public virtual ICollection<Cartitem> Cartitems { get; set; } = new List<Cartitem>();



    /// <summary>
    /// 
    /// </summary>
    public virtual Category? Category { get; set; }



    /// <summary>
    /// 
    /// </summary>
    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();
}
