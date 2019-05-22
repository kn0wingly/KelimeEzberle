using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KelimeEzberlemeProje
{
    public partial class KelimeEkle : MetroForm
    {
        public KelimeEkle()
        {
            InitializeComponent();
        }

        private void KelimeEkle_Load(object sender, EventArgs e)
        {

        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (txtTr.Text == "" || 
                txtIng.Text == "" || 
                cmbTur.Text == "" || 
                txtTrCumle.Text == "" ||
                txtIngCumle.Text == "")
            {
                MessageBox.Show("Herhangi bir alan boş bırakılamaz!");
                return;
            }

            kelimeEkle();
        }

        private void kelimeEkle()
        {
            Kelime k = new Kelime();
            k.tr = txtTr.Text.Trim();
            k.ing = txtIng.Text.Trim();
            k.tur = cmbTur.Text;
            k.trCumle = txtTrCumle.Text;
            k.ingCumle = txtIngCumle.Text;

            int eklenmeDurumu = DB_Kelime.kelimeEkle(k);

            if (eklenmeDurumu == 1)
                MessageBox.Show("Kelime eklendi!");
            else if (eklenmeDurumu == 0)
                MessageBox.Show("Kelime zaten mevcut!");
            else
                MessageBox.Show("Kelime eklenemedi!");
        }

        private void pcBack_Click(object sender, EventArgs e)
        {
            AnaMenu anaMenu = new AnaMenu();
            this.Close();
            anaMenu.Show();
        }
    }
}
