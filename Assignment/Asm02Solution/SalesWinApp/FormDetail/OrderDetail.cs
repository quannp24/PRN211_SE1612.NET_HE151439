using BusinessObject;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesWinApp.FormDetail
{
    public partial class OrderDetail : Form
    {
        IOrderDetailRepository orderDetailRepository = new OrderDetailRepository();
        IProductRepository productRepository = new ProductRepository();
        BindingSource source;
        public IOrderRepository orderRepository { get; set; }
        public bool InsertOrUpdate { get; set; } //false- insert , true - update
        public OrderObject OrderInfo { get; set; }
        private int orderid, productid, memID;
        public OrderDetail(bool IorU, IOrderRepository order, OrderObject ob, int memberID)
        {
            InsertOrUpdate = IorU;
            orderRepository = order;
            OrderInfo = ob;

            memID = memberID;
            InitializeComponent();
            dgvOrderDetail.CellClick += DgvOrderList_CellClick;
            dgvOrderDetail.CellDoubleClick += DgvOrderDetail_CellDoubleClick;
            if (InsertOrUpdate == true)
            {
                if (memID != 0)
                {
                    if (OrderInfo.RequiredDate.ToString().Length > 1)
                    {
                        timeRequiredDate.Value = OrderInfo.RequiredDate.Date;
                    }
                    if (OrderInfo.ShippedDate.ToString().Length > 1)
                    {
                        timeShippedDate.Value = OrderInfo.ShippedDate.Date;
                    }
                    timeOrderDate.Value = OrderInfo.OrderDate.Date;
                    txtMemberID.Enabled = false;
                    txtFreight.Text = OrderInfo.Freight.ToString();
                    txtMemberID.Text = OrderInfo.MemberId.ToString();
                    LoadOrderDetailList();
                }
                else
                {
                    if (OrderInfo.RequiredDate.ToString().Length > 1)
                    {
                        timeRequiredDate.Value = OrderInfo.RequiredDate.Date;
                    }
                    if (OrderInfo.ShippedDate.ToString().Length > 1)
                    {
                        timeShippedDate.Value = OrderInfo.ShippedDate.Date;
                    }
                    timeOrderDate.Value = OrderInfo.OrderDate.Date;
                    txtFreight.Text = OrderInfo.Freight.ToString();
                    txtMemberID.Text = OrderInfo.MemberId.ToString();
                    LoadOrderDetailList();
                }

            }

        }

        public OrderDetail(bool IorU, IOrderRepository order, int memberID)  //insert
        {

            InitializeComponent();
            InsertOrUpdate = IorU;
            memID = memberID;
            orderRepository = order;
            dgvOrderDetail.CellClick += DgvOrderList_CellClick;
            dgvOrderDetail.CellDoubleClick += DgvOrderDetail_CellDoubleClick;
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void panelTitle_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnClose_Click(object sender, EventArgs e) => Close();

        private void DgvOrderList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvOrderDetail.Rows[e.RowIndex];
            productid = int.Parse(row.Cells[1].Value.ToString());
        }

        private void DgvOrderDetail_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            DataGridViewRow row = dgvOrderDetail.Rows[e.RowIndex];
            OrderDetailObject order = orderDetailRepository.GetODByProductAndOrderID(OrderInfo.OrderId, productid);

            var product = productRepository.GetProductByID(order.ProductId);

            lbProductName.Text = product.ProductName;
            if (row.Cells[1].Value.ToString() != null)
            {
                if (product != null)
                {
                    lbProductName.Text = product.ProductName;
                }
                txtProductID.Text = order.ProductId.ToString();
                txtQuantity.Text = order.Quantity.ToString();
                txtUnitPrice.Text = order.UnitPrice.ToString();
                txtDiscount.Text = order.Discount.ToString();
            }



        }

        private void LoadOrderDetailList()
        {
            var orders = orderDetailRepository.GetOrderDetailByOrderID(OrderInfo.OrderId);
            try
            {
                source = new BindingSource();
                source.DataSource = orders;
                dgvOrderDetail.DataSource = source;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load order list");
            }
        }

        private void btnAddOrder_Click(object sender, EventArgs e)
        {
            try
            {
                if (InsertOrUpdate) //update
                {
                    var order = new OrderDetailObject
                    {
                        ProductId = int.Parse(txtProductID.Text),
                        UnitPrice = Decimal.Parse(txtUnitPrice.Text),
                        Quantity = int.Parse(txtQuantity.Text),
                        Discount = Double.Parse(txtDiscount.Text),
                        OrderId = OrderInfo.OrderId
                    };
                    var product = orderDetailRepository.GetODByProductAndOrderID(order.OrderId, order.ProductId);
                    if (product == null)
                        orderDetailRepository.InsertOrderDetail(order);
                    else
                        orderDetailRepository.UpdateOrderDetail(order);
                    LoadOrderDetailList();
                }
                else  //insert
                {
                    if (OrderInfo == null)
                    {
                        DateTime dateTime = DateTime.UtcNow.Date;
                        var order = new OrderObject
                        {
                            MemberId = int.Parse(txtMemberID.Text),
                            OrderDate = dateTime,
                            ShippedDate = dateTime,
                            RequiredDate = dateTime,
                            Freight = 0

                        };
                        orderRepository.InsertOrder(order);
                        order = orderRepository.GetOrderByMemberID(int.Parse(txtMemberID.Text));  // lay order vua insert

                        OrderInfo = order;
                    }
                    var orderdetail = new OrderDetailObject
                    {
                        ProductId = int.Parse(txtProductID.Text),
                        UnitPrice = Decimal.Parse(txtUnitPrice.Text),
                        Quantity = int.Parse(txtQuantity.Text),
                        Discount = Double.Parse(txtDiscount.Text),
                        OrderId = OrderInfo.OrderId
                    };
                    var product = orderDetailRepository.GetODByProductAndOrderID(orderdetail.OrderId, orderdetail.ProductId);
                    if (product == null)
                        orderDetailRepository.InsertOrderDetail(orderdetail);
                    else
                        orderDetailRepository.UpdateOrderDetail(orderdetail);
                    LoadAllForm();

                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Add a new order detail");
            }


        }

        private void LoadAllForm()
        {
            txtMemberID.Text = OrderInfo.MemberId.ToString();
            txtMemberID.Enabled = false;
            timeOrderDate.Value = OrderInfo.OrderDate.Date;
            timeRequiredDate.Value = OrderInfo.RequiredDate.Date;
            timeShippedDate.Value = OrderInfo.ShippedDate.Date;
            txtFreight.Text = OrderInfo.Freight.ToString();
            LoadOrderDetailList();

        }



        private void txtProductID_KeyDown(object sender, KeyEventArgs e)
        {
            if (txtProductID.Text.Length > 0)
            {
                int productId = int.Parse(txtProductID.Text);
                var product = productRepository.GetProductByID(productId);
                if (product != null)
                {
                    lbProductName.Text = product.ProductName;
                    txtUnitPrice.Enabled = true;
                    txtQuantity.Enabled = true;
                    txtDiscount.Enabled = true;
                    btnAddOrder.Enabled = true;
                }
            }
            else
            {
                txtUnitPrice.Enabled = false;
                txtQuantity.Enabled = false;
                txtDiscount.Enabled = false;
                btnAddOrder.Enabled = false;
                lbProductName.Text = string.Empty;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                //int memID = int.Parse(row.Cells[0].Value.ToString());
                orderDetailRepository.DeleteODByProductAndOrder(OrderInfo.OrderId, productid);
                LoadOrderDetailList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete a order detail");
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (memID != 0)  // member
            {
                if (InsertOrUpdate)
                {
                    OrderObject od = new OrderObject
                    {
                        OrderDate = timeOrderDate.Value,
                        RequiredDate = timeRequiredDate.Value,
                        ShippedDate = timeRequiredDate.Value,
                        Freight = Decimal.Parse(txtFreight.Text),
                        MemberId = memID,
                        OrderId = OrderInfo.OrderId

                    };
                    orderRepository.UpdateOrder(od);

                }
            }
            else  //admin
            {
                OrderObject od = new OrderObject
                {
                    OrderDate = timeOrderDate.Value,
                    RequiredDate = timeRequiredDate.Value,
                    ShippedDate = timeRequiredDate.Value,
                    Freight = Decimal.Parse(txtFreight.Text),
                    MemberId = int.Parse(txtMemberID.Text),
                    OrderId = OrderInfo.OrderId

                };
                orderRepository.UpdateOrder(od);
            }

        }

        private void txtProductID_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtProductID.Text.Length > 0)
            {
                int productId = int.Parse(txtProductID.Text);
                var product = productRepository.GetProductByID(productId);
                if (product != null)
                {
                    lbProductName.Text = product.ProductName;
                    txtUnitPrice.Enabled = true;
                    txtQuantity.Enabled = true;
                    txtDiscount.Enabled = true;
                    btnAddOrder.Enabled = true;
                }
            }
            else
            {
                txtUnitPrice.Enabled = false;
                txtQuantity.Enabled = false;
                txtDiscount.Enabled = false;
                btnAddOrder.Enabled = false;
                lbProductName.Text = string.Empty;
            }
        }
    }
}
