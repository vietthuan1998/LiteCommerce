using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin.Models
{
    public class OrderPaginationResult : PaginationResult
    {
        public string Customer { get; set; }
        public int Employee { get; set; }
        public int Shipper { get; set; }
        public List<Order> Data { get; set; }
    }
}