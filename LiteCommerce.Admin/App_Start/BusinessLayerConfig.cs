using LiteCommerce.BusinessLayers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace LiteCommerce.Admin
{
    /// <summary>
    /// 
    /// </summary>
    public class BusinessLayerConfig
    {
        /// <summary>
        /// 
        /// </summary>
        public static void Init()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["LiteCommerce"].ConnectionString;
            //TODO: khởi tạo các BLL khi cần sử dụng đến.
            CatalogBLL.Initialize(connectionString);
            UserAccountBLL.Initialize(connectionString);
        }
    }
}