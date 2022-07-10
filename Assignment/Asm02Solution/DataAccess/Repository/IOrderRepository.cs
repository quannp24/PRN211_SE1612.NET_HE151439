using BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public interface IOrderRepository
    {
        IEnumerable<OrderObject> GetOrders();
        OrderObject GetOrderByID(int OrderID);
        OrderObject GetOrderByMemberID(int memberID);
        void InsertOrder(OrderObject order);
        void UpdateOrder(OrderObject order);
        void DeleteOrder(int OrderID);
    }
}
