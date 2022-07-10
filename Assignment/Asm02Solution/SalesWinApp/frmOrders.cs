using BusinessObject;
using DataAccess.Repository;
using SalesWinApp.FormDetail;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesWinApp
{
    public partial class frmOrders : Form
    {
        IOrderRepository orderRepository = new OrderRepository();
        IOrderDetailRepository orderdetailRepository = new OrderDetailRepository();
        BindingSource source;
        public bool AdminOrMember; // admin-true ; member-false
        public int memberID;
        public int orderId,clickMemID;
        public frmOrders(int id, bool AorM)
        {
            InitializeComponent();
            memberID = id;
            AdminOrMember = AorM;
            LoadOrderList();

            dgvOrderList.CellDoubleClick += DgvOrderList_CellDoubleClick;

            dgvOrderList.CellClick += DgvOrderList_CellClick;

        }

        private void DgvOrderList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvOrderList.Rows[e.RowIndex];
            orderId = int.Parse(row.Cells[0].Value.ToString());
            clickMemID= int.Parse(row.Cells[1].Value.ToString());
        }

        private void DgvOrderList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            DataGridViewRow row = dgvOrderList.Rows[e.RowIndex];
            OrderObject order = orderRepository.GetOrderByID(orderId);
            if (int.Parse(row.Cells[1].Value.ToString()) == memberID || memberID == 0)
            {
                OrderDetail frmDetails = new OrderDetail(true, orderRepository, order, memberID);
                if (frmDetails.ShowDialog() == DialogResult.OK)
                {
                    LoadOrderList();
                    source.Position = source.Count - 1;
                }
            }
            else
            {
                MessageBox.Show("Just edit your information", "Error");
            }


        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OrderDetail MemberDetails = new OrderDetail(false, orderRepository, memberID);
            if (MemberDetails.ShowDialog() == DialogResult.OK)
            {
                LoadOrderList();
                source.Position = source.Count - 1;
            }
        }

        private void LoadOrderList()
        {
            var orders = orderRepository.GetOrders();
            try
            {
                source = new BindingSource();
                source.DataSource = orders;
                dgvOrderList.DataSource = source;
                if (orders.Count() == 0)
                {
                    btnDelete.Enabled = false;
                }
                else
                {
                    if (AdminOrMember)
                    {
                        btnDelete.Enabled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load order list");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (clickMemID == memberID || memberID == 0)
            {
                try
                {
                    orderdetailRepository.DeleteOrderDetailByOrderID(orderId);
                    orderRepository.DeleteOrder(orderId);
                    LoadOrderList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Delete a order");
                }
            }
            else
            {
                MessageBox.Show("Just edit your information", "Error");
            }
            
        }
    }
}
