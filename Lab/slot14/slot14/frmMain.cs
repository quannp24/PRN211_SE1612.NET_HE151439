using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace slot14
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
        int counter = 1;

        private void frmMain_Load(object sender, EventArgs e)
        {
            CreateMyMainMenu();
        }
        public void CreateMyMainMenu()
        {
            MenuStrip mainMenu = new MenuStrip();
            this.Controls.Add(mainMenu);
            this.MainMenuStrip = mainMenu;
            ToolStripMenuItem menuFile = new ToolStripMenuItem("&File");
            ToolStripMenuItem mnuOpen = new ToolStripMenuItem("&Open");
            ToolStripSeparator separetor = new ToolStripSeparator();
            ToolStripMenuItem mnuExit = new ToolStripMenuItem("&Exit");
            ToolStripMenuItem menuWindow = new ToolStripMenuItem("&Window");
            mainMenu.Items.AddRange(new ToolStripItem[] { menuFile, menuWindow });
            mainMenu.MdiWindowListItem = menuWindow;
            menuFile.DropDownItems.AddRange(new ToolStripItem[] { mnuOpen, separetor, mnuExit });
            mnuOpen.ShortcutKeys = (Keys)((Keys.Control | Keys.O));
            mnuOpen.Click += new EventHandler(mnuOpen_Click);
            mnuExit.ShortcutKeys = (Keys)((Keys.Alt | Keys.X));
            mnuExit.Click += new EventHandler(mnuExit_Click);
        }
        private void mnuOpen_Click(Object sender,EventArgs e)
        {
            frmChildForm childForm = new frmChildForm();
            childForm.Text = $"ChildForm{counter++:D2}";
            childForm.MdiParent = this;
            childForm.Show();
        }
        private void mnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
