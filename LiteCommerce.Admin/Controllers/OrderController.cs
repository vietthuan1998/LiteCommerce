using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize( Roles = WebUserRoles.SALEMAN)]
    public class OrderController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(string customer = "", int employee = 0, int shipper = 0, string searchValue = "", int page = 1)
        {
            int pageSize = 5;
            int rowCount = 0;
            List<Order> listOfOrder = CatalogBLL.ListOfOrders(page, pageSize, searchValue, customer, employee, shipper, out rowCount);
            var model = new Models.OrderPaginationResult()
            {
                Page = page,
                PageSize = pageSize,
                RowCount = rowCount,
                SearchValue = searchValue,
                Customer = customer,
                Employee = employee,
                Shipper = shipper,
                Data = listOfOrder
            };
            return View(model);
        }

        public ActionResult Detail(string id = "")
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Title = "Order Detail";
                    ViewBag.Method = "Edit";
                    Order order = CatalogBLL.GetOrder(Convert.ToInt32(id));
                    List<OrderDetail> data = CatalogBLL.ListOfOrderDetails(Convert.ToInt32(id));
                    var model = new Models.OrderDetailResult()
                    {
                        order = order,
                        data = data
                    };
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                return Content(ex.Message + ":" + ex.StackTrace);
            }
        }
        public ActionResult Create()
        {
            return View();
        }
    }
}