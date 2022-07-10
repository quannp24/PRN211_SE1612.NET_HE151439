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

namespace SalesWinApp
{
    public partial class frmMain : Form
    {
        public bool AdminOrMember; // admin-true ; member-false
        public int accID; //lấy id của người login
        public event EventHandler logout;
        private Button currentButton;
        private Form activateForm;


        public frmMain(int id,bool AorM)
        {
            InitializeComponent();
            accID = id;
            AdminOrMember = AorM;
            this.Text = string.Empty;
            this.ControlBox = false;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            Form childForm = new frmMembers(accID, AdminOrMember);
            activateForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelDeskstop.Controls.Add(childForm);
            this.panelDeskstop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lbTitle.Text = "Member Management";

        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);



        private void ActivateButton(object btnSender,Color color)
        {
            if (btnSender != null)
            {
                if(currentButton != (Button)btnSender)
                {
                    DisableButton();
                    currentButton = (Button)btnSender;
                    currentButton.BackColor = color;
                    currentButton.ForeColor = Color.White;
                    currentButton.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                    sidebar.BackColor = color;
                    panelLogo.BackColor = color;
                    btnMax.BackColor = color;
                    btnMin.BackColor = color;
                    btnExit.BackColor = color;
                    lbTitle.BackColor = color;
                }
            }
        }

        private void DisableButton()
        {
            foreach(Control previousBtn in panelMenu.Controls)
            {
                if (previousBtn.GetType() == typeof(Button))
                {
                   
                    previousBtn.BackColor = Color.FromArgb(240,144,38);
                    previousBtn.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
                    
                }
            }
        }

        private void OpenChildForm(Form childForm, object btnSender,Color color,string text)
        {
            if (activateForm != null)
            {
                activateForm.Close();
            }
            ActivateButton(btnSender,color);
            activateForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            this.panelDeskstop.Controls.Add(childForm);
            this.panelDeskstop.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
            lbTitle.Text = text;
        }

        private void btnMember_Click(object sender, EventArgs e)
        {
            string text = "Member Management";
            Color color = ColorTranslator.FromHtml("#FFC080");
            OpenChildForm(new frmMembers(accID, AdminOrMember),sender,color,text);
        }

        private void btnOrder_Click(object sender, EventArgs e)
        {
            string text = "Order Management";
            Color color = ColorTranslator.FromHtml("#4772A2");
            OpenChildForm(new frmOrders(accID, AdminOrMember), sender, color,text);
        }

        private void btnProduct_Click(object sender, EventArgs e)
        {
            string text = "Product Management";
            Color color = ColorTranslator.FromHtml("#84BF7E");
            OpenChildForm(new frmProducts(accID,AdminOrMember), sender, color,text);
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            Color color = ColorTranslator.FromHtml("#FFC080");
            ActivateButton(sender,color);
            logout(this, new EventArgs());
        }

        private void sidebar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMax_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
                this.WindowState = FormWindowState.Maximized;
            else
                this.WindowState = FormWindowState.Normal;
        }

        private void btnExit_Click(object sender, EventArgs e) => Application.Exit();

        private void frmMain_Load(object sender, EventArgs e)
        {
            
        }
    }
}
