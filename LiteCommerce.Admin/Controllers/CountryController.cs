using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin.Controllers
{
    [Authorize(Roles = WebUserRoles.DATAMAN)]
    public class CountryController : Controller
    {

        public ActionResult Index(int page = 1, string searchValue = "")
        {
            int pageSize = 5;
            int rowCount = 0;
            List<Country> listOfCountries = CatalogBLL.ListOfCountries(page, pageSize, searchValue, out rowCount);
            var model = new Models.CountryPaginationResult()
            {
                Page = page,
                PageSize = pageSize,
                RowCount = rowCount,
                SearchValue = searchValue,
                Data = listOfCountries
            };
            return View(model);
        }
        [HttpGet]
        public ActionResult Input(string id = "" )
        {

            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    ViewBag.Title = "Create new Country";
                    Country newCountry = new Country()
                    {
                        CountryID = 0
                    };
                    return View(newCountry);
                }
                else
                {
                    ViewBag.Title = "Edit a Country";
                    Country editCountry = CatalogBLL.GetCountry(Convert.ToInt32(id));
                    if (editCountry == null) return RedirectToAction("Index");
                    else return View(editCountry);
                }
            }
            catch (Exception ex)
            {
                return Content(ex.Message + ":" + ex.StackTrace);
            }
        }
        [HttpPost]
        public ActionResult Input(Country data)
        {
            if (string.IsNullOrEmpty(data.CountryName))
                ModelState.AddModelError("CountryName", "CountryName Expected");
            if (string.IsNullOrEmpty(data.Code))
                ModelState.AddModelError("Code", "Code Expected");

            if (!ModelState.IsValid)
            {
                return View(data);
            }
            //TODO: Lưu dữ liệu vào DB
            if (data.CountryID == 0)
            {
                CatalogBLL.AddCountry(data);
            }
            else
            {
                CatalogBLL.UpdateCountry(data);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Delete(int[] countryIDs = null)
        {
            if (countryIDs != null)
                CatalogBLL.DeleteCountries(countryIDs);
            return RedirectToAction("Index");
        }
    }
}