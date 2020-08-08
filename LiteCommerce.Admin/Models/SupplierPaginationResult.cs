﻿using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class SupplierPaginationResult :PaginationResult
    {
        /// <summary>
        /// 
        /// </summary>
        public List<Supplier> Data { get; set; }
    }
}