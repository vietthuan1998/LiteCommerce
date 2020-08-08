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
    public class ShipperController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            int pageSize = 5;
            int rowCount = 0;
            List<Shipper> listOfShipper = CatalogBLL.ListOfShippers(page, pageSize, searchValue, out rowCount);
            var model = new Models.ShipperPaginationResult()
            {
                Page = page,
                PageSize = pageSize,
                RowCount = rowCount,
                SearchValue = searchValue,
                Data = listOfShipper
            };
            return View(model);
        }
        [HttpGet]
        public ActionResult Input(string id = "")
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    ViewBag.Title = "Create new Shipper";
                    Shipper newShipper = new Shipper()
                    {
                        ShipperID = 0
                    };
                    return View(newShipper);
                }
                else
                {
                    ViewBag.Title = "Edit a Shipper";
                    Shipper editShipper = CatalogBLL.GetShipper(Convert.ToInt32(id));
                    if (editShipper == null) return RedirectToAction("Index");
                    else return View(editShipper);
                }
            }
            catch (Exception ex)
            {
                return Content(ex.Message + ":" + ex.StackTrace);
            }
            
        }
        [HttpPost]
        public ActionResult Input(Shipper data)
        {
            //try
            //{
            //TODO: kiểm tr tính hợp lệ của dữ liệu
            if (string.IsNullOrEmpty(data.CompanyName))
                ModelState.AddModelError("CompanyName", "CompanyName Expected");
            if (string.IsNullOrEmpty(data.Phone))
                ModelState.AddModelError("Phone", "Phone Expected");

            if (!ModelState.IsValid)
            {
                return View(data);
            }
            //TODO: Lưu dữ liệu vào DB
            if (data.ShipperID == 0)
            {
                CatalogBLL.AddShipper(data);
            }
            else
            {
                CatalogBLL.UpdateShipper(data);
            }
            return RedirectToAction("Index");
            //}
            //catch (Exception ex)
            //{
            //    ModelState.AddModelError("", ex.Message + ": " + ex.StackTrace);
            //    return View(data);
            //}

        }
        [HttpPost]
        public ActionResult Delete(int[] shipperIDs = null)
        {
            if (shipperIDs != null)
                CatalogBLL.DeleteShipper(shipperIDs);
            return RedirectToAction("Index");
        }
    }
}