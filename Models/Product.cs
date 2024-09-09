using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VirtualShoppingStore.Models;


/// <summary>
/// 
/// </summary>

public partial class Product
{

    /// <summary>
    /// ProductId
    /// </summary>
    
    public int ProductId { get; set; }

    /// <summary>
    /// ProductName
    /// </summary>

    [Required(ErrorMessage = "Product name is required.")]
    [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
    public string ProductName { get; set; } = null!;

    /// <summary>
    /// Description
    /// </summary>

    [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
    public string? Description { get; set; }

    /// <summary>
    /// Price
    /// </summary>

    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
    public decimal Price { get; set; }

    /// <summary>
    /// StockQuantity
    /// </summary>

    [Range(0, int.MaxValue, ErrorMessage = "Stock Quantity must be a non-negative value.")]
    public int StockQuantity { get; set; }
   

    /// <summary>
    ///CategoryId
    /// </summary>

    public int? CategoryId { get; set; }

    /// <summary>
    /// CreatedAt
    /// </summary>

    public DateTime? CreatedAt { get; set; }

    /// <summary>
    /// UpdatedAt
    /// </summary>

    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// IsDeleted
    /// </summary>

    public bool? IsDeleted { get; set; }

    /// <summary>
    /// Cartitems
    /// </summary>

    public virtual ICollection<Cartitem> Cartitems { get; set; } = new List<Cartitem>();

    /// <summary>
    /// Category
    /// </summary>

    public virtual Category? Category { get; set; }

    /// <summary>
    /// Orderitems
    /// </summary>

    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();

}
