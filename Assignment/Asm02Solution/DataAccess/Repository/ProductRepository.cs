using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class ProductRepository : IProductRepository
    {
        public void DeleteProduct(int productID) => ProductDAO.Instance.Remove(productID);

        public ProductObject GetProductByID(int productID) => ProductDAO.Instance.GetProductByID(productID);

        public IEnumerable<ProductObject> GetProductByName(string name) => ProductDAO.Instance.GetProductByName(name);

        public IEnumerable<ProductObject> GetProducts() => ProductDAO.Instance.GetProductList();

        public void InsertProduct(ProductObject product) => ProductDAO.Instance.AddNew(product);

        public void UpdateProduct(ProductObject product) => ProductDAO.Instance.Update(product);
    }
}
