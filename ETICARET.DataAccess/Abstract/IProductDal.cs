using ETICARET.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETICARET.DataAccess.Abstract
{
    public interface IProductDal : IRepository<Product>
    {
        int GetCountByCategory(string category);
        Product GetProductDetails(int id);
        List<Product> GetProductsByCategory(string category,int page,int pageSize);
        void Update(Product entity, int[] categoryIds);
    }
}
