using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace KelimeEzberlemeProje
{
    class DB_Kelime
    {
        private static MySqlCommand cmd = new MySqlCommand();
        private static MySqlDataReader reader;

        public static Kelime[] kelimeBelirle()
        {
            DB_Main.dbBaglan();
            cmd.Connection = DB_Main.con;

            int[] belirlenenIdler = randomIdBelirle();
            Kelime[] kelimeler = new Kelime[Consts.OGRENILECEK_KELIME_SAYISI + DB_Main.prSorulacakKelimeSayisiBelirle()];
            
            for (int i = 0; i < Consts.OGRENILECEK_KELIME_SAYISI; i++)
            {
                cmd.CommandText = "SELECT * FROM kelimeler WHERE id = " + belirlenenIdler[i]; 
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                       Kelime kelime = new Kelime();
                       kelime.id = Convert.ToInt32(reader["id"]);
                       kelime.tr = reader["tr"].ToString();
                       kelime.ing = reader["ing"].ToString();
                       kelime.tur = reader["tur"].ToString();
                       kelime.trCumle = reader["trCumle"].ToString();
                       kelime.ingCumle = reader["ingCumle"].ToString();

                       kelimeler[i] = kelime;
                }
                reader.Close();
            }
            DB_Main.dbBaglantiKes();

            return kelimeler;
        }

        private static int[] randomIdBelirle()
        {
            int sonId = DB_Main.sonKelimeIdCek();
            int[] prVeDoneTabloIdler = DB_Main.prVeDoneTumIdleriCek();
            int[] belirlenenIdler = new int[Consts.OGRENILECEK_KELIME_SAYISI];

            Random rand = new Random();
            for (int i = 0; i < Consts.OGRENILECEK_KELIME_SAYISI; i++)
            {
                int id = rand.Next(1, sonId + 1);
                bool idKullanilabilir = idKullanilabilirMi(id, belirlenenIdler, 
                                                           prVeDoneTabloIdler, i);

                if (idKullanilabilir)
                    belirlenenIdler[i] = id;
                else
                    i--;
            }

            return belirlenenIdler;
        }
        
        private static bool idKullanilabilirMi(int id, int[] belirlenenIdler,
                                               int[] prveDoneTabloIdler, int belirlenenIdSayisi)
        {
            int i;

            for (i = 0; i < prveDoneTabloIdler.Length; i++)
                if (id == prveDoneTabloIdler[i])
                    return false;

            for (i = 0; i < belirlenenIdSayisi; i++)
                if (id == belirlenenIdler[i])
                    return false;

            return true;
        }
        
        public static Kelime[] progressSorulacakKelimeleriCek()
        {
            int[] progressIdler = DB_Main.prSorulacakIdleriCek();
            int kelimeSayisi = DB_Main.prSorulacakKelimeSayisiBelirle();

            Kelime[] kelimeler = new Kelime[kelimeSayisi];

            for (int i = 0; i < kelimeSayisi; i++)
                kelimeler[i] = kelimeCek(progressIdler[i]);
            
            return kelimeler;
        }

        public static Kelime kelimeCek(int kelimeId)
        {
            DB_Main.dbBaglan();
            cmd.Connection = DB_Main.con;
            cmd.CommandText = "SELECT * FROM kelimeler WHERE id = " + kelimeId;
            reader = cmd.ExecuteReader();

            Kelime kelime = new Kelime();
            while (reader.Read())
            {
                kelime.id = Convert.ToInt32(reader["id"]);
                kelime.tr = reader["tr"].ToString();
                kelime.ing = reader["ing"].ToString();
                kelime.tur = reader["tur"].ToString();
                kelime.trCumle = reader["trCumle"].ToString();
                kelime.ingCumle = reader["ingCumle"].ToString();
            }

            reader.Close();
            DB_Main.dbBaglantiKes();

            return kelime;
        }
        
        public static int kelimeEkle(Kelime kelime)
        {
            int eklenmeDurumu = -1;                         // -1 basarisiz, 0 mevcut, 1 basarili
            bool kelimeMevcut = kelimeMevcutMu(kelime);

            if (kelimeMevcut)
                eklenmeDurumu = 0;
            else
            {
                DB_Main.dbBaglan();
                cmd.Connection = DB_Main.con;
                cmd.CommandText = "INSERT INTO kelimeler(tr, ing, tur, trCumle, ingCumle) " +
                                  "VALUES ('" + kelime.tr + "','" +
                                  kelime.ing + "','" + kelime.tur + "','" +
                                  kelime.trCumle +"','" + kelime.ingCumle +"')";

                int Count = Convert.ToInt32(cmd.ExecuteScalar());

                if (Count == 0)
                    eklenmeDurumu = 1;

                DB_Main.dbBaglantiKes();
            }

            return eklenmeDurumu;
        }

        public static bool kelimeMevcutMu(Kelime kelime)
        {
            bool mevcutMu = false;

            DB_Main.dbBaglan();
            cmd.Connection = DB_Main.con;
            cmd.CommandText = "SELECT * FROM kelimeler " +
                              "WHERE tr = '" + kelime.tr + "'";

            int Count = Convert.ToInt32(cmd.ExecuteScalar());
            if (Count != 0)
                mevcutMu = true;

            DB_Main.dbBaglantiKes();

            return mevcutMu;
        }
    }
}