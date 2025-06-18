namespace anothertry
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea3 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend3 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series8 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series9 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.panel1 = new System.Windows.Forms.Panel();
            this.calibrate = new System.Windows.Forms.Button();
            this.save = new System.Windows.Forms.CheckBox();
            this.choose_data = new System.Windows.Forms.ComboBox();
            this.start = new System.Windows.Forms.Button();
            this.connect = new System.Windows.Forms.Button();
            this.Xaxis = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Yaxis = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.Zaxis = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.serial2 = new System.IO.Ports.SerialPort(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Xaxis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Yaxis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Zaxis)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.calibrate);
            this.panel1.Controls.Add(this.save);
            this.panel1.Controls.Add(this.choose_data);
            this.panel1.Controls.Add(this.start);
            this.panel1.Controls.Add(this.connect);
            this.panel1.Location = new System.Drawing.Point(863, 124);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(195, 384);
            this.panel1.TabIndex = 0;
            // 
            // calibrate
            // 
            this.calibrate.BackColor = System.Drawing.Color.DarkOrange;
            this.calibrate.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.calibrate.Location = new System.Drawing.Point(41, 176);
            this.calibrate.Name = "calibrate";
            this.calibrate.Size = new System.Drawing.Size(120, 40);
            this.calibrate.TabIndex = 4;
            this.calibrate.Text = "Reset";
            this.calibrate.UseVisualStyleBackColor = false;
            this.calibrate.Click += new System.EventHandler(this.calibrate_Click);
            // 
            // save
            // 
            this.save.AutoSize = true;
            this.save.Location = new System.Drawing.Point(3, 364);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(77, 17);
            this.save.TabIndex = 3;
            this.save.Text = "Save Data";
            this.save.UseVisualStyleBackColor = true;
            this.save.CheckedChanged += new System.EventHandler(this.save_CheckedChanged);
            // 
            // choose_data
            // 
            this.choose_data.FormattingEnabled = true;
            this.choose_data.Items.AddRange(new object[] {
            "Angle from Accelerometer ",
            "Angle from Gyroscope",
            "Angle from Both",
            "Angle estimation with KF"});
            this.choose_data.Location = new System.Drawing.Point(17, 52);
            this.choose_data.Name = "choose_data";
            this.choose_data.Size = new System.Drawing.Size(160, 21);
            this.choose_data.TabIndex = 2;
            this.choose_data.SelectedIndexChanged += new System.EventHandler(this.choose_data_SelectedIndexChanged);
            // 
            // start
            // 
            this.start.BackColor = System.Drawing.Color.Gold;
            this.start.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.start.Location = new System.Drawing.Point(41, 306);
            this.start.Name = "start";
            this.start.Size = new System.Drawing.Size(120, 40);
            this.start.TabIndex = 1;
            this.start.Text = "Start";
            this.start.UseVisualStyleBackColor = false;
            this.start.Click += new System.EventHandler(this.start_Click);
            // 
            // connect
            // 
            this.connect.BackColor = System.Drawing.Color.Red;
            this.connect.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.connect.Location = new System.Drawing.Point(41, 238);
            this.connect.Name = "connect";
            this.connect.Size = new System.Drawing.Size(120, 40);
            this.connect.TabIndex = 0;
            this.connect.Text = "Connect";
            this.connect.UseVisualStyleBackColor = false;
            this.connect.Click += new System.EventHandler(this.connect_Click);
            // 
            // Xaxis
            // 
            chartArea1.AxisX.Title = "time(sec)";
            chartArea1.AxisY.Title = "Angle(deg)";
            chartArea1.Name = "ChartArea1";
            this.Xaxis.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.Xaxis.Legends.Add(legend1);
            this.Xaxis.Location = new System.Drawing.Point(12, 31);
            this.Xaxis.Name = "Xaxis";
            this.Xaxis.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SemiTransparent;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series1.Legend = "Legend1";
            series1.LegendText = "Accelerometer";
            series1.Name = "channel1";
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series2.Legend = "Legend1";
            series2.LegendText = "Gyroscope";
            series2.Name = "channel2";
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series3.Legend = "Legend1";
            series3.LegendText = "Estimation";
            series3.Name = "channel3";
            this.Xaxis.Series.Add(series1);
            this.Xaxis.Series.Add(series2);
            this.Xaxis.Series.Add(series3);
            this.Xaxis.Size = new System.Drawing.Size(800, 170);
            this.Xaxis.TabIndex = 1;
            this.Xaxis.Text = "Xaxis";
            // 
            // Yaxis
            // 
            this.Yaxis.Anchor = System.Windows.Forms.AnchorStyles.Left;
            chartArea2.AxisX.Title = "time(sec)";
            chartArea2.AxisY.Title = "Angle(deg)";
            chartArea2.Name = "ChartArea1";
            this.Yaxis.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.Yaxis.Legends.Add(legend2);
            this.Yaxis.Location = new System.Drawing.Point(12, 222);
            this.Yaxis.Name = "Yaxis";
            this.Yaxis.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SemiTransparent;
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series4.Legend = "Legend1";
            series4.LegendText = "Acceleration";
            series4.Name = "channel1";
            series5.ChartArea = "ChartArea1";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series5.Legend = "Legend1";
            series5.LegendText = "Gyroscope";
            series5.Name = "channel2";
            series6.ChartArea = "ChartArea1";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series6.Legend = "Legend1";
            series6.LegendText = "Estimation";
            series6.Name = "channel3";
            this.Yaxis.Series.Add(series4);
            this.Yaxis.Series.Add(series5);
            this.Yaxis.Series.Add(series6);
            this.Yaxis.Size = new System.Drawing.Size(800, 170);
            this.Yaxis.TabIndex = 2;
            this.Yaxis.Text = "Yaxis";
            // 
            // Zaxis
            // 
            this.Zaxis.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            chartArea3.AxisX.Title = "time(sec)";
            chartArea3.AxisY.Title = "Angle(deg)";
            chartArea3.Name = "ChartArea1";
            this.Zaxis.ChartAreas.Add(chartArea3);
            legend3.Name = "Legend1";
            this.Zaxis.Legends.Add(legend3);
            this.Zaxis.Location = new System.Drawing.Point(12, 415);
            this.Zaxis.Name = "Zaxis";
            this.Zaxis.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.SemiTransparent;
            series7.ChartArea = "ChartArea1";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series7.Legend = "Legend1";
            series7.LegendText = "Acceleration";
            series7.Name = "channel1";
            series8.ChartArea = "ChartArea1";
            series8.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series8.Legend = "Legend1";
            series8.LegendText = "Gyroscope";
            series8.Name = "channel2";
            series9.ChartArea = "ChartArea1";
            series9.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
            series9.Legend = "Legend1";
            series9.LegendText = "Estimation";
            series9.Name = "channel3";
            this.Zaxis.Series.Add(series7);
            this.Zaxis.Series.Add(series8);
            this.Zaxis.Series.Add(series9);
            this.Zaxis.Size = new System.Drawing.Size(800, 170);
            this.Zaxis.TabIndex = 3;
            this.Zaxis.Text = "Zaxis";
            // 
            // serial2
            // 
            this.serial2.BaudRate = 115200;
            this.serial2.PortName = "COM4";
            this.serial2.WriteBufferSize = 4096;
            this.serial2.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serial2_DataReceived);
            // 
            // timer1
            // 
            this.timer1.Interval = 10;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1094, 611);
            this.Controls.Add(this.Zaxis);
            this.Controls.Add(this.Yaxis);
            this.Controls.Add(this.Xaxis);
            this.Controls.Add(this.panel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Xaxis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Yaxis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Zaxis)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ComboBox choose_data;
        private System.Windows.Forms.Button start;
        private System.Windows.Forms.Button connect;
        private System.Windows.Forms.DataVisualization.Charting.Chart Xaxis;
        private System.Windows.Forms.DataVisualization.Charting.Chart Yaxis;
        private System.Windows.Forms.DataVisualization.Charting.Chart Zaxis;
        private System.IO.Ports.SerialPort serial2;
        private System.Windows.Forms.CheckBox save;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button calibrate;
    }
}

