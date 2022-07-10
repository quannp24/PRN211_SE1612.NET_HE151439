using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrderRepository : IOrderRepository
    {
        public void DeleteOrder(int OrderID) => OrderDAO.Instance.Remove(OrderID);

        public OrderObject GetOrderByID(int OrderID) => OrderDAO.Instance.GetOrderByID(OrderID);

        public OrderObject GetOrderByMemberID(int memberID) => OrderDAO.Instance.GetOrderByMemberID(memberID);

        public IEnumerable<OrderObject> GetOrders() => OrderDAO.Instance.GetOrderList();

        public void InsertOrder(OrderObject order) => OrderDAO.Instance.AddNew(order);

        public void UpdateOrder(OrderObject order) => OrderDAO.Instance.Update(order);
    }
}
