using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace CPUMonitor
{
    public partial class StockMonitorForm : Form
    {

        public List<string> ConfigList { get; set; }
        public StockMonitorForm()
        {
            InitializeComponent();
        }

        private bool isMouseDown = false;
        private Point FormLocation;     //form的location
        private Point mouseOffset;      //鼠标的按下位置

       
        private void StockMonitorForm_MouseEnter(object sender, EventArgs e)
        {
            this.Opacity = 1;
        }

        private void StockMonitorForm_MouseLeave(object sender, EventArgs e)
        {
            this.Opacity = 0.5;
        }

        private void StockMonitorForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isMouseDown = true;
                FormLocation = this.Location;
                mouseOffset = Control.MousePosition;
            }
        }

        private void StockMonitorForm_MouseUp(object sender, MouseEventArgs e)
        {
            isMouseDown = false;
        }

        private void StockMonitorForm_MouseMove(object sender, MouseEventArgs e)
        {
            int _x = 0;
            int _y = 0;
            if (isMouseDown)
            {
                Point pt = Control.MousePosition;
                _x = mouseOffset.X - pt.X;
                _y = mouseOffset.Y - pt.Y;

                this.Location = new Point(FormLocation.X - _x, FormLocation.Y - _y);
            }
        }

        private void label1_MouseEnter(object sender, EventArgs e)
        {
            this.Opacity = 1;
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            this.Opacity = 1;
        }

        private void label3_MouseEnter(object sender, EventArgs e)
        {
            this.Opacity = 1;
        }

        private void label4_MouseEnter(object sender, EventArgs e)
        {
            this.Opacity = 1;
        }

       
        private void label1_MouseLeave(object sender, EventArgs e)
        {
            this.Opacity = 0.5;
        }

        private void label2_MouseLeave(object sender, EventArgs e)
        {
            this.Opacity = 0.5;
        }

        private void label4_MouseLeave(object sender, EventArgs e)
        {
            this.Opacity = 0.5;
        }

        private void label3_MouseLeave(object sender, EventArgs e)
        {
            this.Opacity = 0.5;
        }

        private void StockMonitorForm_Load(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(state => monitorLabel1());

            ThreadPool.QueueUserWorkItem(state => monitorLabel2());

            ThreadPool.QueueUserWorkItem(state => monitorLabel3());

            ThreadPool.QueueUserWorkItem(state => monitorLabel4());
        }

        private void monitorLabel1()
        {
           
            if (ConfigList.Count >= 1)
            {
                while (true)
                {
                    var result = GetMessage(ConfigList[0],1);
                    this.Invoke(new Action(() => label1.Text = result));
                    Thread.Sleep(60000);
                }
                
            }
        }

        private void monitorLabel2()
        {
            if (ConfigList.Count >= 2)
            {
                while (true)
                {
                    var result = GetMessage(ConfigList[1], 2);
                    this.Invoke(new Action(() => label2.Text = result));
                    Thread.Sleep(15000);
                }
            }
        }
        private void monitorLabel3()
        {
            if (ConfigList.Count >= 3)
            {
                while (true)
                {
                    var result = GetMessage(ConfigList[2], 3);
                    this.Invoke(new Action(() => label3.Text = result));
                    Thread.Sleep(15000);
                }

            }
        }
        private void monitorLabel4()
        {
            if (ConfigList.Count >= 4)
            {
                while (true)
                {
                    var result = GetMessage(ConfigList[3], 4);
                    this.Invoke(new Action(() => label4.Text = result));
                    Thread.Sleep(15000);
                }

            }
        }

        private static string GetHttpResponse(string code)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://hq.sinajs.cn/list="+code);
            request.Method = "GET";
            request.ContentType = "text/html;charset=unicode";
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream myResponseStream = response.GetResponseStream();
            StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8);
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();
            return retString;
        }

        private static string GetMessage(string code,int index)
        {
            var result = GetHttpResponse(code);

            var usingInfo = result.Split('=')[1].Replace("\"",string.Empty);

            var currentPrice = usingInfo.Split(',')[3];

            var stockCode = code.Substring(2);

            return $"cpu core {index.ToString()}:0x{stockCode} {currentPrice.Substring(0,currentPrice.Length-1)}"+"%";
        }
    }
}
