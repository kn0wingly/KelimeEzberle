using MetroFramework.Forms;
using MySql.Data.MySqlClient;
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
    public partial class KelimeEzberle : MetroForm
    {
        public KelimeEzberle()
        {
            InitializeComponent();
        }

        Kelime[] kelimeler;
        int kelimeIndeks = 0;
        int prSorulacaklarSayisi = DB_Main.prSorulacakKelimeSayisiBelirle();

        private void KelimeEzberle_Load(object sender, EventArgs e)
        {
            kelimeler = new Kelime[Consts.OGRENILECEK_KELIME_SAYISI + prSorulacaklarSayisi];
            
            ogrenilecekSorulariEkle();
            progressSorulacaklariEkle();
            
            kelimeyiGoster(kelimeler[kelimeIndeks]);
        }

        private void ogrenilecekSorulariEkle()
        {
            Kelime[] ogrenilecekKelimeler = DB_Kelime.kelimeBelirle();

            for (int i = 0; i < ogrenilecekKelimeler.Length; i++)
            {
                kelimeler[i] = ogrenilecekKelimeler[i];
            }
        }

        private void progressSorulacaklariEkle()
        {
            int progressKelimeSayisi = DB_Main.prSorulacakKelimeSayisiBelirle();
            Kelime[] progressKelimeler = DB_Kelime.progressSorulacakKelimeleriCek();

            int j = 0;
            for (int i = Consts.OGRENILECEK_KELIME_SAYISI; i < progressKelimeSayisi + Consts.OGRENILECEK_KELIME_SAYISI; i++)
            {
                kelimeler[i] = progressKelimeler[j];
                j++;
            }
        }

        private void kelimeyiGoster(Kelime kelime)
        {
            lblTurTr.Text = kelime.tr + " (" + kelime.tur + ")";
            lblIng.Text = kelime.ing;
            lblTrCumle.Text = kelime.trCumle;
            lblIngCumle.Text = kelime.ingCumle;
        }

        private void pcNext_Click(object sender, EventArgs e)
        {
            kelimeIndeks++;
            kelimeyiGoster(kelimeler[kelimeIndeks]);

            if (kelimeIndeks == Consts.OGRENILECEK_KELIME_SAYISI - 1)
            {
                pcNextWord.Visible = false;
                btnTesteGec.Visible = true;
            }
        }

        private void btnTesteGec_Click(object sender, EventArgs e)
        {
            kelimeIndeks = 0;
            soruyuGoster(kelimeler[kelimeIndeks]);

            pnlOgren.Visible = false;
            pnlTest.Visible = true;
        }

        private void soruyuGoster(Kelime kelime)
        {
            lblTestIng.Text = kelime.ing;
            lblTestIngCumle.Text = kelime.ingCumle;
        }

        private void pcNextQue_Click(object sender, EventArgs e)
        {
            cevapKontrol();
            txtCevap.Text = "";
            kelimeIndeks++;
            soruyuGoster(kelimeler[kelimeIndeks]);

            if (kelimeIndeks == prSorulacaklarSayisi + Consts.OGRENILECEK_KELIME_SAYISI - 1)
            {
                pcNextQue.Visible = false;
                btnTestBitir.Visible = true;
            }
        }

        private void cevapKontrol()
        {
            if (kelimeler[kelimeIndeks].tr.Equals(txtCevap.Text.Trim().ToLower()))
            {
                DB_Main.ilerlemeyiKaydet(kelimeler[kelimeIndeks].id);
                MessageBox.Show("Tebrikler! Doğru cevap!");
            }
            else
            {
                if (DB_Main.prTablosundaMi(kelimeler[kelimeIndeks].id))
                {
                    int level = DB_Main.prLevelCek(kelimeler[kelimeIndeks].id);
                    DB_Main.prTablosundanCikar(kelimeler[kelimeIndeks].id);

                    if (level != 1)
                        DB_Main.prIlkKayit(kelimeler[kelimeIndeks].id);
                }

                MessageBox.Show("Yanlış cevap!");
            }
        }

        private void btnTestBitir_Click(object sender, EventArgs e)
        {
            cevapKontrol();
            AnaMenu anaMenu = new AnaMenu();
            this.Close();
            anaMenu.Show();
        }

        private void pcBack_Click(object sender, EventArgs e)
        {
            AnaMenu anaMenu = new AnaMenu();
            this.Close();
            anaMenu.Show();
        }
    }
}