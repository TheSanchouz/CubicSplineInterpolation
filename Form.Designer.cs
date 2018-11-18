namespace CubicSplineInterpolation
{
    partial class Form
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend2 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.buttonInteprolate = new System.Windows.Forms.Button();
            this.buttonClear = new System.Windows.Forms.Button();
            this.dataGridViewVars = new System.Windows.Forms.DataGridView();
            this.buttonSavePic = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVars)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.AllowUserToResizeColumns = false;
            this.dataGridView.AllowUserToResizeRows = false;
            this.dataGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.Location = new System.Drawing.Point(1073, 12);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 24;
            this.dataGridView.Size = new System.Drawing.Size(440, 265);
            this.dataGridView.TabIndex = 0;
            // 
            // chart
            // 
            this.chart.AntiAliasing = System.Windows.Forms.DataVisualization.Charting.AntiAliasingStyles.Graphics;
            chartArea2.Name = "ChartArea1";
            this.chart.ChartAreas.Add(chartArea2);
            legend2.Name = "Legend1";
            this.chart.Legends.Add(legend2);
            this.chart.Location = new System.Drawing.Point(12, 12);
            this.chart.Name = "chart";
            this.chart.Size = new System.Drawing.Size(1021, 761);
            this.chart.TabIndex = 1;
            this.chart.Text = "chart";
            this.chart.TextAntiAliasingQuality = System.Windows.Forms.DataVisualization.Charting.TextAntiAliasingQuality.SystemDefault;
            // 
            // buttonInteprolate
            // 
            this.buttonInteprolate.Location = new System.Drawing.Point(1073, 283);
            this.buttonInteprolate.Name = "buttonInteprolate";
            this.buttonInteprolate.Size = new System.Drawing.Size(211, 42);
            this.buttonInteprolate.TabIndex = 2;
            this.buttonInteprolate.Text = "Интерполировать";
            this.buttonInteprolate.UseVisualStyleBackColor = true;
            this.buttonInteprolate.Click += new System.EventHandler(this.buttonInteprolate_Click);
            // 
            // buttonClear
            // 
            this.buttonClear.Location = new System.Drawing.Point(1378, 283);
            this.buttonClear.Name = "buttonClear";
            this.buttonClear.Size = new System.Drawing.Size(135, 42);
            this.buttonClear.TabIndex = 3;
            this.buttonClear.Text = "Очистить";
            this.buttonClear.UseVisualStyleBackColor = true;
            this.buttonClear.Click += new System.EventHandler(this.buttonClear_Click);
            // 
            // dataGridViewVars
            // 
            this.dataGridViewVars.AllowUserToAddRows = false;
            this.dataGridViewVars.AllowUserToDeleteRows = false;
            this.dataGridViewVars.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewVars.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewVars.Location = new System.Drawing.Point(1073, 538);
            this.dataGridViewVars.Name = "dataGridViewVars";
            this.dataGridViewVars.ReadOnly = true;
            this.dataGridViewVars.RowTemplate.Height = 24;
            this.dataGridViewVars.Size = new System.Drawing.Size(440, 235);
            this.dataGridViewVars.TabIndex = 4;
            // 
            // buttonSavePic
            // 
            this.buttonSavePic.Location = new System.Drawing.Point(1073, 481);
            this.buttonSavePic.Name = "buttonSavePic";
            this.buttonSavePic.Size = new System.Drawing.Size(119, 51);
            this.buttonSavePic.TabIndex = 5;
            this.buttonSavePic.Text = "Сохранить график";
            this.buttonSavePic.UseVisualStyleBackColor = true;
            this.buttonSavePic.Click += new System.EventHandler(this.buttonSavePic_Click);
            // 
            // buttonSave
            // 
            this.buttonSave.Location = new System.Drawing.Point(1394, 481);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(119, 51);
            this.buttonSave.TabIndex = 6;
            this.buttonSave.Text = "Сохранить в файл";
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            // 
            // Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1525, 795);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonSavePic);
            this.Controls.Add(this.dataGridViewVars);
            this.Controls.Add(this.buttonClear);
            this.Controls.Add(this.buttonInteprolate);
            this.Controls.Add(this.chart);
            this.Controls.Add(this.dataGridView);
            this.Name = "Form";
            this.Text = "Cubic Spline Interpolator";
            this.Load += new System.EventHandler(this.Form_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewVars)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.Button buttonInteprolate;
        private System.Windows.Forms.Button buttonClear;
        private System.Windows.Forms.DataGridView dataGridViewVars;
        private System.Windows.Forms.Button buttonSavePic;
        private System.Windows.Forms.Button buttonSave;
    }
}

