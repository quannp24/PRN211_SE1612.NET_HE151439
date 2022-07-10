using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public void DeleteODByProductAndOrder(int OrderID, int productID) => OrderDetailDAO.Instance.RemoveByOrderAndProduct(OrderID, productID);

        public void DeleteOrderDetailByOrderID(int OrderID) => OrderDetailDAO.Instance.RemoveByOrderID(OrderID);

        public OrderDetailObject GetODByProductAndOrderID(int productID, int orderID) => OrderDetailDAO.Instance.GetODByOrderAndProduct(productID, orderID);

        public IEnumerable<OrderDetailObject> GetOrderDetailByOrderID(int OrderID) => OrderDetailDAO.Instance.GetOrderDetailByOrderID(OrderID);

        public IEnumerable<OrderDetailObject> GetOrderDetails() => OrderDetailDAO.Instance.GetOrderDetailList();

        public void InsertOrderDetail(OrderDetailObject orderDetail) => OrderDetailDAO.Instance.AddNew(orderDetail);

        public void UpdateOrderDetail(OrderDetailObject orderDetail) => OrderDetailDAO.Instance.Update(orderDetail);
    }
}
