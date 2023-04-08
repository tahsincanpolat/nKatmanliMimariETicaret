using ETICARET.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETICARET.Business.Abstract
{
    public interface IProductService
    {
        Product GetById(int id);
        List<Product> GetProductsByCategory(string category,int page,int pageSize);
        List<Product> GetAll();
        Product GetProductDetails(int id);
        void Create(Product entity);
        void Update(Product entity, int[] categoryIds);
        void Delete(Product entity);
        int GetCountByCategory(string category);
    }
}
