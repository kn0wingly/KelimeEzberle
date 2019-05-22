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
    public partial class AnaMenu : MetroForm
    {
        public AnaMenu()
        {
            InitializeComponent();
        }

        private void btnKelimeEzberle_Click(object sender, EventArgs e)
        {
            KelimeEzberle frmKelimeEzberle = new KelimeEzberle();
            this.Hide();
            frmKelimeEzberle.Show();
        }

        private void btnKelimeEkle_Click(object sender, EventArgs e)
        {
            KelimeEkle frmKelimeEkle = new KelimeEkle();
            this.Hide();
            frmKelimeEkle.Show();
        }

        private void btnIstatistik_Click(object sender, EventArgs e)
        {
            Istatistikler frmIstatistik = new Istatistikler();
            this.Hide();
            frmIstatistik.Show();
        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AnaMenu_Load(object sender, EventArgs e)
        {
            Kelime k = DB_Kelime.kelimeCek(1);
            MessageBox.Show(k.ing);
        }
    }
}