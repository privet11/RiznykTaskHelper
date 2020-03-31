using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RiznykTaskHelper
{
    public partial class Create : Form
    {
        public Create()
        {
            InitializeComponent();
        }


        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                DialogResult dialog = MessageBox.Show("Create Task?", "", MessageBoxButtons.OKCancel);
                if (dialog == DialogResult.OK)
                {
                    Task newTask = new Task() { Name = textBox1.Text, Description = richTextBox1.Text, Done=false };
                    Serialisation.Serialise<Task>(newTask, Application.StartupPath + @"\tasks.json");
                    MessageBox.Show("Task created!");
                }
            }
            if (tabControl1.SelectedIndex == 1)
            {
                DialogResult dialog = MessageBox.Show("Create Reminding?", "", MessageBoxButtons.OKCancel);
                if (dialog == DialogResult.OK)
                {
                    Reminding newReminding = new Reminding() { Name = textBox2.Text, Description = richTextBox2.Text, Date = dateTimePicker1.Value, Done = false };
                    Serialisation.Serialise<Reminding>(newReminding, Application.StartupPath + @"\remindings.json");
                    MessageBox.Show("Reminding created!");
                }
            }
        }
    }
}
