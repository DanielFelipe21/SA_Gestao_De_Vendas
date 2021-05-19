using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp7
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Form1 f = new Form1();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form1 f = new Form1();
            ListViewItem varItem = new ListViewItem(new string[] { textBox1.Text,
                                                                textBox2.Text,
                                                                textBox3.Text
                                                               });
            f.listView1.Items.Add(varItem);
            f.listView1.Items[f.listView1.Items.Count - 1].EnsureVisible(); //for autoscroll
            Close();
        }
    }
}
