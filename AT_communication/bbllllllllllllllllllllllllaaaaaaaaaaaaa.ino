using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;

namespace anothertry
{
    public partial class Form1 : Form
    {
        //private Int32 myline;
        private string Acc_OR_Gyr;
        //private string fout;
        private List<Byte> mpuBuffer = new List<byte>();
        private double t = 0;
        private double Acc_X1, Acc_Y1, Acc_Z1, Acc_X2, Acc_Y2, Acc_Z2;
        private double Gyr_X1, Gyr_Y1, Gyr_Z1, Gyr_X2, Gyr_Y2, Gyr_Z2;
        private double LSB_GY = 131.0;
        private double LSB_ACC = 16384.0;
        //private int index;
        public Form1()
        {
            InitializeComponent();
            //Init_Chart();
        }

        private void start_Click(object sender, EventArgs e)
        {
            int bytestoread = 0;
            timer1.Stop();
            try
            {
                bytestoread = serial2.BytesToRead;
                Console.WriteLine(bytestoread);
            }
            catch (InvalidOperationException ex)
            {
                //MessageBox.Show("Serial connection lost. Exception types:" + ex.ToString());
            }
            if (serial2.IsOpen)
            {
                while (bytestoread == 0)
                {
                    bytestoread = serial2.BytesToRead;
                    Console.WriteLine(bytestoread);
                    Application.DoEvents();
                }

                    if (bytestoread != 0)
                {
                    byte[] mpu_frame = new byte[bytestoread];
                    byte check = 0;
                    int Len = mpu_frame.Length - 1;
                    serial2.Read(mpu_frame, 0, bytestoread);
                    Console.WriteLine(mpu_frame);
                    Console.WriteLine("u");
                    if (mpu_frame[0] == 0xFF && mpu_frame[1] == 0xFF && mpu_frame[2] == 0xFF)
                    {
                        
                        for(int i=0; i<Len; i++)
                        {
                            check = (byte)(check ^ mpu_frame[i]);
                        }
                        if (check == mpu_frame[Len])
                        {
                            mpuBuffer.AddRange(mpu_frame);
                            process_Received_Data();
                            connect.BackColor = Color.Green;
                        }

                    }
                    else
                    {
                        connect.BackColor = Color.Orange;

                    }
                }

            }
            timer1.Start();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            t = t + 0.01;
            Console.WriteLine(t);
        }


