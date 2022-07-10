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
    public class OrderDetailDAO:DBContext
    {
        private static OrderDetailDAO instance = null;
        private static readonly object instanceLock = new object();
        private OrderDetailDAO() { }
        public static OrderDetailDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDetailDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<OrderDetailObject> GetOrderDetailList()
        {
            IDataReader dataReader = null;
            string SQLSelect = "SELECT [OrderId],[ProductId],[UnitPrice],[Quantity],[Discount] FROM [dbo].[OrderDetail]";
            var ods = new List<OrderDetailObject>();
            try
            {
                dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection);
                while (dataReader.Read())
                {
                    ods.Add(new OrderDetailObject
                    {
                        OrderId = dataReader.GetInt32(0),
                        ProductId = dataReader.GetInt32(1),
                        UnitPrice = dataReader.GetInt32(2),
                        Quantity = dataReader.GetInt32(3),
                        Discount = dataReader.GetFloat(4)
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
            return ods;
        }

        public IEnumerable<OrderDetailObject> GetOrderDetailByOrderID(int orderID)
        {
            var ods = new List<OrderDetailObject>();
            IDataReader dataReader = null;
            string SQLSelect = "SELECT [OrderId],[ProductId],[UnitPrice],[Quantity],[Discount] FROM [dbo].[OrderDetail] " +
                " where OrderId=@OrderId";
            try
            {
                var param = dataProvider.CreateParameter("@OrderId", 4, orderID, DbType.Int32);
                dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection, param);
                while (dataReader.Read())
                {
                    ods.Add( new OrderDetailObject
                    {
                        OrderId = dataReader.GetInt32(0),
                        ProductId = dataReader.GetInt32(1),
                        UnitPrice = dataReader.GetDecimal(2),
                        Quantity = dataReader.GetInt32(3),
                        Discount = dataReader.GetDouble(4)
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
            return ods;
        }

        public OrderDetailObject GetODByOrderAndProduct(int orderID,int productID)
        {
            OrderDetailObject od = null;
            IDataReader dataReader = null;
            string SQLSelect = "SELECT [OrderId],[ProductId],[UnitPrice],[Quantity],[Discount] FROM [dbo].[OrderDetail]" +
                " where OrderId=@OrderId and ProductId=@ProductId";
            try
            {
                var parameters = new List<SqlParameter>();
                parameters.Add(dataProvider.CreateParameter("@OrderId", 4, orderID, DbType.Int32));
                parameters.Add(dataProvider.CreateParameter("@ProductId", 4, productID, DbType.Int32));
                dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection, parameters.ToArray());
                if (dataReader.Read())
                {
                    od = new OrderDetailObject
                    {
                        OrderId = dataReader.GetInt32(0),
                        ProductId = dataReader.GetInt32(1),
                        UnitPrice = dataReader.GetDecimal(2),
                        Quantity = dataReader.GetInt32(3),
                        Discount = dataReader.GetDouble(4)
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
            return od;
        }

        public void AddNew(OrderDetailObject orderdetail)
        {
            try
            {
                    string SQLInsert = "INSERT INTO [dbo].[OrderDetail] ([OrderId],[ProductId],[UnitPrice],[Quantity],[Discount]) VALUES " +
                        "(@OrderId,@ProductId,@UnitPrice,@Quantity,@Discount)";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(dataProvider.CreateParameter(("@OrderId"), 4, orderdetail.OrderId, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter(("@ProductId"), 4, orderdetail.ProductId, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter(("@UnitPrice"), 50, orderdetail.UnitPrice, DbType.Decimal));
                    parameters.Add(dataProvider.CreateParameter(("@Quantity"), 4, orderdetail.Quantity, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter(("@Discount"), 4, orderdetail.Discount, DbType.Double));
                    dataProvider.Insert(SQLInsert, CommandType.Text, parameters.ToArray());

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

        public void Update(OrderDetailObject od)
        {
            try
            {
                OrderDetailObject orderdetail = GetODByOrderAndProduct(od.OrderId,od.ProductId);
                if (orderdetail != null)
                {
                    string SQLUpdate = "UPDATE [dbo].[OrderDetail] SET [UnitPrice] = @UnitPrice," +
                        "[Quantity] = @Quantity,[Discount] = @Discount" +
                        " WHERE [OrderId] = @OrderId and [ProductId] = @ProductId";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(dataProvider.CreateParameter(("@UnitPrice"), 50, od.UnitPrice, DbType.Decimal));
                    parameters.Add(dataProvider.CreateParameter(("@Quantity"), 4, od.Quantity, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter(("@Discount"), 5, od.Discount, DbType.Double));
                    parameters.Add(dataProvider.CreateParameter(("@OrderId"), 4, od.OrderId, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter(("@ProductId"), 4, od.ProductId, DbType.Int32));
                    dataProvider.Update(SQLUpdate, CommandType.Text, parameters.ToArray());

                }
                else
                {
                    throw new Exception("The od is already exist.");
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

        public void RemoveByOrderID(int orderID)
        {
            try
            {
                var od = GetOrderDetailByOrderID(orderID);
                if (od != null)
                {
                    string SQLDelete = "Delete OrderDetail where OrderID=@OrderID";
                    var param = dataProvider.CreateParameter("@OrderID", 4, orderID, DbType.Int32);
                    dataProvider.Delete(SQLDelete, CommandType.Text, param);
                }
                else
                {
                    throw new Exception("The order detail does not already exist.");
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
        public void RemoveByOrderAndProduct(int orderID,int productID)
        {
            try
            {
                OrderDetailObject od = GetODByOrderAndProduct(orderID, productID);
                if (od != null)
                {
                    string SQLDelete = "Delete OrderDetail where OrderID=@OrderID and ProductID=@ProductID";
                    var parameters = new List<SqlParameter>();
                    parameters.Add( dataProvider.CreateParameter("@OrderID", 4, orderID, DbType.Int32));
                    parameters.Add( dataProvider.CreateParameter("@ProductID", 4, productID, DbType.Int32));
                    dataProvider.Delete(SQLDelete, CommandType.Text, parameters.ToArray());
                }
                else
                {
                    throw new Exception("The order detail does not already exist.");
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
