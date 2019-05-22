namespace KelimeEzberlemeProje
{
    partial class Istatistikler
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Istatistikler));
            this.chartGrafik = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pcBack = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.chartGrafik)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcBack)).BeginInit();
            this.SuspendLayout();
            // 
            // chartGrafik
            // 
            chartArea1.Name = "ChartArea1";
            this.chartGrafik.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartGrafik.Legends.Add(legend1);
            this.chartGrafik.Location = new System.Drawing.Point(-16, 47);
            this.chartGrafik.Name = "chartGrafik";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Ezberlenenler";
            series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.UInt32;
            this.chartGrafik.Series.Add(series1);
            this.chartGrafik.Size = new System.Drawing.Size(530, 340);
            this.chartGrafik.TabIndex = 0;
            // 
            // pcBack
            // 
            this.pcBack.Image = ((System.Drawing.Image)(resources.GetObject("pcBack.Image")));
            this.pcBack.Location = new System.Drawing.Point(5, 15);
            this.pcBack.Name = "pcBack";
            this.pcBack.Size = new System.Drawing.Size(28, 26);
            this.pcBack.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pcBack.TabIndex = 8;
            this.pcBack.TabStop = false;
            this.pcBack.Click += new System.EventHandler(this.pcBack_Click);
            // 
            // Istatistikler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 395);
            this.Controls.Add(this.pcBack);
            this.Controls.Add(this.chartGrafik);
            this.Name = "Istatistikler";
            this.Load += new System.EventHandler(this.Istatistikler_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartGrafik)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pcBack)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartGrafik;
        private System.Windows.Forms.PictureBox pcBack;
    }
}