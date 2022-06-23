using BusinessObject;
using DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//author: Phan Quân

namespace MyStoreWinApp
{
    public partial class frmMemberManagement : Form
    {
        IMemberRepository memberRepository = new MemberRepository();
        BindingSource source;
        int id;
        public event EventHandler logout;
        public bool AdminOrMember; // admin-true ; member-false
        public int memberID; //lấy id của người login
        public frmMemberManagement()
        {
            InitializeComponent();
        }

        private void frmMemberManagement_Load(object sender, EventArgs e)
        {
            LoadMemberList();
            dgvMemberList.CellDoubleClick += DgvMemberList_CellDoubleClick;
            if (AdminOrMember)
            {
                
                dgvMemberList.CellClick += DgvMemberList_CellClick;
            }
            else
            {
                btnDelete.Enabled = false;
                btnAdd.Enabled = false;

            }
            
        }

        private void DgvMemberList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            DataGridViewRow row = dgvMemberList.Rows[e.RowIndex];
            MemberObject mem = null;
            if (int.Parse(row.Cells[0].Value.ToString()) == memberID || memberID==0)
            {

                if (row.Cells[2].Value != null)
                {
                    mem = new MemberObject
                    {
                        MemberID = int.Parse(row.Cells[0].Value.ToString()),
                        MemberName = row.Cells[1].Value.ToString(),
                        Email = row.Cells[2].Value.ToString(),
                        Password = row.Cells[3].Value.ToString(),
                        City = row.Cells[4].Value.ToString(),
                        Country = row.Cells[5].Value.ToString(),
                    };



                    frmMemberDetail frmMemberDetails = new frmMemberDetail
                    {
                        Text = "Update member",
                        InsertOrUpdate = true,
                        MemInfo = mem,
                        MemberRepository = memberRepository
                    };
                    if (frmMemberDetails.ShowDialog() == DialogResult.OK)
                    {
                        LoadMemberList();
                        source.Position = source.Count - 1;
                    }
                }
                else
                {
                    MessageBox.Show("Member does not exists.", "Error");
                }
            }
            else
            {
                MessageBox.Show("Just edit your information.", "Error");
            }
        }


        private void LoadMemberList()
        {
            var mems = memberRepository.GetMemberObjects();
            try
            {
                source = new BindingSource();
                source.DataSource = mems;
                dgvMemberList.DataSource = source;
                if (mems.Count() == 0)
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
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Load member list");
            }
        }

        private void LoadMemberListByName()
        {
            var mems = memberRepository.DescSortListByName();
           

            try
            {
                source = new BindingSource();
                source.DataSource = mems;
                dgvMemberList.DataSource = source;

                if (mems.Count() == 0)
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
                MessageBox.Show(ex.Message, "Load member list");
            }
        }

        private void LoadMemberListByCity()
        {
            var mems = memberRepository.DescSortListByCity();

            try
            {
                source = new BindingSource();
                source.DataSource = mems;
                dgvMemberList.DataSource = source;
                if (mems.Count() == 0)
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
                MessageBox.Show(ex.Message, "Load member list");
            }
        }

        private void LoadMemberListByCountry()
        {
            var mems = memberRepository.DescSortListByCountry();

            try
            {
                source = new BindingSource();
                source.DataSource = mems;
                dgvMemberList.DataSource = source;
                if (mems.Count() == 0)
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
                MessageBox.Show(ex.Message, "Load member list");
            }
        }
        private void btnClose_Click(object sender, EventArgs e) => Application.Exit();

        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmMemberDetail frmMemberDetails = new frmMemberDetail
            {
                Text = "Add member",
                InsertOrUpdate = false,
                MemberRepository = memberRepository
            };
            if (frmMemberDetails.ShowDialog() == DialogResult.OK)
            {
                LoadMemberList();
                source.Position = source.Count - 1;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

                try
                {
                    //int memID = int.Parse(row.Cells[0].Value.ToString());
                    memberRepository.DeleteMember(id);
                    LoadMemberList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Delete a member");
                }
            

        }

        private void DgvMemberList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvMemberList.Rows[e.RowIndex];
            id = int.Parse(row.Cells[0].Value.ToString());
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            logout(this, new EventArgs());
        }



        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string filter = cbFilter.SelectedItem.ToString().Trim();
            if (filter == "Name Z-A")
            {
                LoadMemberListByName();
            }
            if (filter == "City")
            {
                LoadMemberListByCity();
            }
            if (filter == "Country")
            {
                LoadMemberListByCountry();
            }


        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string search = txtSearch.Text;
            int id;
            if(int.TryParse(search,out id))
            {
                LoadMemberSearchByID(id);
            }
            else
            {
                LoadMemberSearchByName(search);
            }
        }

        private void LoadMemberSearchByID(int memberID)
        {
            var mems = memberRepository.GetMembersByID(memberID);

            try
            {
                source = new BindingSource();
                source.DataSource = mems;
                dgvMemberList.DataSource = source;
                if (mems.Count() == 0)
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
                MessageBox.Show(ex.Message, "Load member list");
            }
        }

        private void LoadMemberSearchByName(string memberName)
        {
            var mems = memberRepository.GetMembersByName(memberName);

            try
            {
                source = new BindingSource();
                source.DataSource = mems;
                dgvMemberList.DataSource = source;
                if (mems.Count() == 0)
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
                MessageBox.Show(ex.Message, "Load member list");
            }
        }

        private void dgvMemberList_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                if (e.Value != null)
                {
                    e.Value = new string('*', e.Value.ToString().Length);
                } 
            }
        }
    }
}
