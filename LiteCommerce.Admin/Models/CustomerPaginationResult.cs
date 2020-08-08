using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class CustomerPaginationResult: PaginationResult
    {
        /// <summary>
        /// 
        /// </summary>
        public List<Customer> Data { get; set; }
        public string CountryName { get; set; }
    }
}