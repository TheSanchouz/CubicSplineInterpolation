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
        CubicSplineInterpolation interpolation = null;

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

            //chart.ChartAreas[0].AxisX.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.FixedCount;
            //chart.ChartAreas[0].AxisY.IntervalAutoMode = System.Windows.Forms.DataVisualization.Charting.IntervalAutoMode.FixedCount;
                
            //chart.ChartAreas[0].AxisX.LabelStyle.Format = "#";
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

                for (int i = 0; i < interpolation.size; i++)
                {
                    chart.Series.Add("Spline " + (i + 1).ToString());
                    chart.Series["Spline " + (i + 1).ToString()].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;

                    double dx = (interpolation[i].xRight - interpolation[i].xLeft) * 0.01;

                    for (double j = interpolation[i].xLeft; j <= interpolation[i].xRight; j += dx)
                    {
                        chart.Series["Spline " + (i + 1).ToString()].Points.AddXY(Math.Round(j + 0.01, 2), interpolation[i].Function(j));
                        Console.WriteLine($"{Math.Round(j, 2)}");
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

                //chart.Series.Add("Spline");
                //chart.Series["Spline"].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;
                //for (int i = 0; i < interpolation.size + 1; i++)
                //{
                //    chart.Series["Spline"].Points.AddXY(x[i], y[i]);
                //}
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

        private void buttonSave_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",
                Title = "Save an Interpolation results"
            };
            saveFileDialog.ShowDialog();

            if (saveFileDialog.FileName != "")
            {
                interpolation.SaveInterpolation(saveFileDialog.FileName);
            }
        }

        private void buttonOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*",
            };

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                interpolation = new CubicSplineInterpolation(openFileDialog.FileName);
                System.Windows.Forms.DataVisualization.Charting.DataPoint[] dataPoint =
                    new System.Windows.Forms.DataVisualization.Charting.DataPoint[interpolation.size + 1];

                chart.Series.Clear();

                for (int i = 0; i < interpolation.size; i++)
                {
                    chart.Series.Add("Spline " + (i + 1).ToString());
                    chart.Series["Spline " + (i + 1).ToString()].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Spline;

                    double dx = (interpolation[i].xRight - interpolation[i].xLeft) * 0.01;

                    for (double j = interpolation[i].xLeft; j <= interpolation[i].xRight; j += dx)
                    {
                        chart.Series["Spline " + (i + 1).ToString()].Points.AddXY(Math.Round(j + 0.01, 2), interpolation[i].Function((double)j));
                        //Console.WriteLine($"{j}");
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
    }
}
