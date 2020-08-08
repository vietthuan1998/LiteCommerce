using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface IOrderDetailDAL
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderID"></param>
        /// <returns></returns>
        List<OrderDetail> List(int orderID);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderID"></param>
        /// <param name="customerID"></param>
        /// <returns></returns>
        OrderDetail Get(int orderID, int productID);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Add(OrderDetail data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        bool Update(OrderDetail data);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="detailIDs"></param>
        /// <returns></returns>
        int Delete(string[] detailIDs);
    }
}
