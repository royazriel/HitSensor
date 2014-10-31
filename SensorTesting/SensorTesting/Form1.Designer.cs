namespace SensorTesting
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.listComNames = new System.Windows.Forms.ListBox();
            this.terminalRx = new System.Windows.Forms.RichTextBox();
            this.terminalTx = new System.Windows.Forms.RichTextBox();
            this.logFileName = new System.Windows.Forms.TextBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // listComNames
            // 
            this.listComNames.FormattingEnabled = true;
            this.listComNames.Location = new System.Drawing.Point(21, 12);
            this.listComNames.Name = "listComNames";
            this.listComNames.Size = new System.Drawing.Size(114, 30);
            this.listComNames.TabIndex = 0;
            this.listComNames.Click += new System.EventHandler(this.listComNames_Click);
            // 
            // terminalRx
            // 
            this.terminalRx.Location = new System.Drawing.Point(21, 107);
            this.terminalRx.Name = "terminalRx";
            this.terminalRx.Size = new System.Drawing.Size(387, 372);
            this.terminalRx.TabIndex = 2;
            this.terminalRx.Text = "";
            // 
            // terminalTx
            // 
            this.terminalTx.Location = new System.Drawing.Point(21, 58);
            this.terminalTx.Name = "terminalTx";
            this.terminalTx.Size = new System.Drawing.Size(387, 26);
            this.terminalTx.TabIndex = 3;
            this.terminalTx.Text = "";
            this.terminalTx.KeyUp += new System.Windows.Forms.KeyEventHandler(this.terminalTx_KeyUp);
            // 
            // logFileName
            // 
            this.logFileName.Location = new System.Drawing.Point(300, 18);
            this.logFileName.Name = "logFileName";
            this.logFileName.Size = new System.Drawing.Size(106, 20);
            this.logFileName.TabIndex = 4;
            this.logFileName.Text = "log.csv";
            // 
            // chart1
            // 
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(503, 18);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "shot";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(666, 461);
            this.chart1.TabIndex = 5;
            this.chart1.Text = "chart1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1240, 523);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.logFileName);
            this.Controls.Add(this.terminalTx);
            this.Controls.Add(this.terminalRx);
            this.Controls.Add(this.listComNames);
            this.Name = "Form1";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listComNames;
        private System.Windows.Forms.RichTextBox terminalRx;
        private System.Windows.Forms.RichTextBox terminalTx;
        private System.Windows.Forms.TextBox logFileName;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    }
}

