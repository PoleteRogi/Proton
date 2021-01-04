using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proton
{
    public partial class Alert : Form
    {
        public Alert(string Website, string Text)
        {
            InitializeComponent();
            guna2HtmlLabel1.Text = string.Format(guna2HtmlLabel1.Text, Website);
            guna2HtmlLabel2.Text = string.Format(guna2HtmlLabel2.Text, Text);
        }

        private void Alert_Load(object sender, EventArgs e)
        {

        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
