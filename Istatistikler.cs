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
    public partial class Istatistikler : MetroForm
    {
        public Istatistikler()
        {
            InitializeComponent();
        }

        private void Istatistikler_Load(object sender, EventArgs e)
        {
            grafigiTemizle();

            grafikBilgileriDoldur();
        }

        private void grafigiTemizle()
        {
            foreach (var series in chartGrafik.Series)
                series.Points.Clear();
        }

        private void grafikAylariYerlestir()
        {
            chartGrafik.Series["Ezberlenenler"].Points[0].AxisLabel = "Ocak";
            chartGrafik.Series["Ezberlenenler"].Points[1].AxisLabel = "Şubat";
            chartGrafik.Series["Ezberlenenler"].Points[2].AxisLabel = "Mart";
            chartGrafik.Series["Ezberlenenler"].Points[3].AxisLabel = "Nisan";
            chartGrafik.Series["Ezberlenenler"].Points[4].AxisLabel = "Mayıs";
            chartGrafik.Series["Ezberlenenler"].Points[5].AxisLabel = "Haziran";
            chartGrafik.Series["Ezberlenenler"].Points[6].AxisLabel = "Temmuz";
            chartGrafik.Series["Ezberlenenler"].Points[7].AxisLabel = "Ağustos";
            chartGrafik.Series["Ezberlenenler"].Points[8].AxisLabel = "Eylül";
            chartGrafik.Series["Ezberlenenler"].Points[9].AxisLabel = "Ekim";
            chartGrafik.Series["Ezberlenenler"].Points[10].AxisLabel = "Kasım";
            chartGrafik.Series["Ezberlenenler"].Points[11].AxisLabel = "Aralık";
            chartGrafik.ChartAreas[0].AxisX.LabelStyle.Angle = -65;
        }
        
        private void grafikBilgileriDoldur()
        {
            int[] ogrenilenSayilari = DB_Main.ezberlenenKelimeSayilariCek();

            foreach (int i in ogrenilenSayilari)
            {
                chartGrafik.Series["Ezberlenenler"].Points.Add(i);
            }

            grafikAylariYerlestir();
        }
        
        private void pcBack_Click(object sender, EventArgs e)
        {
            AnaMenu anaMenu = new AnaMenu();
            this.Close();
            anaMenu.Show();
        }
    }
}
