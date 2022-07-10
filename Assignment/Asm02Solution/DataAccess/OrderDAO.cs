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
    public class OrderDAO:DBContext
    {
        private static OrderDAO instance = null;
        private static readonly object instanceLock = new object();
        private OrderDAO() { }
        public static OrderDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new OrderDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<OrderObject> GetOrderList()
        {
            IDataReader dataReader = null;
            string SQLSelect = "SELECT [OrderId],[MemberId],[OrderDate],[RequiredDate],[ShippedDate],[Freight] FROM [dbo].[Order]";
            var orders = new List<OrderObject>();
            try
            {
                dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection);
                while (dataReader.Read())
                {
                    orders.Add(new OrderObject
                    {
                        OrderId = dataReader.GetInt32(0),
                        MemberId = dataReader.GetInt32(1),
                        OrderDate = dataReader.GetDateTime(2),
                        RequiredDate = dataReader.GetDateTime(3),
                        ShippedDate = dataReader.GetDateTime(4),
                        Freight = dataReader.GetDecimal(5)
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
            return orders;
        }

        public OrderObject GetOrderByID(int orderID)
        {
            OrderObject order = null;
            IDataReader dataReader = null;
            string SQLSelect = "SELECT [OrderId],[MemberId],[OrderDate],[RequiredDate],[ShippedDate],[Freight] FROM [dbo].[Order]" +
                " where OrderId=@OrderId";
            try
            {
                var param = dataProvider.CreateParameter("@OrderId", 4, orderID, DbType.Int32);
                dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection, param);
                if (dataReader.Read())
                {
                    order = new OrderObject
                    {
                        OrderId = dataReader.GetInt32(0),
                        MemberId = dataReader.GetInt32(1),
                        OrderDate = dataReader.GetDateTime(2),
                        RequiredDate = dataReader.GetDateTime(3),
                        ShippedDate = dataReader.GetDateTime(4),
                        Freight = dataReader.GetDecimal(5)
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
            return order;
        }

        public OrderObject GetOrderByMemberID(int memID)
        {
            OrderObject order = null;
            IDataReader dataReader = null;
            string SQLSelect = "SELECT top(1) [OrderId],[MemberId],[OrderDate],[RequiredDate],[ShippedDate],[Freight] FROM [dbo].[Order]" +
                " where MemberId=@MemberId order by OrderId desc";
            try
            {
                var param = dataProvider.CreateParameter("@MemberId", 4, memID, DbType.Int32);
                dataReader = dataProvider.GetDataReader(SQLSelect, CommandType.Text, out connection, param);
                if (dataReader.Read())
                {
                    order = new OrderObject
                    {
                        OrderId = dataReader.GetInt32(0),
                        MemberId = dataReader.GetInt32(1),
                        OrderDate = dataReader.GetDateTime(2),
                        RequiredDate = dataReader.GetDateTime(3),
                        ShippedDate = dataReader.GetDateTime(4),
                        Freight = dataReader.GetDecimal(5)
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
            return order;
        }

        public void AddNew(OrderObject order)
        {
            try
            {
                OrderObject test = GetOrderByID(order.OrderId);
                if (test == null)
                {
                    string SQLInsert = "INSERT INTO [dbo].[Order] ([MemberId],[OrderDate],[RequiredDate],[ShippedDate],[Freight]) VALUES " +
                        "(@MemberId,@OrderDate,@RequiredDate,@ShippedDate,@Freight)";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(dataProvider.CreateParameter(("@MemberId"), 4, order.MemberId, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter(("@OrderDate"), 50, order.OrderDate, DbType.DateTime));
                    parameters.Add(dataProvider.CreateParameter(("@RequiredDate"), 50, order.RequiredDate, DbType.DateTime));
                    parameters.Add(dataProvider.CreateParameter(("@ShippedDate"), 50, order.ShippedDate, DbType.DateTime));
                    parameters.Add(dataProvider.CreateParameter(("@Freight"), 10, order.Freight, DbType.Decimal));
                    dataProvider.Insert(SQLInsert, CommandType.Text, parameters.ToArray());

                }
                else
                {
                    throw new Exception("The order is already exist.");
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

        public void Update(OrderObject order)
        {
            try
            {
                OrderObject o = GetOrderByID(order.OrderId);
                if (o != null)
                {
                    string SQLUpdate = "UPDATE [dbo].[Order] SET [MemberId] = @MemberId ,[OrderDate] = @OrderDate,[RequiredDate] = @RequiredDate," +
                        "[ShippedDate] = @ShippedDate,[Freight] = @Freight where OrderId=@OrderId";
                    var parameters = new List<SqlParameter>();
                    parameters.Add(dataProvider.CreateParameter(("@MemberId"), 4, order.MemberId, DbType.Int32));
                    parameters.Add(dataProvider.CreateParameter(("@OrderDate"), 70, order.OrderDate, DbType.DateTime));
                    parameters.Add(dataProvider.CreateParameter(("@RequiredDate"), 70, order.RequiredDate, DbType.DateTime));
                    parameters.Add(dataProvider.CreateParameter(("@ShippedDate"), 70, order.ShippedDate, DbType.DateTime));
                    parameters.Add(dataProvider.CreateParameter(("@Freight"), 70, order.Freight, DbType.Decimal));
                    parameters.Add(dataProvider.CreateParameter(("@OrderId"), 4, order.OrderId, DbType.Int32));
                    dataProvider.Update(SQLUpdate, CommandType.Text, parameters.ToArray());

                }
                else
                {
                    throw new Exception("The order is already exist.");
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

        public void Remove(int orderID)
        {
            try
            {
                OrderObject order = GetOrderByID(orderID);
                if (order != null)
                {
                    string SQLDelete = "Delete [Order] where OrderId=@OrderId";
                    var param = dataProvider.CreateParameter("@OrderId", 4, orderID, DbType.Int32);
                    dataProvider.Delete(SQLDelete, CommandType.Text, param);
                }
                else
                {
                    throw new Exception("The order does not already exist.");
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
