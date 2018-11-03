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
        CubicSplineInterpolation interpolation;

        //double[] x = { -3, -1, 1, 3 };
        //double[] y = { 4, 2, 2, 4 };

        //double[] x = new double[5] { 0, 1 / 4, 1.0 / 2, 3.0 / 4, 1};
        //double[] y = new double[5] { 1, 2, 1, 0, 1 };

        //double[] x = new double[5] { 0, 1, 2, 4, 8};
        //double[] y = new double[5] { 0, 1, 4, 16, 64 };

        public Form()
        {
            InitializeComponent(); 
        }

        private void Form_Load(object sender, EventArgs e)
        {
            dataGridView.Columns.Clear();
            dataGridViewVars.Columns.Clear();

            dataGridView.Columns.Add("X", "X");
            dataGridView.Columns.Add("Y", "Y");

            dataGridViewVars.Columns.Add("X", "X");
            dataGridViewVars.Columns.Add("A", "A");
            dataGridViewVars.Columns.Add("B", "B");
            dataGridViewVars.Columns.Add("C", "C");
            dataGridViewVars.Columns.Add("D", "D");

            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            foreach (DataGridViewColumn column in dataGridViewVars.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dataGridView.RowCount = 8;
            dataGridViewVars.RowCount = 7;

            chart.ChartAreas[0].AxisX.Title = "X";
            chart.ChartAreas[0].AxisY.Title = "Y";
        }

        private void buttonInteprolate_Click(object sender, EventArgs e)
        {
            double[] tmpX = new double[8];
            double[] tmpY = new double[8];
            int size = 0;

            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                if (row.Cells[0].Value != null && row.Cells[1].Value != null)
                {
                    row.HeaderCell.Value = (size + 1).ToString();

                    tmpX[size] = Convert.ToDouble(row.Cells[0].Value);
                    tmpY[size] = Convert.ToDouble(row.Cells[1].Value);

                    size++;
                }
            }

            if (size == 0 || size == 1)
            {
                MessageBox.Show(
                    "Введите хотя бы 2 точки",
                    "Предупреждение",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1,
                    MessageBoxOptions.DefaultDesktopOnly); 
            }
            else
            {
                double[] x = new double[size];
                double[] y = new double[size];

                for (int i = 0; i < size; i++)
                {
                    x[i] = tmpX[i];
                    y[i] = tmpY[i];
                }


                interpolation = new CubicSplineInterpolation(x, y);
                System.Windows.Forms.DataVisualization.Charting.DataPoint[] dataPoint =
                    new System.Windows.Forms.DataVisualization.Charting.DataPoint[interpolation.size + 1];

                chart.Series.Clear();

                decimal dx = 0.01m;

                for (int i = 0; i < interpolation.size; i++)
                {
                    chart.Series.Add("Spline " + (i + 1).ToString());
                    chart.Series["Spline " + (i + 1).ToString()].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;

                    for (decimal j = (decimal)interpolation[i].xLeft; j <= (decimal)interpolation[i].xRight; j += dx)
                    {
                        chart.Series["Spline " + (i + 1).ToString()].Points.AddXY(j, interpolation[i].Function((double)j));
                        Console.WriteLine($"{j}");
                    }


                    dataPoint[i] = chart.Series["Spline " + (i + 1).ToString()].Points[0];
                    dataPoint[i].Color = Color.Black;
                    dataPoint[i].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
                    dataPoint[i].MarkerSize = 5;

                    dataGridViewVars.Rows[i].HeaderCell.Value = (i + 1).ToString();
                    dataGridViewVars.Rows[i].Cells[0].Value = interpolation[i].xLeft;
                    dataGridViewVars.Rows[i].Cells[1].Value = interpolation[i].a;
                    dataGridViewVars.Rows[i].Cells[2].Value = interpolation[i].b;
                    dataGridViewVars.Rows[i].Cells[3].Value = interpolation[i].c;
                    dataGridViewVars.Rows[i].Cells[4].Value = interpolation[i].d;
                }


                dataPoint[interpolation.size] = chart.Series["Spline " + (interpolation.size).ToString()].Points.Last();
                dataPoint[interpolation.size].Color = Color.Black;
                dataPoint[interpolation.size].MarkerStyle = System.Windows.Forms.DataVisualization.Charting.MarkerStyle.Circle;
                dataPoint[interpolation.size].MarkerSize = 5;


            }   
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            dataGridView.Columns.Clear();
            dataGridViewVars.Columns.Clear();

            dataGridView.Columns.Add("X", "X");
            dataGridView.Columns.Add("Y", "Y");

            dataGridViewVars.Columns.Add("X", "X");
            dataGridViewVars.Columns.Add("A", "A");
            dataGridViewVars.Columns.Add("B", "B");
            dataGridViewVars.Columns.Add("C", "C");
            dataGridViewVars.Columns.Add("D", "D");

            foreach (DataGridViewColumn column in dataGridView.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            foreach (DataGridViewColumn column in dataGridViewVars.Columns)
            {
                column.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dataGridView.RowCount = 8;
            dataGridViewVars.RowCount = 7;

            chart.Series.Clear();
        }
    }
}
