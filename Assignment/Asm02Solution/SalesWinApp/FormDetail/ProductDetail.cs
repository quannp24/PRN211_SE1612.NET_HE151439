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
    public partial class ProductDetail : Form
    {
        public IProductRepository ProductRepository { get; set; }
        public bool InsertOrUpdate { get; set; } //false- insert , true - update
        public ProductObject ProInfo { get; set; }
        public ProductDetail(bool IorU,ProductObject pro,IProductRepository proRepository)
        {
            InsertOrUpdate = IorU;
            ProInfo = pro;
            ProductRepository = proRepository;
            InitializeComponent();
        }

        public ProductDetail(bool IorU,IProductRepository proRepository)
        {
            InsertOrUpdate = IorU;
            ProductRepository = proRepository;
            InitializeComponent();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private void flowLayoutPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var pro = new ProductObject
                {
                   
                    CategoryId =int.Parse( txtCategoryID.Text),
                    ProductName = txtProductName.Text,
                    UnitInStock = int.Parse(txtUnitInStock.Text),
                    UnitPrice = int.Parse(txtUnitPrice.Text),
                    Weight = txtWeight.Text
                };
                if (InsertOrUpdate == false)
                {
                    ProductRepository.InsertProduct(pro);
                }
                else
                {
                    ProductRepository.UpdateProduct(pro);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, InsertOrUpdate == false ? "Add a new product" : "Update a product");
            }
        }

        private void btnClose_Click(object sender, EventArgs e) => Close();

        private void ProductDetail_Load(object sender, EventArgs e)
        {
            if (InsertOrUpdate == true)
            {
                txtProductID.Text = ProInfo.ProductId.ToString();
                txtProductName.Text = ProInfo.ProductName;
                txtCategoryID.Text = ProInfo.CategoryId.ToString();
                txtWeight.Text = ProInfo.Weight;
                txtUnitPrice.Text = ProInfo.UnitPrice.ToString();
                txtUnitInStock.Text = ProInfo.UnitInStock.ToString();

            }
        }
    }
}
