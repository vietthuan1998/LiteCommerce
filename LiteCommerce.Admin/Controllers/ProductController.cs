using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.IO;

namespace LiteCommerce.Admin.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    public class ProductController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult Index(int category = 0, int supplier = 0, string searchValue = "",int page=1)
        {
            int pageSize = 5;
            int rowCount = 0;
            List<Product> listOfProduct = CatalogBLL.ListOfProduct(page, pageSize, searchValue, out rowCount, supplier, category);
            var model = new Models.ProductPaginationResult()
            {
                Page = page,
                PageSize = pageSize,
                RowCount = rowCount,
                SearchValue = searchValue,
                Category = category,
                Supplier = supplier,
                Data = listOfProduct
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
                    ViewBag.Title = "Create new product";
                    ViewBag.Method = "Add";
                    Product newProduct = new Product()
                    {
                        ProductID = 0
                    };
                    Models.ProductAttributeResult model = new Models.ProductAttributeResult()
                    {
                        Product = newProduct,
                        ProductAttributes = null
                    };
                    return View(model);
                }
                else
                {
                    ViewBag.Title = "Edit a product";
                    ViewBag.Method = "Edit";
                    Product editProduct = CatalogBLL.GetProduct(Convert.ToInt32(id));
                    List<ProductAttribute> attr = CatalogBLL.ListOfProductAttribute(Convert.ToInt32(id));
                    Models.ProductAttributeResult model = new Models.ProductAttributeResult()
                    {
                        Product = editProduct,
                        ProductAttributes = attr
                    };
                    if (editProduct == null)
                        return RedirectToAction("Index");
                    return View(model);
                }
            }
            catch(Exception ex)
            {
                return Content(ex.Message + ":" + ex.StackTrace);
            }
        }
        [HttpPost]
        public ActionResult Input(Product model, HttpPostedFileBase uploadFile, string method)
        {
            Models.ProductAttributeResult models = new Models.ProductAttributeResult();
            try
            {
                //TODO: Kiểm tra tính hợp lệ của dữ liệu
                if (string.IsNullOrEmpty(model.ProductName))
                    ModelState.AddModelError("ProductName", "ProductName expected!");
                if (string.IsNullOrEmpty(model.QuantityPerUnit))
                    ModelState.AddModelError("QuantityPerUnit", "QuantityPerUnit expected!");
                if (double.IsNaN(model.UnitPrice))
                    ModelState.AddModelError("UnitPrice", "UnitPrice expected!");
                if (model.SupplierID == 0)
                    ModelState.AddModelError("SupplierID", "SupplierID expected!");
                if (model.CategoryID == 0)
                    ModelState.AddModelError("CategoryID", "CategoryID expected!");
                if (string.IsNullOrEmpty(model.Descriptions))
                    model.Descriptions = "";
                if (string.IsNullOrEmpty(model.PhotoPath))
                    model.PhotoPath = "";

                //Upload ảnh
                if (uploadFile != null)
                {
                    string folder = Server.MapPath("~/Images/Uploads");
                    //Lấy phần mở rộng của file
                    string extensionName = System.IO.Path.GetExtension(uploadFile.FileName);
                    // Tạo tên file ngẫu nhiên
                    string fileName = $"{DateTime.Now.Ticks}{Path.GetExtension(uploadFile.FileName)}";
                    string filePath = Path.Combine(folder, fileName);
                    uploadFile.SaveAs(filePath);

                    model.PhotoPath = fileName;
                    
                }

                if (method == "Add")
                {
                    ViewBag.Title = "Create new product";
                    ViewBag.Method = "Add";
                    Product newProduct = new Product()
                    {
                        ProductID = 0
                    };
                    models = new Models.ProductAttributeResult()
                    {
                        Product = newProduct,
                        ProductAttributes = null
                    };
                }
                else if (method == "Edit")
                {
                    ViewBag.Title = "Edit a product";
                    ViewBag.Method = "Edit";
                    Product editProduct = CatalogBLL.GetProduct(Convert.ToInt32(model.ProductID));
                    List<ProductAttribute> attr = CatalogBLL.ListOfProductAttribute(Convert.ToInt32(model.ProductID));
                    models = new Models.ProductAttributeResult()
                    {
                        Product = editProduct,
                        ProductAttributes = attr
                    };
                }

                if (!ModelState.IsValid)
                {
                    return View(models);
                }

                //TODO: Lưu dữ liệu vào DB
                if (model.ProductID == 0)
                {
                    CatalogBLL.AddProduct(model);
                }
                else
                {
                    CatalogBLL.UpdateProduct(model);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message + ":" + ex.StackTrace);
                return View(models);
            }
        }
        [HttpGet]
        public ActionResult Attribute(string ProductID = "", string AttributeID = "")
        {
            try
            {
                if (string.IsNullOrEmpty(AttributeID))
                {
                    ViewBag.Title = "Create new product attributes";
                    ProductAttribute newProductAtribute = new ProductAttribute()
                    {
                        AttributeID = 0,
                        ProductID = Convert.ToInt32(ProductID)
                    };
                    return View(newProductAtribute);
                }
                else
                {
                    ViewBag.Title = "Edit product attributes";
                    ProductAttribute editProductAttribute = CatalogBLL.GetProductAttribute(Convert.ToInt32(AttributeID));
                    if (editProductAttribute == null)
                        return RedirectToAction("Input/" + Convert.ToString(ProductID));
                    return View(editProductAttribute);
                }
            }
            catch (Exception ex)
            {
                return Content(ex.Message + ":" + ex.StackTrace);
            }
        }
        [HttpPost]
        public ActionResult Attribute(ProductAttribute model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.AttributeValues))
                    ModelState.AddModelError("AttributeValues", "AttributeValues expected!");
                if (string.IsNullOrEmpty(model.AttributeName))
                    ModelState.AddModelError("AttributeName", "AttributeName expected!");

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                //TODO: Lưu dữ liệu vào DB
                if (model.AttributeID == 0)
                {
                    CatalogBLL.AddProductAttribute(model);
                }
                else
                {
                    CatalogBLL.UpdateProductAttribute(model);
                }
                return RedirectToAction("Input/" + Convert.ToString(model.ProductID));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message + ":" + ex.StackTrace);
                return View(model);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="productIDs"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int[] productIDs = null)
        {
            if (productIDs != null)
                CatalogBLL.DeleteProducts(productIDs);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult DeleteAttribute(int productID, int[] attributeIDs = null)
        {
            if (attributeIDs != null)
                CatalogBLL.DeleteProductAttributes(attributeIDs);
            return RedirectToAction("Input/" + Convert.ToString(productID));
        }
    }
}