using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sly_WAL_Editor
{
    public partial class Startup : Form
    {
        public Startup()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Sly_1 sly1 = new Sly_1();
            sly1.Show();
            Visible = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form1 sly2 = new Form1();
            sly2.Show();
            Visible = false;
        }
    }
}
