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
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            Form2.GetItems(listView1);
        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            String[] arr = { listView1.SelectedItems[0].SubItems[0].Text, listView1.SelectedItems[0].SubItems[1].Text,
            listView1.SelectedItems[0].SubItems[2].Text, listView1.SelectedItems[0].SubItems[3].Text, listView1.SelectedItems[0].SubItems[4].Text,
            listView1.SelectedItems[0].SubItems[5].Text, listView1.SelectedItems[0].SubItems[6].Text, listView1.SelectedItems[0].SubItems[7].Text};

            Form2 frm2 = new Form2(arr);
            frm2.ShowDialog();
            //this.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 frm1 = new Form1();
            this.Close();
            frm1.Show();
            

        }

        private void button2_Click(object sender, EventArgs e)
        {
            update_list();
        }

        public void update_list()
        {
            listView1.Items.Clear();
            Form2.GetItems(listView1);
        }

        

        private void Form3_Activated(object sender, EventArgs e)
        {
            update_list();
        }
    }
}
