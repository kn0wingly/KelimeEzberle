using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using MySql.Data;

namespace KelimeEzberlemeProje
{
    class DB_Main
    {
        public static MySqlConnection con { get; private set; }
        private static MySqlCommand cmd = new MySqlCommand();
        private static MySqlDataReader reader;

        public static bool dbBaglan()
        {
            con = new MySqlConnection("Database=kelime_ezberle;Uid=root;Pwd=;");
        
            try
            {
                con.Open();
                
                if (con.State != System.Data.ConnectionState.Closed)
                    return true;

                else
                    return false;
                
            }
            catch 
            {
                return false;
            }
        }

        public static bool dbBaglantiKes()
        {
            try
            {
                con.Close();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static int sonKelimeIdCek()
        {
            int sonId = -1;

            dbBaglan();
            cmd.CommandText = "SELECT id FROM kelimeler ORDER BY id DESC LIMIT 1";
            cmd.Connection = con;

            reader = cmd.ExecuteReader();
            while (reader.Read())
                sonId = Convert.ToInt32(reader["id"]);

            reader.Close();
            dbBaglantiKes();

            return sonId;
        }

        public static int[] prVeDoneTumIdleriCek()
        {
            int prKayitSayisi = prKayitSayisiBelirle();
            int doneKayitSayisi = doneKayitSayisiBelirle();
            int[] prVeDoneIdler = new int[prKayitSayisi + doneKayitSayisi];
            int[] prIdler = prTumIdleriCek();
            int[] doneIdler = doneTumIdleriCek();

            int i = 0;
            for (i = 0; i < prKayitSayisi; i++)
                prVeDoneIdler[i] = prIdler[i];

            int j = 0;
            for (i = prKayitSayisi; i < prKayitSayisi + doneKayitSayisi; i++)
            {
                prVeDoneIdler[i] = doneIdler[j];
                j++;
            }

            return prVeDoneIdler;
        }

        private static int[] prTumIdleriCek()
        {
            int[] progressIds = new int[prKayitSayisiBelirle()];

            dbBaglan();
            cmd.Connection = con;
            cmd.CommandText = "SELECT kelime_id from in_progress";

            reader = cmd.ExecuteReader();
            int i = 0;
            while (reader.Read())
            {
                progressIds[i] = Convert.ToInt32(reader["kelime_id"]);
                i++;
            }

            reader.Close();
            dbBaglantiKes();

            return progressIds;
        }

        private static int prKayitSayisiBelirle()
        {
            int elemanSayisi = -1;

            dbBaglan();
            cmd.Connection = con;
            cmd.CommandText = "SELECT count(*) from in_progress";

            elemanSayisi = Convert.ToInt32(cmd.ExecuteScalar());

            dbBaglantiKes();

            return elemanSayisi;
        }

        private static int[] doneTumIdleriCek()
        {
            int[] doneIds = new int[doneKayitSayisiBelirle()];

            dbBaglan();
            cmd.Connection = con;
            cmd.CommandText = "SELECT kelime_id FROM done";

            reader = cmd.ExecuteReader();
            int i = 0;
            while (reader.Read())
            {
                doneIds[i] = Convert.ToInt32(reader["kelime_id"]);
                i++;
            }

            reader.Close();
            dbBaglantiKes();

            return doneIds;
        }

        private static int doneKayitSayisiBelirle()
        {
            int elemanSayisi = -1;

            dbBaglan();
            cmd.Connection = con;
            cmd.CommandText = "SELECT count(*) FROM done";

            elemanSayisi = Convert.ToInt32(cmd.ExecuteScalar());

            dbBaglantiKes();

            return elemanSayisi;
        }

        public static bool ilerlemeyiKaydet(int kelimeId)
        {
            bool progressteMi = prTablosundaMi(kelimeId);
            bool islemBasariliMi = false;

            if (!progressteMi)
                islemBasariliMi = prIlkKayit(kelimeId);
            else
                islemBasariliMi = prLevelArttir(kelimeId);

            return islemBasariliMi;
        }

        public static bool prIlkKayit(int kelimeId)
        {
            bool islemBasariliMi = false;

            DateTime guncelTarih = DateTime.Now;
            DateTime sorulacakTarihD = guncelTarih.AddDays(1);
            string sorulacakTarih = sorulacakTarihD.ToString("yyyyMMdd");

            dbBaglan();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO in_progress (kelime_id, ogrenme_level, sorulacak_tarih) " +
                              "VALUES (" + kelimeId + ", 1," + sorulacakTarih + ")";    

            if (cmd.ExecuteNonQuery() == 1)
                islemBasariliMi = true;

            dbBaglantiKes();

            return islemBasariliMi;
        }

        private static bool prLevelArttir(int kelimeId)
        {
            int guncelLevel = prLevelCek(kelimeId);
            int yeniLevel = guncelLevel + 1;
            DateTime guncelTarih = DateTime.Now;
            DateTime sorulacakTarih = guncelTarih;

            switch(guncelLevel)
            {
                case 1:
                    sorulacakTarih = guncelTarih.AddDays(7);
                    break;
                case 2:
                    sorulacakTarih = guncelTarih.AddDays(30);
                    break;
                case 3:
                    sorulacakTarih = guncelTarih.AddDays(180);
                    break;
                case 4:
                    prTablosundanCikar(kelimeId);
                    doneTablosunaEkle(kelimeId); 
                    return true;
            }

            bool islemBasariliMi = prSatirDuzenle(kelimeId, yeniLevel, sorulacakTarih);

            return islemBasariliMi;
        }

        public static int prLevelCek(int kelimeId)
        {
            dbBaglan();
            cmd.Connection = con;
            cmd.CommandText = "SELECT * FROM in_progress WHERE kelime_id = " + kelimeId;
            reader = cmd.ExecuteReader();

            int level = -1;
            while (reader.Read())
                level = Convert.ToInt32(reader["ogrenme_level"]);
               
            reader.Close();
            dbBaglantiKes();

            return level;
        }

        private static bool prSatirDuzenle(int kelimeId, int yeniLevel, DateTime sorulacakTarih)
        {
            bool islemBasariliMi = false;
            
            dbBaglan();
            cmd.Connection = con;
            cmd.CommandText = "UPDATE in_progress SET ogrenme_level=" + yeniLevel 
                              + ",sorulacak_tarih=" + sorulacakTarih.ToString("yyyyMMdd")
                              + " WHERE kelime_id=" + kelimeId;

            if (cmd.ExecuteNonQuery() == 1)
                islemBasariliMi = true;

            dbBaglantiKes();
            return islemBasariliMi;
        }

        public static bool prTablosundanCikar(int kelimeId)
        {
            bool islemBasariliMi = false;

            dbBaglan();
            cmd.Connection = con;
            cmd.CommandText = "DELETE FROM in_progress WHERE kelime_id =" + kelimeId;

            if (cmd.ExecuteNonQuery() == 1)
                islemBasariliMi = true;

            dbBaglantiKes();
            
            return islemBasariliMi;
        }

        public static bool doneTablosunaEkle(int kelimeId)
        {
            bool islemBasariliMi = false;
            dbBaglan();
            cmd.Connection = con;
            cmd.CommandText = "INSERT INTO done(kelime_id, ogrenildigi_tarih)" +
                              "VALUES (" + kelimeId + ",curdate())";

            if (cmd.ExecuteNonQuery() == 1)
                islemBasariliMi = true;

            dbBaglantiKes();
            return islemBasariliMi;
        }

        public static int prSorulacakKelimeSayisiBelirle()
        {
            dbBaglan();
            cmd.Connection = con;
            cmd.CommandText = "SELECT count(*) FROM in_progress " +
                              "WHERE sorulacak_tarih = curdate()";

            int elemanSayisi = Convert.ToInt32(cmd.ExecuteScalar());

            dbBaglantiKes();

            return elemanSayisi;
        }

        public static int[] prSorulacakIdleriCek()
        {
            int kelimeSayisi = prSorulacakKelimeSayisiBelirle();
            int[] progressIdler = new int[kelimeSayisi];

            if (kelimeSayisi != 0)
            {
                dbBaglan();
                cmd.Connection = con;
                cmd.CommandText = "SELECT kelime_id FROM in_progress WHERE sorulacak_tarih = CURRENT_DATE";
                reader = cmd.ExecuteReader();
                
                int i = 0;
                while (reader.Read())
                {
                    progressIdler[i] = Convert.ToInt32(reader["kelime_id"]);
                    i++;
                }

                reader.Close();
                dbBaglantiKes();
            }

            return progressIdler;
        }

        public static bool prTablosundaMi(int kelimeId)
        {
            bool mevcutMu = false;

            dbBaglan();
            cmd.Connection = con;
            cmd.CommandText = "SELECT kelime_id FROM in_progress WHERE kelime_id = " + kelimeId;

            int Count = Convert.ToInt32(cmd.ExecuteScalar());

            if (Count != 0)
                mevcutMu = true;
            
            dbBaglantiKes();

            return mevcutMu;
        }

        public static int[] ezberlenenKelimeSayilariCek()
        {
            int kelimeSayisi = doneKelimeSayisiBelirle();
            int[] ogrenilenSayilari = new int[12];

            if (kelimeSayisi != 0)
            {
                dbBaglan();
                cmd.Connection = con;
                cmd.CommandText = "SELECT ogrenildigi_tarih FROM done";
                                  
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string ay = Convert.ToDateTime(reader["ogrenildigi_tarih"]).ToString("MM");
                    int ay2 = Convert.ToInt32(ay);
                    ogrenilenSayilari[ay2 - 1]++;
                }

                reader.Close();
                dbBaglantiKes();
            }

            return ogrenilenSayilari;
        }

        public static int doneKelimeSayisiBelirle()
        {
            dbBaglan();
            cmd.Connection = con;
            cmd.CommandText = "SELECT count(*) FROM done";

            int elemanSayisi = Convert.ToInt32(cmd.ExecuteScalar());

            dbBaglantiKes();

            return elemanSayisi;
        }
    }
}