using BusinessObject;
using DataAccess;
using DataAccess.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

//author: Phan Quân

namespace MyStoreWinApp
{
    public partial class frmLogin : Form
    {
        List<MemberObject> listAccount = MemberDAO.Instance.GetMemberList;

        //public IMemberRepository memberRepository { get; set; }
        IMemberRepository memberRepository = new MemberRepository();
        public bool AorM; //admin-true ; member-false
        
        public frmLogin()
        {
            InitializeComponent();
        }

     

        //read account admin from file .json
        public Admin ReadAccountAdmin()
        {
            string _path = @"C:\Users\Quan\FU\PRN211\Ass01Solution\appsettings.json";
            FileStream fs = new FileStream(_path, FileMode.Open);

            StreamReader rd = new StreamReader(fs, Encoding.UTF8);
            string json = rd.ReadToEnd();
            Admin adminFile = JsonConvert.DeserializeObject<Admin>(json);
            Admin admin = new Admin
            {
                Email = adminFile.Email,
                Password = adminFile.Password
            };
            rd.Close();
            return admin;
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            
            if (CheckLogin(txtEmail.Text, txtPassword.Text))
            {
                if (AorM)
                {
                    frmMemberManagement f = new frmMemberManagement()
                    {
                        Text = "Member Management",
                        AdminOrMember = true,
                        memberID = 0

                    };
                    f.Show();
                    this.Hide();
                    f.logout += F_logout;
                }
                else
                {
                    MemberObject mem = GetMemberByAccount(); //lấy thông tin tài khoản login bằng email và password
                    frmMemberManagement f = new frmMemberManagement()
                    {
                        Text = "Member Management",
                        AdminOrMember = false,
                        memberID=mem.MemberID
                    };
                    f.Show();
                    this.Hide();
                    f.logout += F_logout;
                }
            }
            else
            {
                MessageBox.Show("Email or password wrong!", "Error");
            }
        }

        private void F_logout(object sender, EventArgs e)
        {
            (sender as frmMemberManagement).Close();
            txtEmail.Text = string.Empty;
            txtPassword.Text = string.Empty;
            this.Show();
        }

        private MemberObject GetMemberByAccount()
        {
            MemberObject mem = null;
            try
            {
                string email=txtEmail.Text.Trim();
                string password=txtPassword.Text;
                mem = memberRepository.GetMemberByAccount(email, password);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get member");
            }
            return mem;
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        bool CheckLogin(string email,string password)
        {

            Admin admin = ReadAccountAdmin();
            foreach (MemberObject mem in listAccount)
            {
                if (mem.Email == email && mem.Password == password)
                {
                    AorM = false;
                    return true;
                }
                else
                {
                    if (email == admin.Email && password == admin.Password)
                    {
                        AorM = true;
                        return true;
                    }
                }
                
            }
            return false;
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            
        }

        public class Admin
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }
    }
}
