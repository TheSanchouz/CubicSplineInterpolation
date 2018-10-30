using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CubicSplineInterpolation
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            chart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
            chart.ChartAreas[0].AxisX.Title = "X";
            chart.ChartAreas[0].AxisY.Title = "Y";

            double x = 0.001;
            const int N = 1000;
            for (int i = 1; i < N; i++)
            {
                double y = (x * x - 0.3) * (x - 0.3);
                chart.Series[0].Points.AddXY(x, y);
                if (i % 100 == 0)
                {
                    System.Windows.Forms.DataVisualization.Charting.DataPoint dataPoint = chart.Series[0].Points[i - 1];
                    dataPoint.Color = Color.Red;
                    dataPoint.MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
                    dataPoint.MarkerSize = 5;
                }
                x = x + 0.001;
            }
        }
       
    }
}
