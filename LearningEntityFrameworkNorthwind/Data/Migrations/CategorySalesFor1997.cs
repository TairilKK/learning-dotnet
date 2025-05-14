using System;
using System.Collections.Generic;

namespace LearningEntityFrameworkNorthwind.Data.Migrations;

public partial class CategorySalesFor1997
{
    public string CategoryName { get; set; } = null!;

    public decimal? CategorySales { get; set; }
}
