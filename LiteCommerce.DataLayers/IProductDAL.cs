using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface IProductDAL 
    {
        List<Product> List(int page, int pageSize, string searchValue,int supplierId,int categoriesId);

        int Count(string searchValue, int supplierID, int categoriesID);

        Product Get(int productID);

        int Add(Product data);

        bool Update(Product data);

        int Delete(int[] productIDs);

    }
}
