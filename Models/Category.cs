using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VirtualShoppingStore.Models;

/// <summary>
/// Category Class
/// </summary>

public partial class Category
{

    /// <summary>
    /// CategoryId
    /// </summary>

    
    public int CategoryId { get; set; }

    /// <summary>
    /// CategoryName
    /// </summary>

    [Required(ErrorMessage = "Category name is required.")]
    [StringLength(100, ErrorMessage = "Category name cannot exceed 100 characters.")]
    [MinLength(1, ErrorMessage ="Category name must have atleast one character in it")]
    public string CategoryName { get; set; } = null!;

    /// <summary>
    /// Products
    /// </summary>

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

}
