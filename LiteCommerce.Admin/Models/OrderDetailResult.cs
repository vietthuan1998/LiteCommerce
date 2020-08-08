using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin.Models
{
    public class OrderDetailResult
    {
        public Order order { get; set; }
        public List<OrderDetail> data { get; set; }
    }
}