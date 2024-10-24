using System;
using System.Collections.Generic;

namespace VirtualShoppingStore.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public bool? Deactive { get; set; }

    public string PasswordHash { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? PhoneNo { get; set; }

    public DateTime? CreatedAt { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public virtual ICollection<Cartitem> Cartitems { get; set; } = new List<Cartitem>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
