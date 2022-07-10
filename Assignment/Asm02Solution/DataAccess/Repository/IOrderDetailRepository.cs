using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IOrderDetailRepository
    {
        IEnumerable<OrderDetailObject> GetOrderDetails();
        IEnumerable<OrderDetailObject> GetOrderDetailByOrderID(int OrderID);
        OrderDetailObject GetODByProductAndOrderID(int productID, int orderID);
        void InsertOrderDetail(OrderDetailObject orderDetail);
        void UpdateOrderDetail(OrderDetailObject orderDetail);
        void DeleteOrderDetailByOrderID(int OrderID);
        void DeleteODByProductAndOrder(int OrderID,int productID);

    }
}
