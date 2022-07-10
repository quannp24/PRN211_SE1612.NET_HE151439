using BusinessObject;
using DataAccess;
using DataAccess.Repository;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SalesWinApp
{
    public partial class frmLogin : Form
    {
        

        //public IMemberRepository memberRepository { get; set; }
        IMemberRepository memberRepository = new MemberRepository();
        BindingSource source;
        public bool AorM; //admin-true ; member-false
        public frmLogin()
        {
            InitializeComponent();
            this.Text = string.Empty;
            this.ControlBox = false;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
        }


        private void btnExit_Click(object sender, EventArgs e) => Application.Exit();

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd,int wMsg,int wParam,int lParam);

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (CheckLogin(txtEmail.Text, txtPassword.Text))
            {
                if (AorM)
                {
                    frmMain f = new frmMain(0, true);
                    f.Show();
                 
                    this.Hide();
                    f.logout += F_logout;
                }
                else
                {
                    MemberObject mem = GetMemberByAccount(); //lấy thông tin tài khoản login bằng email và password
                    frmMain f = new frmMain(mem.MemberID, false);
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
            (sender as frmMain).Close();
            txtEmail.Text = string.Empty;
            txtPassword.Text = string.Empty;
            this.Show();
        }

        private MemberObject GetMemberByAccount()
        {
            MemberObject mem = null;
            try
            {
                string email = txtEmail.Text.Trim();
                string password = txtPassword.Text;
                mem = memberRepository.GetMemberByAccount(email, password);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Get member");
            }
            return mem;
        }



        bool CheckLogin(string email, string password)
        {
            var mems = memberRepository.GetMembers();
            List<MemberObject> listMember = mems.ToList();
            Admin admin = ReadAccountAdmin();
            foreach (MemberObject mem in listMember)
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

        public Admin ReadAccountAdmin()  //đọc admin account từ json
        {
            string email;
            string password;
            IConfiguration config = new ConfigurationBuilder().
                SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", true, true).Build();
            email = config["AdminAccount:Email"];
            password = config["AdminAccount:Password"];

            Admin admin = new Admin
            {
                Email = email,
                Password = password
            };
            return admin;
        }

        public class Admin
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }


    }
}
