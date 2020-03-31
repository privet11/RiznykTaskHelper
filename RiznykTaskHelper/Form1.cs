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
    public partial class Form1 : Form
    {
        List<Task> tasksList = new List<Task>();
        List<Reminding> remindingsList = new List<Reminding>();
        public Form1()
        {
            InitializeComponent();
            DataGrindLoad();
            for (int i = 0; i < remindingsList.Count; i++)
            {
                if (remindingsList[i].Date.ToString("MM/dd/yyyy") == DateTime.Today.ToString("MM/dd/yyyy")&& remindingsList[i].Done!=true)
                {
                    MessageBox.Show("Today you have reminding: " + remindingsList[i].Name + '\n' + remindingsList[i].Description);
                }
            }
        }
        public void DataGrindLoad()
        {
            tasksList = Serialisation.GetList<Task>(Application.StartupPath + @"\tasks.json");
            remindingsList = Serialisation.GetList<Reminding>(Application.StartupPath + @"\remindings.json");
            for (int i = 0; i < tasksList.Count; i++)
            {
                dataGridView1.Rows.Add(tasksList[i].Name,tasksList[i].Done);
            }
            for (int i = 0; i < remindingsList.Count; i++)
            {
                dataGridView2.Rows.Add(remindingsList[i].Date, remindingsList[i].Name, remindingsList[i].Done);
            }
        }


        private void button1_Click(object sender, EventArgs e) //create
        {
            Create f1 = new Create();
            f1.Show();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.SelectedCells[0].Value != null)
            {
                int selectedRowIndex = dataGridView2.SelectedCells[0].RowIndex;
                richTextBox1.Text = remindingsList[selectedRowIndex].Description;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedCells[0].Value != null)
            {
                int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;
                richTextBox1.Text = tasksList[selectedRowIndex].Description;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                if (dataGridView1.SelectedCells[0].Value != null)
                {
                    DialogResult dialog = MessageBox.Show("Delete Task?", "", MessageBoxButtons.OKCancel);
                    if (dialog == DialogResult.OK)
                    {
                        int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;
                        tasksList.RemoveAt(selectedRowIndex);
                        Serialisation.Serialise<Task>(tasksList, Application.StartupPath + @"\tasks.json");
                        dataGridView1.Rows.RemoveAt(selectedRowIndex);
                        MessageBox.Show("Task deleted!");
                    }
                }
            }
            if (tabControl1.SelectedIndex == 1)
            {
                if (dataGridView2.SelectedCells[0].Value != null)
                {
                    DialogResult dialog = MessageBox.Show("Delete Reminding?", "", MessageBoxButtons.OKCancel);
                    if (dialog == DialogResult.OK)
                    {
                        int selectedRowIndex = dataGridView2.SelectedCells[0].RowIndex;
                        remindingsList.RemoveAt(selectedRowIndex);
                        Serialisation.Serialise<Reminding>(remindingsList, Application.StartupPath + @"\remindings.json");
                        dataGridView2.Rows.RemoveAt(selectedRowIndex);
                        MessageBox.Show("Reminding deleted!");
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
            {
                if (dataGridView1.SelectedCells[0].Value != null)
                {
                    DialogResult dialog = MessageBox.Show("Update Task?", "", MessageBoxButtons.OKCancel);
                    if (dialog == DialogResult.OK)
                    {
                        int selectedRowIndex = dataGridView1.SelectedCells[0].RowIndex;
                        tasksList[selectedRowIndex].Name = Convert.ToString(dataGridView1[0, selectedRowIndex].Value);
                        tasksList[selectedRowIndex].Description = richTextBox1.Text;
                        tasksList[selectedRowIndex].Done = Convert.ToBoolean(dataGridView1[1, selectedRowIndex].Value);
                        Serialisation.Serialise<Task>(tasksList, Application.StartupPath + @"\tasks.json");
                        MessageBox.Show("Task updated!");
                    }
                }
            }
            if (tabControl1.SelectedIndex == 1)
            {
                if (dataGridView2.SelectedCells[0].Value != null)
                {
                    DialogResult dialog = MessageBox.Show("Update Reminding?", "", MessageBoxButtons.OKCancel);
                    if (dialog == DialogResult.OK)
                    {
                        int selectedRowIndex = dataGridView2.SelectedCells[0].RowIndex;
                        remindingsList[selectedRowIndex].Name = Convert.ToString(dataGridView2[1, selectedRowIndex].Value);
                        remindingsList[selectedRowIndex].Description = richTextBox1.Text;
                        remindingsList[selectedRowIndex].Date = Convert.ToDateTime(Convert.ToString(dataGridView2[0, selectedRowIndex].Value));
                        remindingsList[selectedRowIndex].Done = Convert.ToBoolean(dataGridView2[2, selectedRowIndex].Value);
                        Serialisation.Serialise<Reminding>(remindingsList, Application.StartupPath + @"\remindings.json");
                        MessageBox.Show("Reminding updated!");
                    }
                }
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            DataGrindLoad();
        }
    }
}
