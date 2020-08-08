using LiteCommerce.DataLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.BusinessLayers
{
    /// <summary>
    /// các chức năng xử lý nghiệp vụ liên quan đến
    /// quản lý dữ liệu chung của hệ thông :
    ///     Nhà cung cấp, khác hàng, mặt hàng, ........
    /// </summary>
    public static class CatalogBLL
    {
        #region khai báo các thuộc tính giao tiếp vs DAL

        /// <summary>
        /// 
        /// </summary>
        private static ISupplierDAL SupplierDB { get; set; }
        private static ICustomerDAL CustomerDB { get; set; }
        private static IShipperDAL ShipperDB { get; set; }
        private static IEmployeeDAL EmployeeDB { get; set; }
        private static ICategoryDAL CategoryDB { get; set; }
        private static IProductDAL ProductDB { get; set; }
        private static ICountryDAL CountryDB { get; set; }
        private static IAttributeDAL AttributeDB { get; set; }
        private static IProductAttributeDAL ProductAttributeDB { get; set; }
        private static IOrderDAL OrderDB { get; set; }
        private static IOrderDetailDAL DetailDB { get; set; }




        #endregion
        /// <summary>
        /// Hàm phải được gọi để khởi tạo chức năng tác nghiệp
        /// </summary>
        /// <param name="connectionString"></param>
        public static void Initialize(string connectionString)
        {
            SupplierDB = new DataLayers.SqlServer.SupplierDAL(connectionString);
            CustomerDB = new DataLayers.SqlServer.CustomerDAL(connectionString);
            ShipperDB = new DataLayers.SqlServer.ShipperDAL(connectionString);
            EmployeeDB = new DataLayers.SqlServer.EmployeeDAL(connectionString);
            CategoryDB = new DataLayers.SqlServer.CategorieDAL(connectionString);
            ProductDB = new DataLayers.SqlServer.ProductDAL(connectionString);
            CountryDB = new DataLayers.SqlServer.CountryDAL(connectionString);
            AttributeDB = new DataLayers.SqlServer.AttributeDAL(connectionString);
            ProductAttributeDB = new DataLayers.SqlServer.ProductAttributeDAL(connectionString);
            OrderDB = new DataLayers.SqlServer.OrderDAL(connectionString);
            DetailDB = new DataLayers.SqlServer.OrderDetailDAL(connectionString);
        }

        

        #region khai báo các chứng năng xử lý nghiệp vụ
        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagaSize"></param>
        /// <param name="searchValue"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public static List<Supplier> ListOfSupplier(int page, int pagaSize, string searchValue, out int rowCount)
        {
            if (page < 1)
                page = 1;
            if (pagaSize < 0)
                pagaSize = 20;
            rowCount = SupplierDB.Count(searchValue);
            return SupplierDB.List(page, pagaSize, searchValue);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Supplier> ListOfSupplier(int page, int pagaSize, string searchValue)
        {
            if (page < 1)
                page = 1;
            if (pagaSize < 0)
                pagaSize = 2000;
            return SupplierDB.List(page, pagaSize, searchValue);
        }
        public static List<Customer> ListOfCustomers(int page, int pagaSize, string searchValue, out int rowCount, string countryName)
        {
            if (page < 1)
                page = 1;
            if (pagaSize < 0)
                pagaSize = 20;
            rowCount = CustomerDB.Count(searchValue, countryName);
            return CustomerDB.List(page, pagaSize, searchValue, countryName);
        }
        public static List<Customer> ListOfCustomers(int page, int pagaSize, string searchValue, string countryName)
        {
            if (page < 1)
                page = 1;
            if (pagaSize < 0)
                pagaSize = 2000;
            return CustomerDB.List(page, pagaSize, searchValue, countryName);
        }
        public static List<Shipper> ListOfShippers(int page, int pagaSize, string searchValue, out int rowCount)
        {
            if (page < 1)
                page = 1;
            if (pagaSize < 0)
                pagaSize = 20;
            rowCount = ShipperDB.Count(searchValue);
            return ShipperDB.List(page, pagaSize, searchValue);
        }
        public static List<Shipper> ListOfShippers(int page, int pageSize, string searchValue)
        {
            if (page < 1)
                page = 1;
            if (pageSize < 0)
                pageSize = 2000;
            return ShipperDB.List(page, pageSize, searchValue);
        }
        public static List<Employee> ListOfEmployees(int page, int pagaSize, string searchValue, out int rowCount)
        {
            if (page < 1)
                page = 1;
            if (pagaSize < 0)
                pagaSize = 20;
            rowCount = EmployeeDB.Count(searchValue);
            return EmployeeDB.List(page, pagaSize, searchValue);
        }
        public static List<Employee> ListOfEmployees(int page, int pagaSize, string searchValue)
        {
            if (page < 1)
                page = 1;
            if (pagaSize < 0)
                pagaSize = 2000;
            return EmployeeDB.List(page, pagaSize, searchValue);
        }
        public static List<Category> ListOfCategorys(int page, int pagaSize, string searchValue, out int rowCount)
        {
            if (page < 1)
                page = 1;
            if (pagaSize < 0)
                pagaSize = 20;
            rowCount = CategoryDB.Count(searchValue);
            return CategoryDB.List(page, pagaSize, searchValue);
        }
        public static List<Category> ListOfCategorys(int page, int pagaSize, string searchValue)
        {
            if (page < 1)
                page = 1;
            if (pagaSize < 0)
                pagaSize = 2000;
            return CategoryDB.List(page, pagaSize, searchValue);
        }
        public static List<Product> ListOfProduct(int page, int pagaSize, string searchValue, out int rowCount,int supplierId, int categoriesId)
        {
            if (page < 1)
                page = 1;
            if (pagaSize < 0)
                pagaSize = 20;
            rowCount = ProductDB.Count(searchValue, supplierId, categoriesId);
            return ProductDB.List(page, pagaSize, searchValue, supplierId, categoriesId);
        }
        public static List<Product> ListOfProduct(int page, int pagaSize, string searchValue, int supplierId, int categoriesId)
        {
            if (page < 1)
                page = 1;
            if (pagaSize < 0)
                pagaSize = 2000;
            return ProductDB.List(page, pagaSize, searchValue, supplierId, categoriesId);
        }
        public static List<Country> ListOfCountries(int page, int pagaSize, string searchValue, out int rowCount)
        {
            if (page < 1)
                page = 1;
            if (pagaSize < 0)
                pagaSize = 20;
            rowCount = CountryDB.Count(searchValue);
            return CountryDB.List(page, pagaSize, searchValue);
        }
        public static List<Country> ListOfCountries(int page, int pagaSize, string searchValue)
        {
            if (page < 1)
                page = 1;
            if (pagaSize < 0)
                pagaSize = 2000;
            return CountryDB.List(page, pagaSize, searchValue);
        }

        /// <summary>
        /// LÁy thông tin 1 supplier dựa vào ID
        /// </summary>
        /// <param name="supplierId"></param>
        public static Supplier GetSupplier(int supplierId)
        {
            return SupplierDB.Get(supplierId);
        }
        /// <summary>
        /// Bổ sung 1 supplier. hàm trả về DI của supplier đc bổ sung
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddSupplier(Supplier data)
        {
            return SupplierDB.Add(data);
        }
        /// <summary>
        /// Update thông tin 1 supplier
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateSupplier(Supplier data)
        {
            return SupplierDB.Update(data);
        }
        /// <summary>
        /// Xóa các supplier dựa vào DI
        /// </summary>
        /// <param name="supplierIds"></param>
        /// <returns></returns>
        public static int DeleteSupplier(int[] supplierIds)
        {
            return SupplierDB.Delete(supplierIds);
        }


        /// <summary>
        /// LÁy thông tin 1 supplier dựa vào ID
        /// </summary>
        /// <param name="supplierId"></param>
        public static Category GetCategory(int categoryId)
        {
            return CategoryDB.Get(categoryId);
        }
        /// <summary>
        /// Bổ sung 1 supplier. hàm trả về DI của supplier đc bổ sung
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddCategory(Category data)
        {
            return CategoryDB.Add(data);
        }
        /// <summary>
        /// Update thông tin 1 supplier
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateCategory(Category data)
        {
            return CategoryDB.Update(data);
        }
        /// <summary>
        /// Xóa các supplier dựa vào DI
        /// </summary>
        /// <param name="supplierIds"></param>
        /// <returns></returns>
        public static int DeleteCategory(int[] categoryIds)
        {
            return CategoryDB.Delete(categoryIds);
        }


        public static Shipper GetShipper(int shipperId)
        {
            return ShipperDB.Get(shipperId);
        }
        /// <summary>
        /// Bổ sung 1 supplier. hàm trả về DI của supplier đc bổ sung
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddShipper(Shipper data)
        {
            return ShipperDB.Add(data);
        }
        /// <summary>
        /// Update thông tin 1 supplier
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateShipper(Shipper data)
        {
            return ShipperDB.Update(data);
        }
        /// <summary>
        /// Xóa các supplier dựa vào DI
        /// </summary>
        /// <param name="supplierIds"></param>
        /// <returns></returns>
        public static int DeleteShipper(int[] shipperIds)
        {
            return ShipperDB.Delete(shipperIds);
        }

        public static Employee GetEmployee(int employeeId)
        {
            return EmployeeDB.Get(employeeId);
        }
        /// <summary>
        /// Bổ sung 1 supplier. hàm trả về DI của supplier đc bổ sung
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddEmployee(Employee data)
        {
            return EmployeeDB.Add(data);
        }
        /// <summary>
        /// Update thông tin 1 supplier
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateEmployee(Employee data)
        {
            return EmployeeDB.Update(data);
        }
        /// <summary>
        /// Xóa các supplier dựa vào DI
        /// </summary>
        /// <param name="supplierIds"></param>
        /// <returns></returns>
        public static int DeleteEmployee(int[] employeeIds)
        {
            return EmployeeDB.Delete(employeeIds);
        }

        public static Customer GetCustomer(string customerID)
        {
            return CustomerDB.Get(customerID);
        }
        /// <summary>
        /// Bổ sung 1 supplier. hàm trả về DI của supplier đc bổ sung
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string AddCustomer(Customer data)
        {
            return CustomerDB.Add(data);
        }
        /// <summary>
        /// Update thông tin 1 supplier
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateCustomer(Customer data)
        {
            return CustomerDB.Update(data);
        }
        /// <summary>
        /// Xóa các supplier dựa vào DI
        /// </summary>
        /// <param name="supplierIds"></param>
        /// <returns></returns>
        public static int DeleteCustomer(string[] customerIDs)
        {
            return CustomerDB.Delete(customerIDs);
        }
        
        public static List<Attributes> ListOfAttributes()
        {
            return AttributeDB.List();
        }

        public static List<ProductAttribute> ListOfProductAttribute(int productID)
        {
            return ProductAttributeDB.List(productID);
        }

        public static int AddProductAttribute(ProductAttribute data)
        {
            return ProductAttributeDB.Add(data);
        }

        public static bool UpdateProductAttribute(ProductAttribute data)
        {
            return ProductAttributeDB.Update(data);
        }

        public static int DeleteProductAttributes(int[] attributeIDs)
        {
            return ProductAttributeDB.Delete(attributeIDs);
        }

        public static ProductAttribute GetProductAttribute(int attributeID)
        {
            return ProductAttributeDB.Get(attributeID);
        }

        public static Product GetProduct(int productID)
        {
            return ProductDB.Get(productID);
        }

        public static int AddProduct(Product data)
        {
            return ProductDB.Add(data);
        }

        public static bool UpdateProduct(Product data)
        {
            return ProductDB.Update(data);
        }

        public static int DeleteProducts(int[] productIDs)
        {
            return ProductDB.Delete(productIDs);
        }

        public static Country GetCountry(int countryID)
        {
            return CountryDB.Get(countryID);
        }

        public static int AddCountry(Country data)
        {
            return CountryDB.Add(data);
        }
        public static bool UpdateCountry(Country data)
        {
            return CountryDB.Update(data);
        }

        public static int DeleteCountries(int[] countryIDs)
        {
            return CountryDB.Delete(countryIDs);
        }

        public static List<Order> ListOfOrders(int page, int pageSize, string searchValue, string customer, int employee, int shipper, out int rowCount)
        {
            if (page < 1)
                page = 1;
            if (pageSize < 0)
                pageSize = 20;
            rowCount = OrderDB.Count(searchValue, customer, employee, shipper);
            return OrderDB.List(page, pageSize, searchValue, customer, employee, shipper);
        }

        public static Order GetOrder(int orderID)
        {
            return OrderDB.Get(orderID);
        }

        public static int AddOrder(Order data)
        {
            return OrderDB.Add(data);
        }

        public static bool UpdateOrder(Order data)
        {
            return OrderDB.Update(data);
        }

        public static int DeleteOrders(int[] orderIDs)
        {
            return OrderDB.Delete(orderIDs);
        }

        public static List<OrderDetail> ListOfOrderDetails(int orderID)
        {
            return DetailDB.List(orderID);
        }

        #endregion
    }
}
