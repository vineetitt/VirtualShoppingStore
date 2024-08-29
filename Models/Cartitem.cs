using System;
using System.Collections.Generic;

namespace VirtualShoppingStore.Models;

public partial class Cartitem
{
    public int CartItemId { get; set; }

    public int? UserId { get; set; }

    public int? ProductId { get; set; }

    public int Quantity { get; set; }

    public DateTime? CreatedAt { get; set; }

    public decimal? TotalAmount { get; set; }

    public bool? IsPlaced { get; set; }

    public virtual Product? Product { get; set; }

    public virtual User? User { get; set; }
}
