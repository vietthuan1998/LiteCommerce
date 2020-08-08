using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface ICategoryDAL
    {

        List<Category> List(int page, int pageSize, string searchValue);

        int Count(string searchValue);

        Category Get(int categoryID);

        int Add(Category data);

        bool Update(Category data);

        int Delete(int[] categoryIDs);

        List<Category> GetAll();

    }
}
