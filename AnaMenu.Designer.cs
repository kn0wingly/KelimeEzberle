namespace KelimeEzberlemeProje
{
    partial class AnaMenu
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
            this.btnCikis = new MetroFramework.Controls.MetroButton();
            this.btnKelimeEzberle = new MetroFramework.Controls.MetroButton();
            this.btnKelimeEkle = new MetroFramework.Controls.MetroButton();
            this.btnIstatistik = new MetroFramework.Controls.MetroButton();
            this.SuspendLayout();
            // 
            // btnCikis
            // 
            this.btnCikis.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCikis.Location = new System.Drawing.Point(178, 297);
            this.btnCikis.Name = "btnCikis";
            this.btnCikis.Size = new System.Drawing.Size(132, 44);
            this.btnCikis.TabIndex = 1;
            this.btnCikis.Text = "Çıkış";
            this.btnCikis.UseSelectable = true;
            this.btnCikis.Click += new System.EventHandler(this.btnCikis_Click);
            // 
            // btnKelimeEzberle
            // 
            this.btnKelimeEzberle.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.btnKelimeEzberle.Location = new System.Drawing.Point(178, 93);
            this.btnKelimeEzberle.Name = "btnKelimeEzberle";
            this.btnKelimeEzberle.Size = new System.Drawing.Size(132, 44);
            this.btnKelimeEzberle.TabIndex = 2;
            this.btnKelimeEzberle.Text = "Kelime Ezberle";
            this.btnKelimeEzberle.UseSelectable = true;
            this.btnKelimeEzberle.Click += new System.EventHandler(this.btnKelimeEzberle_Click);
            // 
            // btnKelimeEkle
            // 
            this.btnKelimeEkle.Location = new System.Drawing.Point(178, 159);
            this.btnKelimeEkle.Name = "btnKelimeEkle";
            this.btnKelimeEkle.Size = new System.Drawing.Size(132, 44);
            this.btnKelimeEkle.TabIndex = 3;
            this.btnKelimeEkle.Text = "Kelime Ekle";
            this.btnKelimeEkle.UseSelectable = true;
            this.btnKelimeEkle.Click += new System.EventHandler(this.btnKelimeEkle_Click);
            // 
            // btnIstatistik
            // 
            this.btnIstatistik.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnIstatistik.Location = new System.Drawing.Point(178, 227);
            this.btnIstatistik.Name = "btnIstatistik";
            this.btnIstatistik.Size = new System.Drawing.Size(132, 44);
            this.btnIstatistik.TabIndex = 1;
            this.btnIstatistik.Text = "İstatistikler";
            this.btnIstatistik.UseSelectable = true;
            this.btnIstatistik.Click += new System.EventHandler(this.btnIstatistik_Click);
            // 
            // AnaMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 395);
            this.Controls.Add(this.btnIstatistik);
            this.Controls.Add(this.btnCikis);
            this.Controls.Add(this.btnKelimeEzberle);
            this.Controls.Add(this.btnKelimeEkle);
            this.Name = "AnaMenu";
            this.Resizable = false;
            this.Text = "Ana Menü";
            this.TextAlign = MetroFramework.Forms.MetroFormTextAlign.Center;
            this.Load += new System.EventHandler(this.AnaMenu_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroButton btnCikis;
        private MetroFramework.Controls.MetroButton btnKelimeEzberle;
        private MetroFramework.Controls.MetroButton btnKelimeEkle;
        private MetroFramework.Controls.MetroButton btnIstatistik;
    }
}