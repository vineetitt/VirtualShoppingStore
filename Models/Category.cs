using System;
using System.Collections.Generic;

namespace VirtualShoppingStore.Models;

/// <summary>
/// 
/// </summary>
public partial class Category
{

    /// <summary>
    /// 
    /// </summary>
    public int CategoryId { get; set; }

    /// <summary>
    /// 
    /// </summary>

    public string CategoryName { get; set; } = null!;

    /// <summary>
    /// 
    /// </summary>

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
