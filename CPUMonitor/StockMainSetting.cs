using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPUMonitor
{
    public partial class StockMainSetting : Form
    {
        public StockMainSetting()
        {
            InitializeComponent();
        }

        private void StockMainSetting_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 1;
            comboBox2.SelectedIndex = 1;
            comboBox3.SelectedIndex = 1;
            comboBox4.SelectedIndex = 1;
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var configList = new List<string>();

            if (comboBox1.SelectedIndex > -1 && !string.IsNullOrWhiteSpace(textBox1.Text))
            {
                configList.Add(comboBox1.SelectedItem.ToString()+textBox1.Text.Trim());
            }

            if (comboBox2.SelectedIndex > -1 && !string.IsNullOrWhiteSpace(textBox2.Text))
            {
                configList.Add(comboBox2.SelectedItem.ToString()+textBox2.Text.Trim());
            }

            if (comboBox3.SelectedIndex > -1 && !string.IsNullOrWhiteSpace(textBox3.Text))
            {
                configList.Add(comboBox3.SelectedItem.ToString()+textBox3.Text.Trim());
            }

            if (comboBox4.SelectedIndex > -1 && !string.IsNullOrWhiteSpace(textBox4.Text))
            {
                configList.Add(comboBox4.SelectedItem.ToString()+textBox4.Text.Trim());
            }

            if (configList.Count > 0)
            {
                this.Hide();
                StockMonitorForm mf = new StockMonitorForm();
                mf.ConfigList = configList;
                mf.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("error configuration");
            }




            

           

        }
    }
}
