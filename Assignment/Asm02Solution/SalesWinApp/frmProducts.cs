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
    public partial class frmProducts : Form
    {
        IProductRepository productRepository = new ProductRepository();
        BindingSource source;
        int productId;
        public bool AdminOrMember; // admin-true ; member-false
        public int memberID; //lấy id của người login
        public frmProducts(int accId,bool AorM)
        {
            memberID = accId;
            AdminOrMember = AorM;
            InitializeComponent();
            LoadProductList();
            if (AdminOrMember)
            {
                dgvProductList.CellDoubleClick += DgvProductList_CellDoubleClick;
                dgvProductList.CellClick += DgvProductList_CellClick;
            }
            else
            {
                btnDelete.Enabled = false;
                btnAdd.Enabled = false;

            }
        }

        private void DgvProductList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvProductList.Rows[e.RowIndex];
            productId = int.Parse(row.Cells[0].Value.ToString());
        }

        private void DgvProductList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            DataGridViewRow row = dgvProductList.Rows[e.RowIndex];
            ProductObject pro = null;
            if (memberID == 0)
            {

                if (row.Cells[2].Value != null)
                {
                    pro = new ProductObject
                    {
                        ProductId = int.Parse(row.Cells[0].Value.ToString()),
                        CategoryId = int.Parse(row.Cells[1].Value.ToString()),
                        ProductName = row.Cells[2].Value.ToString(),
                        Weight = row.Cells[3].Value.ToString(),
                        UnitPrice = Decimal.Parse( row.Cells[4].Value.ToString()),
                        UnitInStock = int.Parse( row.Cells[5].Value.ToString())
                    };



                    ProductDetail frmMemberDetails = new ProductDetail(true, pro, productRepository);
                    if (frmMemberDetails.ShowDialog() == DialogResult.OK)
                    {
                        LoadProductList();
                        source.Position = source.Count - 1;
                    }
                }
                else
                {
                    MessageBox.Show("Product does not exists.", "Error");
                }
            }

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            ProductDetail proDetails = new ProductDetail(false, productRepository);
            if (proDetails.ShowDialog() == DialogResult.OK)
            {
                LoadProductList();
                source.Position = source.Count - 1;
            }
        }

        private void LoadProductList()
        {
            var pro = productRepository.GetProducts();
            try
            {
                source = new BindingSource();
                source.DataSource = pro;
                dgvProductList.DataSource = source;
                if (pro.Count() == 0)
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
                MessageBox.Show(ex.Message, "Load product list");
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                productRepository.DeleteProduct(productId);
                LoadProductList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Delete a member");
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string search = txtSearch.Text;
            int id;
            if (int.TryParse(search, out id))
            {
                LoadProductSearchByID(id);
            }
            else
            {
                LoadProductSearchByName(search);
            }
        }

        private void LoadProductSearchByID(int proID)
        {
            var pro = productRepository.GetProductByID(proID);

            try
            {
                source = new BindingSource();
                source.DataSource = pro;
                dgvProductList.DataSource = source;
                if (pro==null)
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
                MessageBox.Show(ex.Message, "Load product list");
            }
        }

        private void LoadProductSearchByName(string productName)
        {
            var pros = productRepository.GetProductByName(productName);

            try
            {
                source = new BindingSource();
                source.DataSource = pros;
                dgvProductList.DataSource = source;
                if (pros.Count() == 0)
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
                MessageBox.Show(ex.Message, "Load product list");
            }
        }
    }
}
