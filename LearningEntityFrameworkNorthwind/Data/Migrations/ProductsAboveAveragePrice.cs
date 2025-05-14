using System;
using System.Collections.Generic;

namespace LearningEntityFrameworkNorthwind.Data.Migrations;

public partial class ProductsAboveAveragePrice
{
    public string ProductName { get; set; } = null!;

    public decimal? UnitPrice { get; set; }
}
