using System.Collections.Generic;

namespace dotNetDataBaseApp
{
    public interface IProductDal
    {
         List<Product> GetAllProducts();

        Product GetProductById(int id);

        List<Product> Find(string productName);

        int Count();

        int Create(Product p);

        int Update(Product p);

        int Delete(int product_id);
    }
}