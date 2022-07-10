using BusinessObject;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class ProductDAO:DBContext
    {
        private static ProductDAO instance = null;
        private static readonly object instanceLock = new object();
        private ProductDAO() { }
        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<ProductObject> GetProductList()
        {
            IDataReader dataReader = null;
            string SQLSelect = "SELECT [ProductId],[CategoryId],[ProductName],[Weight],[UnitPrice],[UnitInStock] FROM [dbo].[Product]";
            var products = new List<ProductObject>();
            try
            {
                dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection);
                while (dataReader.Read())
                {
                    products.Add(new ProductObject
                    {
                        ProductId = dataReader.GetInt32(0),
                        CategoryId = dataReader.GetInt32(1),
                        ProductName = dataReader.GetString(2),
                        Weight = dataReader.GetString(3),
                        UnitPrice = dataReader.GetDecimal(4),
                        UnitInStock = dataReader.GetInt32(5)
                    });
                }
            }
            catch (Exception EX)
            {
                throw new Exception(EX.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }
            return products;
        }

        public ProductObject GetProductByID(int productID)
        {
            ProductObject pro = null;
            IDataReader dataReader = null;
            string SQLSelect = "SELECT [ProductId],[CategoryId],[ProductName],[Weight],[UnitPrice],[UnitInStock] FROM [dbo].[Product]" +
                " where ProductId=@ProductId";
            try
            {
                var param = dataProvider.CreateParameter("@ProductId", 4, productID, DbType.Int32);
                dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection, param);
                if (dataReader.Read())
                {
                    pro = new ProductObject
                    {
                        ProductId = dataReader.GetInt32(0),
                        CategoryId = dataReader.GetInt32(1),
                        ProductName = dataReader.GetString(2),
                        Weight = dataReader.GetString(3),
                        UnitPrice = dataReader.GetDecimal(4),
                        UnitInStock = dataReader.GetInt32(5)
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }
            return pro;
        }

        public IEnumerable<ProductObject> GetProductByName(string nameproduct)
        {
            var products = new List<ProductObject>();
            IDataReader dataReader = null;
            string SQLSelect = "SELECT [ProductId],[CategoryId],[ProductName],[Weight],[UnitPrice],[UnitInStock] FROM [dbo].[Product]" +
                " where ProductName like @ProductName";
            try
            {
                var param = dataProvider.CreateParameter("@ProductName", 50, "%"+nameproduct+"%", DbType.String);
                dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection, param);
                if (dataReader.Read())
                {
                    products.Add( new ProductObject
                    {
                        ProductId = dataReader.GetInt32(0),
                        CategoryId = dataReader.GetInt32(1),
                        ProductName = dataReader.GetString(2),
                        Weight = dataReader.GetString(3),
                        UnitPrice = dataReader.GetDecimal(4),
                        UnitInStock = dataReader.GetInt32(5)
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                dataReader.Close();
                CloseConnection();
            }
            return products;
        }

        public void AddNew(ProductObject pro)
        {
            try
            {
                ProductObject product = GetProductByID(pro.ProductId);
                if (product == null)
                {
                    string SQLInsert = "INSERT INTO [dbo].[Product] ([CategoryId],[ProductName],[Weight],[UnitPrice],[UnitInStock])" +
                        "VALUES(@CategoryId,@ProductName,@Weight,@UnitPrice,@UnitInStock)";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(dataProvider.CreateParameter(("@CategoryId"), 4, pro.CategoryId, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter(("@ProductName"), 100, pro.ProductName, DbType.String));
                    parameters.Add(dataProvider.CreateParameter(("@Weight"), 10, pro.Weight, DbType.String));
                    parameters.Add(dataProvider.CreateParameter(("@UnitPrice"), 50, pro.UnitPrice, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter(("@UnitInStock"), 4, pro.UnitInStock, DbType.Int32));
                    dataProvider.Insert(SQLInsert, CommandType.Text, parameters.ToArray());

                }
                else
                {
                    throw new Exception("The product is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public void Update(ProductObject pro)
        {
            try
            {
                ProductObject product = GetProductByID(pro.ProductId);
                if (product != null)
                {
                    string SQLUpdate = "Update [Product] set [ProductName]=@ProductName,[Weight]=@Weight," +
                        "[UnitPrice]=@UnitPrice,[UnitInStock]=@UnitInStock where ProductId=@ProductId";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(dataProvider.CreateParameter(("@ProductId"), 4, pro.ProductId, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter(("@UnitInStock"), 4, pro.UnitInStock, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter(("@ProductName"), 100, pro.ProductName, DbType.String));
                    parameters.Add(dataProvider.CreateParameter(("@Weight"), 10, pro.Weight, DbType.String));
                    parameters.Add(dataProvider.CreateParameter(("@UnitPrice"), 50, pro.UnitPrice, DbType.Int32));
                    dataProvider.Update(SQLUpdate, CommandType.Text, parameters.ToArray());

                }
                else
                {
                    throw new Exception("The product is already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }

        public void Remove(int productID)
        {
            try
            {
                ProductObject pro = GetProductByID(productID);
                if (pro != null)
                {
                    string SQLDelete = "Delete Prodcut where ProductId=@ProductId";
                    var param = dataProvider.CreateParameter("@ProductId", 4, productID, DbType.Int32);
                    dataProvider.Delete(SQLDelete, CommandType.Text, param);
                }
                else
                {
                    throw new Exception("The product does not already exist.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
