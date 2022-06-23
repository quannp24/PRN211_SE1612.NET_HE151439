using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Slot13_FristWindowForm
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }



        private void button1_Click(object sender, EventArgs e)
        {

            MessageBox.Show("Employee ID " + txt_id.Text +
                             "\nEmployee name " + txt_name.Text +
                             "\nEmployee phone " + txt_phone.Text +
                             "\nGender " + (radio_female.Checked ? "Female" : "Male")+
                             "\nDegree "+degree.SelectedItem.ToString(),"Employee detail"
                             );
                            
            

        }

        private void cancel_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
