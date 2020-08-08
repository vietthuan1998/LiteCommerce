using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin.Models
{
    public class ProductPaginationResult : PaginationResult
    {
        public int Category { get; set; }
        public int Supplier { get; set; }
        public List<Product> Data { get; set; }
    }
}