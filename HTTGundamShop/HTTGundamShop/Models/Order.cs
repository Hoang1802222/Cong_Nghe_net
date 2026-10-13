using System;
using System.Collections.Generic;

namespace HTTGundamShop.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public DateTime? OrderDate { get; set; }

    public string? OrderStatus { get; set; }

    public decimal TotalAmount { get; set; }

    public string ShippingAddress { get; set; } = null!;

    public int UserId { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual User User { get; set; } = null!;
}
