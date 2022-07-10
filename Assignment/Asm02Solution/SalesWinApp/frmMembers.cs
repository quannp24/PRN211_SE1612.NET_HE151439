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
    public partial class frmMembers : Form
    {

        IMemberRepository memberRepository = new MemberRepository();
        BindingSource source;
        int id;
        public bool AdminOrMember; // admin-true ; member-false
        public int memberID; //lấy id của người login
        public frmMembers(int id,bool AorM)
        {
            memberID = id;
            AdminOrMember = AorM;
            InitializeComponent();
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



        private void DgvMemberList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = dgvMemberList.Rows[e.RowIndex];
            id = int.Parse(row.Cells[0].Value.ToString());
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            MemberDetail MemberDetails = new MemberDetail
            {
                Text = "Add member",
                InsertOrUpdate = false,
                MemberRepository = memberRepository
            };
            if (MemberDetails.ShowDialog() == DialogResult.OK)
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

        private void cbFilter_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string search = txtSearch.Text;
            int id;
            if (search.Length > 0)
            {
                if (int.TryParse(search, out id))
                {
                    lbError.Text = string.Empty;
                    LoadMemberSearchByID(id);
                }
                else
                {
                    lbError.Text = "Type search must number";
                }
            }
        }



        private void LoadMemberSearchByID(int memberID)
        {
            var mems = memberRepository.GetMemberByID(memberID);

            try
            {
                source = new BindingSource();
                source.DataSource = mems;
                dgvMemberList.DataSource = source;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Load member list");
            }
        }

        private void DgvMemberList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            DataGridViewRow row = dgvMemberList.Rows[e.RowIndex];
            MemberObject mem = null;
            if (int.Parse(row.Cells[0].Value.ToString()) == memberID || memberID == 0)
            {

                if (row.Cells[2].Value != null)
                {
                    mem = new MemberObject
                    {
                        MemberID = int.Parse(row.Cells[0].Value.ToString()),
                        Email = row.Cells[1].Value.ToString(),
                        Company = row.Cells[2].Value.ToString(),
                        City = row.Cells[3].Value.ToString(),
                        Country = row.Cells[4].Value.ToString(),
                        Password = row.Cells[5].Value.ToString(),
                    };



                    MemberDetail frmMemberDetails = new MemberDetail
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
            var mems = memberRepository.GetMembers();
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
            if (e.ColumnIndex == 5)
            {
                if (e.Value != null)
                {
                    e.Value = new string('*', e.Value.ToString().Length);
                }
            }
        }

        private void frmMembers_Load(object sender, EventArgs e)
        {
            
        }
    }
}
