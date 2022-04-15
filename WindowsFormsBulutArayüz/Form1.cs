using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsBulutArayüz
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            this.Hide();
            form3.ShowDialog();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String[] arr = {"", "", "", "", "", "", "", ""};
            Form2 form2 = new Form2(arr);
            this.Hide();
            form2.ShowDialog();
            
        }
    }
}
