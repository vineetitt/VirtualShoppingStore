using System;
using System.Collections.Generic;

namespace VirtualShoppingStore.Models;

/// <summary>
/// 
/// </summary>
public partial class Status
{
    /// <summary>
    /// 
    /// </summary>
    public int StatusId { get; set; }


    /// <summary>
    /// 
    /// </summary>
    public string StatusName { get; set; } = null!;


    /// <summary>
    /// 
    /// </summary>
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
