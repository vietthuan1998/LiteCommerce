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
    public class CateloryController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            int pageSize = 5;
            int rowCount = 0;
            List<Category> listOfCategory = CatalogBLL.ListOfCategorys(page, pageSize, searchValue, out rowCount);
            var model = new Models.CategoryPaginationResult()
            {
                Page = page,
                PageSize = pageSize,
                RowCount = rowCount,
                SearchValue = searchValue,
                Data = listOfCategory
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
                    ViewBag.Title = "Create new Category";
                    Category newCategory = new Category()
                    {
                        CategoryID = 0
                    };
                    return View(newCategory);
                }
                else
                {
                    ViewBag.Title = "Edit a Category";
                    Category editCategory = CatalogBLL.GetCategory(Convert.ToInt32(id));
                    if (editCategory == null) return RedirectToAction("Index");
                    else return View(editCategory);
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
        public ActionResult Input(Category data)
        {
            //try
            //{
            //TODO: kiểm tr tính hợp lệ của dữ liệu
            if (string.IsNullOrEmpty(data.CategoryName))
                ModelState.AddModelError("CategoryName", "CategoryName Expected");

            if (string.IsNullOrEmpty(data.Description))
                data.Description = "";

            if (!ModelState.IsValid)
            {
                return View(data);
            }
            //TODO: Lưu dữ liệu vào DB
            if (data.CategoryID == 0)
            {
                CatalogBLL.AddCategory(data);
            }
            else
            {
                CatalogBLL.UpdateCategory(data);
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
        public ActionResult Delete(int[] categoryIDs = null)
        {
            if (categoryIDs != null)
                CatalogBLL.DeleteCategory(categoryIDs);
            return RedirectToAction("Index");
        }
    }
}