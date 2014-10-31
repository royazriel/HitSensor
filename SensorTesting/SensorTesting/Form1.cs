using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms.DataVisualization.Charting.ChartTypes;
namespace SensorTesting
{
    public partial class Form1 : Form
    {
        private SerialCom com;
        public Form1()
        {
            InitializeComponent();
            com = new SerialCom();
            com.ListComNames(listComNames);
            DirectoryInfo di = new DirectoryInfo(@"c:\log");

            if (di.Exists)
            {
                return;
            }
            di.Create();
        }

   
        private void listComNames_Click(object sender, EventArgs e)
        {
            try
            {
                com.InitSerial((string)listComNames.SelectedItem, (int)9600);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void terminalTx_KeyUp(object sender, KeyEventArgs e)
        {
            string rx = string.Empty;
            StreamWriter sw = null;
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    com.SendTxBuffer(terminalTx.Text +"\r\n");
                    System.Threading.Thread.Sleep(300);
                    terminalRx.AppendText( terminalTx.Text );
                    rx = com.ReadRxBuffer();
                    if (terminalTx.Text.Contains("r") && !rx.Contains("no"))
                    {
                        do
                        {
                            rx += com.ReadRxBuffer();
                        } while (!rx.Contains("@"));
                        sw = new StreamWriter(@"c:\log\" + logFileName.Text );
                        sw.Write(rx);
                        sw.Close();
                        DrawGraph(rx);
                    }
                    terminalRx.AppendText(rx + "\r\n");
                    terminalRx.SelectionStart = terminalRx.TextLength;
                    terminalRx.ScrollToCaret();
                    terminalTx.Text = "";
                    break;
            }
        }

        private void DrawGraph(string data)
        {

            string[] lines = Regex.Split(data,"\r\n");
            chart1.Series["shot"].Points.Clear();
            for( int i=0;i<lines.Count();i++)
            {
                int val = 0;
                int.TryParse(lines[i], out val);
                float real = ((float)5.0 / 255) * (float)val;
                chart1.Series["shot"].Points.AddXY(i, real);
            }

            chart1.Series["shot"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.FastLine;
        }
    }
}