        private void process_Received_Data()
        {
            try
            {

                string curItem = choose_data.SelectedItem.ToString();
                int index = choose_data.FindString(curItem);

                switch (index)
                {
                    case 0:
                        Acc_X1 = (mpuBuffer[3] << 8 | mpuBuffer[4]) / LSB_ACC;
                        Acc_Y1 = (mpuBuffer[5] << 8 | mpuBuffer[6]) / LSB_ACC;
                        Acc_Z1 = (mpuBuffer[7] << 8 | mpuBuffer[8]) / LSB_ACC;
                        Acc_OR_Gyr = "Acceleration(m/s^2)-g-";
                        break;

                    case 1:
                        Acc_X2 = (mpuBuffer[9] << 8 | mpuBuffer[10]) / LSB_ACC;
                        Acc_Y2 = (mpuBuffer[11] << 8 | mpuBuffer[12]) / LSB_ACC;
                        Acc_Z2 = (mpuBuffer[13] << 8 | mpuBuffer[14]) / LSB_ACC;
                        Acc_OR_Gyr = "Acceleration(m/s^2)-g-";
                        break;

                    case 2:
                        Gyr_X1 = (mpuBuffer[15] << 8 | mpuBuffer[16]) / LSB_GY;
                        Gyr_Y1 = (mpuBuffer[17] << 8 | mpuBuffer[18]) / LSB_GY;
                        Gyr_Z1 = (mpuBuffer[19] << 8 | mpuBuffer[20]) / LSB_GY;
                        Acc_OR_Gyr = "GyroScope(deg/s)";
                        break;

                    case 3:
                        Gyr_X2 = (mpuBuffer[21] << 8 | mpuBuffer[22]) / LSB_GY;
                        Gyr_Y2 = (mpuBuffer[23] << 8 | mpuBuffer[24]) / LSB_GY;
                        Gyr_Z2 = (mpuBuffer[25] << 8 | mpuBuffer[26]) / LSB_GY;
                        Acc_OR_Gyr = "GyroScope(deg/s)";
                        break;

                    case 4:
                        Acc_X1 = (mpuBuffer[3] << 8 | mpuBuffer[4]) / LSB_ACC;
                        Acc_Y1 = (mpuBuffer[5] << 8 | mpuBuffer[6]) / LSB_ACC;
                        Acc_Z1 = (mpuBuffer[7] << 8 | mpuBuffer[8]) / LSB_ACC;
                        Acc_X2 = (mpuBuffer[9] << 8 | mpuBuffer[10]) / LSB_ACC;
                        Acc_Y2 = (mpuBuffer[11] << 8 | mpuBuffer[12]) / LSB_ACC;
                        Acc_Z2 = (mpuBuffer[13] << 8 | mpuBuffer[14]) / LSB_ACC;
                        Acc_OR_Gyr = "Acceleration(m/s^2)-g-";
                        break;

                    case 5:
                        Gyr_X1 = (mpuBuffer[15] << 8 | mpuBuffer[16]) / LSB_GY;
                        Gyr_Y1 = (mpuBuffer[17] << 8 | mpuBuffer[18]) / LSB_GY;
                        Gyr_Z1 = (mpuBuffer[19] << 8 | mpuBuffer[20]) / LSB_GY;
                        Gyr_X2 = (mpuBuffer[21] << 8 | mpuBuffer[22]) / LSB_GY;
                        Gyr_Y2 = (mpuBuffer[23] << 8 | mpuBuffer[24]) / LSB_GY;
                        Gyr_Z2 = (mpuBuffer[25] << 8 | mpuBuffer[26]) / LSB_GY;
                        Acc_OR_Gyr = "GyroScope(deg/s)";
                        break;
                }
                switch (index)
                {
                    case 0:
                        Xaxis.Series["channel1"].Points.AddXY(t,Acc_X1);
                        Xaxis.ChartAreas[0].RecalculateAxesScale();

                        Yaxis.Series["channel1"].Points.AddXY(t,Acc_Y1);
                        Yaxis.ChartAreas[0].RecalculateAxesScale();

                        Zaxis.Series["channel1"].Points.AddXY(t,Acc_Z1);
                        Zaxis.ChartAreas[0].RecalculateAxesScale();
                        break;
                    case 1:
                        Xaxis.Series["channel1"].Points.AddXY(t, Acc_X2);
                        Xaxis.ChartAreas[0].RecalculateAxesScale();

                        Yaxis.Series["channel1"].Points.AddXY(t, Acc_Y2);
                        Yaxis.ChartAreas[0].RecalculateAxesScale();

                        Zaxis.Series["channel1"].Points.AddXY(t, Acc_Z2);
                        Zaxis.ChartAreas[0].RecalculateAxesScale();
                        break;
                    case 2:
                        Xaxis.Series["channel1"].Points.AddXY(t, Gyr_X1);
                        Xaxis.ChartAreas[0].RecalculateAxesScale();

                        Yaxis.Series["channel1"].Points.AddXY(t, Gyr_Y1);
                        Yaxis.ChartAreas[0].RecalculateAxesScale();

                        Zaxis.Series["channel1"].Points.AddXY(t, Gyr_Z1);
                        Zaxis.ChartAreas[0].RecalculateAxesScale();
                        break;
                    case 3:
                        Xaxis.Series["channel1"].Points.AddXY(t, Gyr_X2);
                        Xaxis.ChartAreas[0].RecalculateAxesScale();

                        Yaxis.Series["channel1"].Points.AddXY(t, Gyr_Y2);
                        Yaxis.ChartAreas[0].RecalculateAxesScale();

                        Zaxis.Series["channel1"].Points.AddXY(t, Gyr_Z2);
                        Zaxis.ChartAreas[0].RecalculateAxesScale();
                        break;
                    case 4:
                        Xaxis.Series["channel1"].Points.AddXY(t, Acc_X1);
                        Xaxis.Series["channel2"].Points.AddXY(t, Acc_X2);
                        Xaxis.ChartAreas[0].RecalculateAxesScale();

                        Yaxis.Series["channel1"].Points.AddXY(t, Acc_Y1);
                        Yaxis.Series["channel2"].Points.AddXY(t, Acc_Y2);
                        Yaxis.ChartAreas[0].RecalculateAxesScale();

                        Zaxis.Series["channel1"].Points.AddXY(t, Acc_Z1);
                        Zaxis.Series["channel2"].Points.AddXY(t, Acc_Z2);
                        Zaxis.ChartAreas[0].RecalculateAxesScale();
                        break;
                    case 5:
                        Xaxis.Series["channel1"].Points.AddXY(t,Gyr_X1);
                        Xaxis.Series["channel2"].Points.AddXY(t,Gyr_X2);
                        Xaxis.ChartAreas[0].RecalculateAxesScale();

                        Yaxis.Series["channel1"].Points.AddXY(t,Gyr_Y1);
                        Yaxis.Series["channel2"].Points.AddXY(t,Gyr_Y2);
                        Yaxis.ChartAreas[0].RecalculateAxesScale();

                        Zaxis.Series["channel1"].Points.AddXY(t,Gyr_Z1);
                        Zaxis.Series["channel2"].Points.AddXY(t,Gyr_Z2);
                        Zaxis.ChartAreas[0].RecalculateAxesScale();
                        break;
                }
                

                //fout = DataBindings.ToString() + Environment.NewLine;
                //if (save.Checked == true){
                //    using (Streamwriter sw = File.AppendText("outputfile.txt"));
                //}
            }
            catch { }

        }

