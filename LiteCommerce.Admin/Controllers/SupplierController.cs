using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebGrease.Activities;

namespace LiteCommerce.Admin.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize(Roles = WebUserRoles.DATAMAN)]
    public class SupplierController : Controller
    {
        // GET: Supplier
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            //int pageSize = 3;
            //int rowCount = 0;
            //List<Supplier> model = CatalogBLL.ListOfSupplier(page, pageSize, searchValue,out rowCount);
            //ViewBag.RowCount = rowCount;
            //return View(model);

            int pageSize = 5;
            int rowCount = 0;
            List<Supplier> listOfSupplier = CatalogBLL.ListOfSupplier(page, pageSize, searchValue, out rowCount);
            var model = new Models.SupplierPaginationResult()
            {
                Page = page,
                PageSize = pageSize,
                RowCount = rowCount,
                SearchValue = searchValue,
                Data = listOfSupplier
            };
            return View(model);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Input(string id = "")
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    ViewBag.Title = "Create new Supplier";
                    Supplier newSupplier = new Supplier()
                    {
                        SupplierID = 0
                    };
                    return View(newSupplier);
                }
                else
                {
                    ViewBag.Title = "Edit a Supplier";
                    Supplier editSupplier = CatalogBLL.GetSupplier(Convert.ToInt32(id));
                    if (editSupplier == null) return RedirectToAction("Index");
                    else return View(editSupplier);
                }
            }
            catch (Exception ex)
            {
                return Content(ex.Message + ":" + ex.StackTrace);
            }


        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Input(Supplier data)
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
            if (string.IsNullOrEmpty(data.HomePage))
                data.HomePage = "";
            if (!ModelState.IsValid)
            {
                return View(data);
            }
            //TODO: Lưu dữ liệu vào DB
            if (data.SupplierID == 0)
            {
                CatalogBLL.AddSupplier(data);
            }
            else
            {
                CatalogBLL.UpdateSupplier(data);
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
        public ActionResult Delete(int[] supplierIDs = null)
        {
            if(supplierIDs != null)
                CatalogBLL.DeleteSupplier(supplierIDs);
            return RedirectToAction("Index");
        }
    }
}