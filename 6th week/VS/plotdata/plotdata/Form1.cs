using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;
using System.Diagnostics;


namespace plotdata
{
    public partial class Form1 : Form
    {
        private Thread cpuThead;
        private double[] cpuArray = new double[30];


        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void getPerformanceCounters()
        {
            var cpuPerfCounter = new PerformanceCounter("Processor Information", "%Processor Time", "_Total");

            while (true)
            {
                cpuArray[cpuArray.Length - 1] = Math.Round(cpuPerfCounter.NextValue(), 0);
                Array.Copy(cpuArray, 1, cpuArray, 0, cpuArray.Length - 1);
                if (chart.IsHandleCreated)
                {
                    this.Invoke((MethodInvoker)delegate { UpdateCpuChart(); });
                }
                else
                {

                }
                Thread.Sleep(1000);
            }
        }
        private void UpdateCpuChart()
        {
            chart.Series["series1"].Points.Clear();
            for(int i=0; i < cpuArray.Length - 1; i++)
            {
                chart.Series["series1"].Points.AddY(cpuArray[i]);

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            cpuThead = new Thread(new ThreadStart(this.getPerformanceCounters));
            cpuThead.IsBackground = true;
            cpuThead.Start();
        }
    }
}