        private void Init_Chart()
        {

            Xaxis.ChartAreas[0].AxisX.IsStartedFromZero = true;
            Xaxis.ChartAreas[0].AxisX.MajorGrid.LineWidth = 1;
            Xaxis.ChartAreas[0].AxisY.MajorGrid.LineWidth = 1;
            Xaxis.ChartAreas[0].AxisX.Title = "time(sec)";
            Xaxis.ChartAreas[0].AxisY.Title = Acc_OR_Gyr;
            Xaxis.ChartAreas[0].AxisX.Enabled = AxisEnabled.True;
            Xaxis.ChartAreas[0].AxisY.Enabled = AxisEnabled.True;
            Xaxis.ChartAreas[0].CursorX.AutoScroll = true;

            Yaxis.ChartAreas[0].AxisX.IsStartedFromZero = true;
            Yaxis.ChartAreas[0].AxisX.MajorGrid.LineWidth = 1;
            Yaxis.ChartAreas[0].AxisY.MajorGrid.LineWidth = 1;
            Yaxis.ChartAreas[0].AxisX.Title = "time(sec)";
            Yaxis.ChartAreas[0].AxisY.Title = Acc_OR_Gyr;
            Yaxis.ChartAreas[0].AxisX.Enabled = AxisEnabled.True;
            Yaxis.ChartAreas[0].AxisY.Enabled = AxisEnabled.True;
            Yaxis.ChartAreas[0].CursorX.AutoScroll = true;

            Zaxis.ChartAreas[0].AxisX.IsStartedFromZero = true;
            Zaxis.ChartAreas[0].AxisX.MajorGrid.LineWidth = 1;
            Zaxis.ChartAreas[0].AxisY.MajorGrid.LineWidth = 1;
            Zaxis.ChartAreas[0].AxisX.Title = "time(sec)";
            Zaxis.ChartAreas[0].AxisY.Title = Acc_OR_Gyr;
            Zaxis.ChartAreas[0].AxisX.Enabled = AxisEnabled.True;
            Zaxis.ChartAreas[0].AxisY.Enabled = AxisEnabled.True;
            Zaxis.ChartAreas[0].CursorX.AutoScroll = true;

            Series seq1 = new Series()
            {
                Name = "channel1",
                Color = System.Drawing.Color.Blue,
                ChartType = SeriesChartType.FastLine,
                IsXValueIndexed = true,
                IsVisibleInLegend = true
            };

            Xaxis.Series.Add(seq1);
            Yaxis.Series.Add(seq1);
            Zaxis.Series.Add(seq1);

            Series seq2 = new Series()
            {
                Name = "channel2",
                Color = System.Drawing.Color.Red,
                ChartType = SeriesChartType.FastLine,
                IsXValueIndexed = true,
                IsVisibleInLegend = true
            };

            Xaxis.Series.Add(seq2);
            Yaxis.Series.Add(seq2);
            Zaxis.Series.Add(seq2);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                serial2.Close();
                timer1.Stop();
            }
            catch { }

        }

        private void connect_Click(object sender, EventArgs e)
        {
            try
            {
                serial2.Open();
                if (serial2.IsOpen)
                    connect.BackColor = Color.Green;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error opening the Serial!");
            }
            
        }

        private void save_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void serial2_DataReceived(object sender, EventArgs e)
        {
            Console.WriteLine()
        }

        private void choose_data_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
