using System;
using System.Collections.Generic;

namespace LearningEntityFrameworkNorthwind.Data.Migrations;

public partial class OrderSubtotal
{
    public int OrderId { get; set; }

    public decimal? Subtotal { get; set; }
}
