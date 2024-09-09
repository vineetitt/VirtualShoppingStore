using System;
using System.Collections.Generic;

namespace VirtualShoppingStore.Models;

/// <summary>
/// 
/// </summary>
public partial class User
{
    /// <summary>
    /// UserId
    /// </summary>
    
    public int UserId { get; set; }

    /// <summary>
    /// Username
    /// </summary>
    
    public string Username { get; set; } = null!;

    /// <summary>
    /// Deactive
    /// </summary>
    
    public bool? Deactive { get; set; }

    /// <summary>
    /// PasswordHash
    /// </summary>

    public string PasswordHash { get; set; } = null!;

    /// <summary>
    /// Email
    /// </summary>
    
    public string Email { get; set; } = null!;

    /// <summary>
    /// FirstName
    /// </summary>
    
    public string? FirstName { get; set; }

    /// <summary>
    /// LastName
    /// </summary>
    
    public string? LastName { get; set; }

    /// <summary>
    /// PhoneNo
    /// </summary>

    public string? PhoneNo { get; set; }

    /// <summary>
    ///CreatedAt
    /// </summary>
    
    public DateTime? CreatedAt { get; set; }

    /// <summary>
    /// Address
    /// </summary>
    
    public string? Address { get; set; }

    /// <summary>
    /// City
    /// </summary>
    
    public string? City { get; set; }

    /// <summary>
    /// Cartitems
    /// </summary>

    public virtual ICollection<Cartitem> Cartitems { get; set; } = new List<Cartitem>();

    /// <summary>
    /// 
    /// </summary>
    
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

}
