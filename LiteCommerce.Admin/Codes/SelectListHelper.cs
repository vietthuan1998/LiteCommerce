using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerce.Admin
{
    public static class SelectListHelper
    {
        public static List<SelectListItem> Countries(bool allowSelectAll = true)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (allowSelectAll)
            {
                list.Add(new SelectListItem() { Value = "", Text = "--Choose Country--" });
            }
            List<Country> data = CatalogBLL.ListOfCountries(1,-1,"");
            foreach (var country in data)
            {
                list.Add(new SelectListItem() { Value = country.CountryName, Text = country.CountryName });
            }
            return list;
        }

        public static List<SelectListItem> Categories(bool allowSelectAll = true)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (allowSelectAll)
            {
                list.Add(new SelectListItem() { Value = "0", Text = " --- All Categories---" });
            }
            foreach(var i in CatalogBLL.ListOfCategorys(1,-1,""))
            {
                list.Add(new SelectListItem() { Value = Convert.ToString(i.CategoryID), Text = i.CategoryName });
            }
            

            return list;
        }

        public static List<SelectListItem> Suppliers(bool allowSelectAll = true)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (allowSelectAll)
            {
                list.Add(new SelectListItem() { Value = "0", Text = " --- All Supplier---" });
            }
            foreach (var i in CatalogBLL.ListOfSupplier(1,-1,""))
            {
                list.Add(new SelectListItem() { Value = Convert.ToString(i.SupplierID), Text = i.CompanyName });
            }

            return list;
        }
        public static List<SelectListItem> Shippers(bool allowSelectAll = true)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (allowSelectAll)
            {
                list.Add(new SelectListItem() { Value = "0", Text = " --- All Shippers---" });
            }
            foreach (var i in CatalogBLL.ListOfShippers(1, -1, ""))
            {
                list.Add(new SelectListItem() { Value = Convert.ToString(i.ShipperID), Text = i.CompanyName });
            }

            return list;
        }
        public static List<SelectListItem> Attributes(bool allowSelectAll = true)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (allowSelectAll)
            {
                list.Add(new SelectListItem() { Value = "", Text = "--Choose Attribute--" });
            }
            List<Attributes> data = CatalogBLL.ListOfAttributes();
            foreach (var attribute in data)
            {
                list.Add(new SelectListItem() { Value = attribute.AttributeName, Text = attribute.AttributeName });
            }
            return list;
        }
        public static List<SelectListItem> Employee(bool allowSelectAll = true)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (allowSelectAll)
            {
                list.Add(new SelectListItem() { Value = "0", Text = " --- All Employees---" });
            }
            foreach (var i in CatalogBLL.ListOfEmployees(1,-1,""))
            {
                list.Add(new SelectListItem() { Value = Convert.ToString(i.EmployeeID), Text = i.FirstName + " " +i.LastName });
            }

            return list;
        }
        public static List<SelectListItem> Customer(bool allowSelectAll = true)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (allowSelectAll)
            {
                list.Add(new SelectListItem() { Value = "", Text = " --- All Customer---" });
            }
            foreach (var i in CatalogBLL.ListOfCustomers(1,-1,"",""))
            {
                list.Add(new SelectListItem() { Value = i.CustomerID, Text = i.CompanyName });
            }

            return list;
        }
        public static List<SelectListItem> Products(bool allowSelectAll = true)
        {
            List<SelectListItem> list = new List<SelectListItem>();
            if (allowSelectAll)
            {
                list.Add(new SelectListItem() { Value = "0", Text = "--Choose Product--" });
            }
            List<Product> data = CatalogBLL.ListOfProduct(1, -1, "", 0, 0);
            foreach (var product in data)
            {
                list.Add(new SelectListItem() { Value = Convert.ToString(product.ProductID), Text = product.ProductName });
            }
            return list;
        }
    }
}