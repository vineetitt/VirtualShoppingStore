using System;
using System.Collections.Generic;

namespace VirtualShoppingStore.Models;

public partial class User
{
    /// <summary>
    /// 
    /// </summary>
    public int UserId { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string Username { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public bool? Deactive { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string PasswordHash { get; set; } = null!;
    /// <summary>
    /// 
    /// </summary>
    public string Email { get; set; } = null!;
    /// <summary>
    /// 
    /// </summary>
    public string? FirstName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string? LastName { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string? PhoneNo { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public DateTime? CreatedAt { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string? Address { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public string? City { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public virtual ICollection<Cartitem> Cartitems { get; set; } = new List<Cartitem>();
    /// <summary>
    /// 
    /// </summary>
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
