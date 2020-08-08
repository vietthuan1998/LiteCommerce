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
    [Authorize(Roles = WebUserRoles.DATAMAN)]
    public class CustomerController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int page=1, string searchValue = "", string countryName = "")
        {
            int pageSize = 10;
            int rowCount = 0;
            List<Customer> listOfCustomers = CatalogBLL.ListOfCustomers(page, pageSize, searchValue, out rowCount, countryName);
            var model = new Models.CustomerPaginationResult()
            {
                Page = page,
                PageSize = pageSize,
                RowCount = rowCount,
                SearchValue = searchValue,
                CountryName = countryName,
                Data = listOfCustomers  
            };
            return View(model);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Input(string id = "")
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    ViewBag.Title = "Create new Customer";
                    ViewBag.Method = "Add";
                    Customer newCustomer = new Customer()
                    {
                        CustomerID = ""
                    };
                    return View(newCustomer);
                }
                else
                {
                    ViewBag.Title = "Edit a Customer";
                    ViewBag.Method = "Edit";
                    Customer editCustomer = CatalogBLL.GetCustomer(id);
                    return View(editCustomer);
                }
            }
            catch (Exception ex)
            {
                return Content(ex.Message + ":" + ex.StackTrace);
            }
            
        }
        public ActionResult Input(Customer data, string method)
        {
            //try
            //{
            //TODO: kiểm tr tính hợp lệ của dữ liệu
            if (string.IsNullOrEmpty(data.CompanyName))
                ModelState.AddModelError("CompanyName", "CompanyName Expected");
            if (string.IsNullOrEmpty(data.ContactName))
                ModelState.AddModelError("ContactName", "ContactName Expected");
            if (string.IsNullOrEmpty(data.ContactTitle))
                ModelState.AddModelError("ContactTitle", "ContactTitle Expected");

            if (string.IsNullOrEmpty(data.Address))
                data.Address = "";
            if (string.IsNullOrEmpty(data.Country))
                data.Country = "";
            if (string.IsNullOrEmpty(data.City))
                data.City = "";
            if (string.IsNullOrEmpty(data.Phone))
                data.Phone = "";
            if (string.IsNullOrEmpty(data.Fax))
                data.Fax = "";
            if (string.IsNullOrEmpty(data.Address))
                data.Address = "";
            if (!ModelState.IsValid)
            {
                return View(data);
            }
            //TODO: Lưu dữ liệu vào DB
            if (method == "Add")
            {
                CatalogBLL.AddCustomer(data);
            }
            else
            {
                CatalogBLL.UpdateCustomer(data);
            }
            return RedirectToAction("Index");
            //}
            //catch (Exception ex)
            //{
            //    ModelState.AddModelError("", ex.Message + ": " + ex.StackTrace);
            //    return View(data);
            //}

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="supplierIDs"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(string[] customerIDs = null)
        {
            if (customerIDs != null)
                CatalogBLL.DeleteCustomer(customerIDs);
            return RedirectToAction("Index");
        }
    }
}