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
        private string Acc_OR_Gyr, graph_title;
        //private string fout;
        private string mpuBuffer;
        private double t = 0, dt = 0.01;
        private double minValue, maxValue;
        private double Acc_X1, Acc_Y1, Acc_Z1, Acc_X2, Acc_Y2, Acc_Z2;
        private double Gyr_X1, Gyr_Y1, Gyr_Z1, Gyr_X2, Gyr_Y2, Gyr_Z2;
        private double LSB_GY = 131.0, LSB_Acc = 16384.0;
        private int index, legend,sensor;
        private string foutput; //file-output
        bool draw = false, usecalibrate = false;
        double rollA1, rollA2, knee_angle_A;
        double rollG1 = 0, rollG2 = 0, knee_angle_G;

        long offsetX_Gyr = 0, offsetY_Gyr = 0, offsetZ_Gyr = 0; //after Matlab Analysis
        long offsetX_Acc = 0 , offsetY_Acc = 0 , offsetZ_Acc =0;
        double offsetX_Gyr2 = -1.5651, offsetY_Gyr2 = -0.3998, offsetZ_Gyr2 = -0.6439; //after Matlab Analysis
        double offsetX_Acc2 = 0.0195, offsetY_Acc2 = -0.0742, offsetZ_Acc2 = 0.0233;
        KalmanFilter kalmanX1 = new KalmanFilter(0.001, 0.003, 0.03);
        KalmanFilter kalmanX2= new KalmanFilter(0.001, 0.003, 0.03);
        private double kalRoll_1 = 0, kalRoll_2 = 0, knee_angle_kal;

        private int ind = 0;
        private long[,] mpucal = new long[6,50];
        public Form1()
        {
            InitializeComponent();
        }

        private void calibrate_Click(object sender, EventArgs e)
        {
            usecalibrate = true;
        }
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            t = Math.Round(t + dt, 2);
            draw = true;
            //Console.WriteLine(String.Join("=>",t));
        }

        private void start_Click(object sender, EventArgs e)
        {
            if(start.Text == "Start")
            {
                timer1.Start();
                start.Text = "Stop";
                start.BackColor = Color.Gold;
                minValue = 0;
                maxValue = 3;

                Xaxis.ChartAreas[0].AxisX.Minimum = minValue;
                Xaxis.ChartAreas[0].AxisX.Maximum = maxValue;
            }
            else
            {
                timer1.Stop();
                start.Text = "Start";
                start.BackColor = Color.Red;
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           

        }


        private void process_Received_Data()
        {
            //split the data separated by spaces
            string[] split_line = mpuBuffer.Split(' ');
            int cnt = split_line.Length;
            int[] str_to_num = new int[cnt];
            bool check = false;

            try
            {
                //parsing the string to double
                for (int i = 0; i < cnt; i++)
                {
                    if (split_line[i][0] == '-')
                    {
                        //Console.WriteLine("neg");
                        str_to_num[i] = -1 * int.Parse(split_line[i].Substring(1));
                    }
                    else
                        str_to_num[i] = int.Parse(split_line[i]);

                }
                //Console.WriteLine(String.Join(",", str_to_num));

                // unstuffing the frame
                if (str_to_num[0] == 0xFF)
                {
                    if (str_to_num[1] == 0xFF)
                    {
                        if (str_to_num[2] == 0xFF)
                        {
                            int fin_check = 0xFF;
                            for(int i = 3; i < cnt-1; i++)
                            {
                                fin_check = fin_check ^ str_to_num[i];
                            }
                            //Console.WriteLine(fin_check);
                            if (fin_check == str_to_num[cnt - 1])
                            {
                                check = true;
                            }
                        }
                    }
                }
                if (usecalibrate)
                {
                    calibrate.BackColor = Color.LightGreen;
                    mpucal[0, ind] = str_to_num[6];
                    mpucal[1, ind] = str_to_num[12];
                    mpucal[2, ind] = str_to_num[4];
                    mpucal[3, ind] = str_to_num[5];
                    mpucal[4, ind] = str_to_num[10];
                    mpucal[5, ind] = str_to_num[11];

                    ind++;
                    //Console.WriteLine(ind);
                    if (ind == 50) calibrateGyro();
                }
                //Acc_X1 = (str_to_num[3] / LSB_Acc)- offsetX_Acc;
                Acc_Y1 = ((str_to_num[4]-offsetY_Acc) / LSB_Acc);
                Acc_Z1 = ((str_to_num[5]-offsetZ_Acc) / LSB_Acc);
                //Acc_X2 = str_to_num[9] / LSB_Acc;
                Acc_Y2 = ((str_to_num[10]-offsetY_Acc2) / LSB_Acc);
                Acc_Z2 = ((str_to_num[11]-offsetZ_Acc2) / LSB_Acc);
                Gyr_X1 = ((str_to_num[6] - offsetX_Gyr) / LSB_GY);
                //Gyr_Y1 = (str_to_num[7] / LSB_GY)- offsetY_Gyr;
                //Gyr_Z1 = (str_to_num[8] / LSB_GY)- offsetZ_Gyr;
                Gyr_X2 = ((str_to_num[12] - offsetX_Gyr2) / LSB_GY);
                //Gyr_Y2 = str_to_num[13] / LSB_GY;
                //Gyr_Z2 = str_to_num[14] / LSB_GY;

                //if (Math.Abs(Gyr_X1) < thX1Axis) Gyr_X1 = 0;
                //if (Math.Abs(Gyr_X2) < thX2Axis) Gyr_X2 = 0;

                if (check && draw)
                {
                    //plot
                    rollA1 = (Math.Atan2(Acc_Y1, Acc_Z1) * 180.0) / Math.PI - 85;
                    rollA2 = (Math.Atan2(Acc_Y2, Acc_Z2) * 180.0) / Math.PI - 85;
                    knee_angle_A = -rollA1 + rollA2;
                    rollG1 = rollG1 + Gyr_X1 * dt;
                    rollG2 = rollG2 + Gyr_X2 * dt;
                    knee_angle_G = -rollG1 + rollG2;
                    kalRoll_1 = kalmanX1.Update(rollA1, Gyr_X1);
                    kalRoll_2 = kalmanX2.Update(rollA2, Gyr_X2);
                    knee_angle_kal = -kalRoll_1 + kalRoll_2;
                    Console.WriteLine(rollA1);
                    Plot_Data();
                    //handle the output
                    foutput = rollA1.ToString() + " " + rollA2.ToString() + " " + rollG1.ToString() + " "
                              + rollG2.ToString() + Environment.NewLine;
                    //we put together the variables into a string (foutput) again
                    if (save.Checked == true) //file saving, same folder as executable
                    {
                        using (StreamWriter sw = File.AppendText("G:\\HIAST\\4th year\\project\\6th week\\Outputfile.txt"))//appendtext = the previous file will be continued
                        {
                            sw.Write(foutput); //write the content of foutput into the file
                        }
                    }
                    draw = false;
                }
            }
            catch { }
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
            //timer1.Stop();
            if (serial2.IsOpen)
            {
                  mpuBuffer = serial2.ReadLine();
                  //Console.WriteLine(mpuBuffer);
                  //at each received line from serial port we "trigger" a new processingdata delegate
                  this.Invoke(new Action(process_Received_Data));
            }

            //timer1.Start();
        }


        private void choose_data_SelectedIndexChanged(object sender, EventArgs e)
        {
            string curItem = choose_data.SelectedItem.ToString();
            index = choose_data.FindString(curItem);
            Init_Chart();
        }

        private void Init_Chart()
        {
            Xaxis.ChartAreas[0].AxisY.IsStartedFromZero = false;
            Yaxis.ChartAreas[0].AxisY.IsStartedFromZero = false;
            Zaxis.ChartAreas[0].AxisY.IsStartedFromZero = false;
            //Xaxis.Series["channel1"].Legend = "Acceleration roll measure";
            //Yaxis.Series["channel1"].Legend = "Acceleration roll measure";
            //Zaxis.Series["channel1"].Legend = "Acceleration roll measure";
            //Xaxis.Series["channel2"].Legend = "Gyroscope roll measure";
            //Yaxis.Series["channel2"].Legend = "Gyroscope roll measure";
            //Zaxis.Series["channel2"].Legend = "Gyroscope roll measure";
            //Xaxis.Series["channel3"].Legend = "Kalman filter";
            //Yaxis.Series["channel3"].Legend = "Kalman filter";
            //Zaxis.Series["channel3"].Legend = "Kalman filter";
            //Yaxis.ChartAreas[0].AxisY.Title = Acc_OR_Gyr;
            //Zaxis.ChartAreas[0].AxisY.Title = Acc_OR_Gyr;
        }

        private void Plot_Data() 
        {
            
            switch (index)
            {
                case 0:
                    Xaxis.Series["channel1"].Points.AddXY(t, rollA1);
                    Yaxis.Series["channel1"].Points.AddXY(t, rollA2);
                    Zaxis.Series["channel1"].Points.AddXY(t, knee_angle_A);
                    break;

                case 1:
                    Xaxis.Series["channel2"].Points.AddXY(t, rollG1);
                    Yaxis.Series["channel2"].Points.AddXY(t, rollG2);
                    Zaxis.Series["channel2"].Points.AddXY(t, knee_angle_G);
                    break;

                case 2:
                    Xaxis.Series["channel1"].Points.AddXY(t, rollA1);
                    Yaxis.Series["channel1"].Points.AddXY(t, rollA2);
                    Zaxis.Series["channel1"].Points.AddXY(t, knee_angle_A);
                    Xaxis.Series["channel2"].Points.AddXY(t, rollG1);
                    Yaxis.Series["channel2"].Points.AddXY(t, rollG2);
                    Zaxis.Series["channel2"].Points.AddXY(t, knee_angle_G);
                    break;
                case 3:
                    Xaxis.Series["channel1"].Points.AddXY(t, rollA1);
                    Yaxis.Series["channel1"].Points.AddXY(t, rollA2);
                    Zaxis.Series["channel1"].Points.AddXY(t, knee_angle_A);
                    Xaxis.Series["channel2"].Points.AddXY(t, rollG1);
                    Yaxis.Series["channel2"].Points.AddXY(t, rollG2);
                    Zaxis.Series["channel2"].Points.AddXY(t, knee_angle_G);
                    Xaxis.Series["channel3"].Points.AddXY(t, kalRoll_1);
                    Yaxis.Series["channel3"].Points.AddXY(t, kalRoll_2);
                    Zaxis.Series["channel3"].Points.AddXY(t, knee_angle_kal);

                    break;
            }
            Xaxis.ChartAreas[0].RecalculateAxesScale();
            Yaxis.ChartAreas[0].RecalculateAxesScale();
            Zaxis.ChartAreas[0].RecalculateAxesScale();

            if (t > maxValue)
            {
                if(index % 2 == 0)
                {
                    Xaxis.Series["channel1"].Points.RemoveAt(0);
                    Xaxis.ChartAreas[0].AxisX.Minimum = Xaxis.Series["channel1"].Points[0].XValue;
                    Xaxis.ChartAreas[0].AxisX.Maximum = Xaxis.Series["channel1"].Points[0].XValue + maxValue;

                    Yaxis.Series["channel1"].Points.RemoveAt(0);
                    Yaxis.ChartAreas[0].AxisX.Minimum = Yaxis.Series["channel1"].Points[0].XValue;
                    Yaxis.ChartAreas[0].AxisX.Maximum = Yaxis.Series["channel1"].Points[0].XValue + maxValue;

                    Zaxis.Series["channel1"].Points.RemoveAt(0);
                    Zaxis.ChartAreas[0].AxisX.Minimum = Yaxis.Series["channel1"].Points[0].XValue;
                    Zaxis.ChartAreas[0].AxisX.Maximum = Yaxis.Series["channel1"].Points[0].XValue + maxValue;       
                }
                else
                {
                    Xaxis.Series["channel2"].Points.RemoveAt(0);
                    Xaxis.ChartAreas[0].AxisX.Minimum = Xaxis.Series["channel2"].Points[0].XValue;
                    Xaxis.ChartAreas[0].AxisX.Maximum = Xaxis.Series["channel2"].Points[0].XValue + maxValue;

                    Yaxis.Series["channel2"].Points.RemoveAt(0);
                    Yaxis.ChartAreas[0].AxisX.Minimum = Yaxis.Series["channel2"].Points[0].XValue;
                    Yaxis.ChartAreas[0].AxisX.Maximum = Yaxis.Series["channel2"].Points[0].XValue + maxValue;

                    Zaxis.Series["channel2"].Points.RemoveAt(0);
                    Zaxis.ChartAreas[0].AxisX.Minimum = Yaxis.Series["channel2"].Points[0].XValue;
                    Zaxis.ChartAreas[0].AxisX.Maximum = Yaxis.Series["channel2"].Points[0].XValue + maxValue;
                }
 
            }

        }

        private void calibrateGyro()
        {
            usecalibrate = false;
            // Reset values
            long sumX1 = 0;
            long sumX2 = 0;
            long sumY1 = 0;
            long sumY2 = 0;
            long sumZ1 = 0;
            long sumZ2 = 0;

            for (int i = 0; i < 50; ++i)
            {
                sumX1 += mpucal[0,i];
                sumX2 += mpucal[1, i];
                sumY1 += mpucal[2, i];
                sumZ1 += mpucal[3, i];
                sumY2 += mpucal[4, i];
                sumZ2 += mpucal[5, i];
            }

            offsetX_Gyr = sumX1 / 50;
            offsetX_Gyr2 = sumX2 / 50;
            //offsetY_Acc = sumY1 / 50;
            //offsetZ_Acc = sumZ1 / 50;
            //offsetY_Acc2 = sumY2 / 50;
            //offsetZ_Acc2 = sumZ2 / 50;
            Console.WriteLine(offsetY_Acc);
            calibrate.BackColor = Color.DarkCyan;
        }
    }
}
