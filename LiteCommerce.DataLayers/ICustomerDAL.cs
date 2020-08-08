using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICustomerDAL
    {
        List<Customer> List(int page, int pageSize, string searchValue, string countryName);
        int Count(string searchValue, string countryName);
        /// <summary>
        /// Lấy 1 supplier theo ID
        /// </summary>
        /// <param name="supplierID"></param>
        /// <returns></returns>
        Customer Get(string customerID);
        /// <summary>
        /// Bổ sung 1 Supplier => trả về ID của Supplier vừa bổ sung.
        /// Nếu lỗi trả về -1;
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        string Add(Customer data);
        /// <summary>
        /// Edit 1 Supplier
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(Customer data);
        /// <summary>
        /// Xóa nhiều Supplier
        /// </summary>
        /// <param name="supplierIDs"></param>
        /// <returns></returns>
        int Delete(string[] customerIDs);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        List<Customer> GetAll();
    }
}
