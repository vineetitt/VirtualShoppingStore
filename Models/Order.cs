using System;
using System.Collections.Generic;

namespace VirtualShoppingStore.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? UserId { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal TotalAmount { get; set; }

    public int? StatusId { get; set; }

    public virtual ICollection<Orderitem> Orderitems { get; set; } = new List<Orderitem>();

    public virtual Status? Status { get; set; }

    public virtual User? User { get; set; }
}
