namespace Villain
{
    partial class LaporanStatistikVilla
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
            this.chartVillain = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBoxLaporan = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBoxTahun = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnBack = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.chartVillain)).BeginInit();
            this.SuspendLayout();
            // 
            // chartVillain
            // 
            chartArea1.Name = "ChartArea1";
            this.chartVillain.ChartAreas.Add(chartArea1);
            legend1.Name = "Legend1";
            this.chartVillain.Legends.Add(legend1);
            this.chartVillain.Location = new System.Drawing.Point(12, 123);
            this.chartVillain.Name = "chartVillain";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartVillain.Series.Add(series1);
            this.chartVillain.Size = new System.Drawing.Size(776, 297);
            this.chartVillain.TabIndex = 0;
            this.chartVillain.Text = "chart1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Impact", 28.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(160, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(468, 59);
            this.label1.TabIndex = 1;
            this.label1.Text = "Laporan Statistik Villa";
            // 
            // comboBoxLaporan
            // 
            this.comboBoxLaporan.FormattingEnabled = true;
            this.comboBoxLaporan.Location = new System.Drawing.Point(100, 88);
            this.comboBoxLaporan.Name = "comboBoxLaporan";
            this.comboBoxLaporan.Size = new System.Drawing.Size(334, 24);
            this.comboBoxLaporan.TabIndex = 2;
            this.comboBoxLaporan.SelectedIndexChanged += new System.EventHandler(this.comboBoxLaporan_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Pilih Laporan";
            // 
            // comboBoxTahun
            // 
            this.comboBoxTahun.FormattingEnabled = true;
            this.comboBoxTahun.Location = new System.Drawing.Point(677, 88);
            this.comboBoxTahun.Name = "comboBoxTahun";
            this.comboBoxTahun.Size = new System.Drawing.Size(111, 24);
            this.comboBoxTahun.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(598, 91);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Pilih Tahun";
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(713, 422);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(75, 23);
            this.btnBack.TabIndex = 6;
            this.btnBack.Text = "Back";
            this.btnBack.UseVisualStyleBackColor = true;
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // LaporanStatistikVilla
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnBack);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.comboBoxTahun);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBoxLaporan);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.chartVillain);
            this.Name = "LaporanStatistikVilla";
            this.Text = "Laporan Statistik Villa";
            this.Load += new System.EventHandler(this.LaporanStatistikVilla_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chartVillain)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartVillain;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxLaporan;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBoxTahun;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnBack;
    }
}