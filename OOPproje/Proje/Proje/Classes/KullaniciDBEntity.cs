using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
namespace Proje
{
    public class KullaniciDBEntity : DBEntity
    {
        public void KullaniciEkle(Kullanici kullanici, string Tablo)
        {

            try
            {
                SqlConnection Connection = new SqlConnection(ConnectionString);
                SqlCommand Command = new SqlCommand("Insert Into " + Tablo + " (Isim, Soyisim, " +
                                                    "TelefonNumarasi, Eposta, KullaniciAdi, Sifre, il, " +
                                                    "ilce, Cadde, Mahalle, Sokak) values (@parIsim, " +
                                                    "@parSoyisim, @parTelefonNumarasi, @parEposta, " +
                                                    "@parKullaniciAdi, @parSifre, @paril, @parilce, " +
                                                    "@parCadde, @parMahalle, @parSokak)", Connection);


                Command.Parameters.AddWithValue("parIsim", kullanici.Isim);
                Command.Parameters.AddWithValue("parSoyisim", kullanici.SoyIsim);
                Command.Parameters.AddWithValue("parTelefonNumarasi", kullanici.TelefonNumarasi);
                Command.Parameters.AddWithValue("parEposta", kullanici.Eposta);
                Command.Parameters.AddWithValue("parKullaniciAdi", kullanici.KullaniciAdi);
                Command.Parameters.AddWithValue("parSifre", kullanici.Sifre);
                Command.Parameters.AddWithValue("paril", kullanici.Adres.Il);
                Command.Parameters.AddWithValue("parilce", kullanici.Adres.Ilce);
                Command.Parameters.AddWithValue("parCadde", kullanici.Adres.Cadde);
                Command.Parameters.AddWithValue("parMahalle", kullanici.Adres.Mahalle);
                Command.Parameters.AddWithValue("parSokak", kullanici.Adres.Sokak);

                Connection.Open();

                Command.ExecuteNonQuery();

                Connection.Close();

            }

            catch (Exception ex)
            {

                throw ex;
            }

        }
                
        public DataSet Listele(string Tablo)
        {
            try
            {
                SqlConnection Connection = new SqlConnection(ConnectionString);
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();

                Connection.Open();
                da.SelectCommand = new SqlCommand("Select * From " + Tablo, Connection);
                da.Fill(ds);


                Connection.Close();
                return ds;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void Sil(int id)
        {
            try
            {
                SqlConnection Connection = new SqlConnection(ConnectionString);
                SqlCommand Command = new SqlCommand("Delete From tbl_KullaniciBasvuru " +
                                                    "Where ID = @parID", Connection);

                Command.Parameters.AddWithValue("parID", id);

                Connection.Open();

                Command.ExecuteNonQuery();

                Connection.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public bool PaneleGirisVeIDtutucu(string kullaniciadi, string sifre, int[] ID)
        {
            try
            {
                SqlConnection Connection = new SqlConnection(ConnectionString);
                SqlCommand Command = new SqlCommand("Select ID From tbl_KayitliKullanici " +
                                                    "Where (KullaniciAdi = @parKullaniciAdi) and " +
                                                    "(Sifre = @parSifre)", Connection);

                Command.Parameters.AddWithValue("parKullaniciAdi", kullaniciadi);
                Command.Parameters.AddWithValue("parSifre", sifre);

                Connection.Open();

                using (var reader = Command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            ID[0] = reader.GetInt32(0);
                        }

                        return true;
                    }

                    else
                    {
                        return false;
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public (DateTime[], bool[,], int) RandevuAraligi(int iId, DateTime[] tarih, bool[,] check, string tablo, int satirsayisi)
        {
            try
            {
                SqlConnection Connection = new SqlConnection(ConnectionString);
                SqlCommand Command = new SqlCommand("Select * From "+ tablo +
                                                    " Where IsYeriID = " +
                                                    "@parIsYeriID", Connection);

                SqlCommand Command2 = new SqlCommand("Select Count(*) From " + 
                                                    tablo + " Where IsYeriID = " +
                                                    "@parIsYeriID", Connection);

                Command.Parameters.AddWithValue("parIsYeriID", iId);
                Command2.Parameters.AddWithValue("parIsYeriID", iId);

                Connection.Open();

                using (var reader = Command2.ExecuteReader())
                {
                    if(reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            satirsayisi = reader.GetInt32(0);
                        }

                        check = new bool[satirsayisi, 8];
                        tarih = new DateTime[satirsayisi];
                    }
                }

                using (var reader = Command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        int i = 0;

                        while (reader.Read())
                        {
                            tarih[i] = reader.GetDateTime(1);
                            check[i,0] = reader.GetBoolean(2);
                            check[i,1] = reader.GetBoolean(3);
                            check[i,2] = reader.GetBoolean(4);
                            check[i,3] = reader.GetBoolean(5);
                            check[i,4] = reader.GetBoolean(6);
                            check[i,5] = reader.GetBoolean(7);
                            check[i,6] = reader.GetBoolean(8);
                            check[i,7] = reader.GetBoolean(9);

                            i++;
                        }
                    }
                }
                Connection.Close();

                return (tarih, check, satirsayisi);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public (int, bool[,], DateTime[]) KayitliRandevuAraligi(int iId, DateTime[] tarih2, 
                                                                string tablo, bool[,] check, 
                                                                int satirsayisi)
        {
            try
            {
                SqlConnection Connection = new SqlConnection(ConnectionString);
                SqlCommand Command = new SqlCommand("Select * From " + tablo +
                                                    " Where IsYeriID = " +
                                                    "@parIsYeriID", Connection);

                SqlCommand Command2 = new SqlCommand("Select Count(*) From " + tablo +
                                                     " Where IsYeriID = " +
                                                     "@parIsYeriID", Connection);

                Command.Parameters.AddWithValue("parIsYeriID", iId);
                Command2.Parameters.AddWithValue("parIsYeriID", iId);

                Connection.Open();

                using (var reader2 = Command2.ExecuteReader())
                {
                    if (reader2.HasRows)
                    {
                        while (reader2.Read())
                        {
                            satirsayisi = reader2.GetInt32(0);
                        }

                        check = new bool[satirsayisi, 8];
                        tarih2 = new DateTime[satirsayisi];
                    }
                }

                using (var reader = Command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        int i = 0;

                        while (reader.Read())
                        {
                            tarih2[i] = reader.GetDateTime(2);

                            check[i, 0] = reader.GetBoolean(3);
                            check[i, 1] = reader.GetBoolean(4);
                            check[i, 2] = reader.GetBoolean(5);
                            check[i, 3] = reader.GetBoolean(6);
                            check[i, 4] = reader.GetBoolean(7);
                            check[i, 5] = reader.GetBoolean(8);
                            check[i, 6] = reader.GetBoolean(9);
                            check[i, 7] = reader.GetBoolean(10);

                            i++;
                        }
                    }
                }

                Connection.Close();

                return (satirsayisi, check, tarih2);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public void RandevuTalebi(int iID, int kId, DateTime tarih, 
                                  bool saat8_9, bool saat9_10, 
                                  bool saat10_11, bool saat11_12, 
                                  bool saat13_14, bool saat14_15, 
                                  bool saat15_16, bool saat16_17)
        {
            try
            {
                SqlConnection Connection = new SqlConnection(ConnectionString);
                SqlCommand Command = new SqlCommand("Insert into tbl_RandevuTalebi " +
                                                    "(IsYeriID, ID, SecilenTarih, " +
                                                    "[08.00 - 09.00], [09.00 - 10.00], " +
                                                    "[10.00 - 11.00], [11.00 - 12.00], " +
                                                    "[13.00 - 14.00], [14.00 - 15.00], " +
                                                    "[15.00 - 16.00], [16.00 - 17.00]) " +
                                                    "values (@parIsYeriID, @parID, " +
                                                    "@parSecilenTarih, @par8_9, @par9_10, " +
                                                    "@par10_11, @par11_12, @par13_14, " +
                                                    "@par14_15, @par15_16, @par16_17)", Connection);

                Command.Parameters.AddWithValue("parIsYeriID", iID);
                Command.Parameters.AddWithValue("parID", kId);
                Command.Parameters.AddWithValue("parSecilenTarih", tarih);
                Command.Parameters.AddWithValue("par8_9", saat8_9);
                Command.Parameters.AddWithValue("par9_10", saat9_10);
                Command.Parameters.AddWithValue("par10_11", saat10_11);
                Command.Parameters.AddWithValue("par11_12", saat11_12);
                Command.Parameters.AddWithValue("par13_14", saat13_14);
                Command.Parameters.AddWithValue("par14_15", saat14_15);
                Command.Parameters.AddWithValue("par15_16", saat15_16);
                Command.Parameters.AddWithValue("par16_17", saat16_17);

                Connection.Open();

                Command.ExecuteNonQuery();

                Connection.Close();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void SoruSor(int iId, int kId, string soru, bool durum)
        {
            try
            {
                SqlConnection Connection = new SqlConnection(ConnectionString);
                SqlCommand Command = new SqlCommand("Insert Into tbl_SoruCevap " +
                                                    "(IsYeriID, SoranID, Soru, Durum) " +
                                                    "values (@parIsYeriID, @parSoranID, " +
                                                    "@parSoru, @parDurum)", Connection);

                Command.Parameters.AddWithValue("parIsYeriID", iId);
                Command.Parameters.AddWithValue("parSoranID", kId);
                Command.Parameters.AddWithValue("parSoru", soru);
                Command.Parameters.AddWithValue("parDurum", durum);

                Connection.Open();

                Command.ExecuteNonQuery();

                Connection.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataSet Randevularim(int id)
        {
            try
            {
                SqlConnection Connection = new SqlConnection(ConnectionString);

                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();

                da.SelectCommand = new SqlCommand("Select * from tbl_KayitliRandevu where ID = @parKullaniciID", Connection);
                da.SelectCommand.Parameters.AddWithValue("parKullaniciID", id);

                Connection.Open();

                da.Fill(ds);

                Connection.Close();

                return ds;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataSet IsYeriFiltrele(string IsYeriAdi, string IsYeriTuru)
        {
            try
            {
                SqlConnection Connection = new SqlConnection(ConnectionString);

                SqlDataAdapter da = new SqlDataAdapter();
                DataSet ds = new DataSet();

                da.SelectCommand = new SqlCommand("Select * From tbl_IsYeri " +
                                                  "Where Isim Like '%'+@parIsYeriAdi+'%'" +
                                                  " and IsYeriTuru Like " +
                                                  "'%'+@parIsYeriTuru+'%'", Connection);

                da.SelectCommand.Parameters.AddWithValue("parIsYeriAdi", IsYeriAdi);
                da.SelectCommand.Parameters.AddWithValue("parIsYeriTuru", IsYeriTuru);

                Connection.Open();

                da.Fill(ds);

                Connection.Close();

                return ds;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int KayitKontrolu(string KullaniciAdi, string Eposta)
        {
            try
            {
                int temp = 0;

                SqlConnection Connection = new SqlConnection(ConnectionString);

                SqlCommand Command = new SqlCommand("Select * from tbl_KayitliKullanici " +
                                                     "Where Eposta = @parEposta", Connection);

                SqlCommand Command2 = new SqlCommand("Select * from tbl_KayitliKullanici " +
                                                    "Where KullaniciAdi = @parKullaniciAdi", Connection);
                

                Command.Parameters.AddWithValue("parEposta", Eposta);
                Command2.Parameters.AddWithValue("parKullaniciAdi", KullaniciAdi);

                Connection.Open();

                if (temp == 0)
                {
                    using (var reader = Command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            temp = 1;
                        }
                    }
                }

                if (temp == 0)
                {
                    using (var reader = Command2.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            temp = 2;
                        }
                    }
                }

                Connection.Close();

                return temp;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void SonGirisZamaniTut(int ID, DateTime SonGiris)
        {
            try
            {

                int temp = 0;

                SqlConnection Connection = new SqlConnection(ConnectionString);
                SqlCommand Command = new SqlCommand("Select * from tbl_KullaniciSonGiris " +
                                                    "Where ID = @parKullaniciID", Connection);

                Command.Parameters.AddWithValue("parKullaniciID", ID);

                SqlCommand Command2 = new SqlCommand("Update tbl_KullaniciSonGiris Set " +
                                                     "SistemeSonGiris = @parSonGiris " +
                                                     "Where ID = @parKullaniciID1", Connection);

                Command2.Parameters.AddWithValue("parKullaniciID1", ID);
                Command2.Parameters.AddWithValue("parSonGiris", SonGiris);

                SqlCommand Command3 = new SqlCommand("Insert Into tbl_KullaniciSonGiris " +
                                                     "(ID, SistemeSonGiris) values " +
                                                     "(@parKullaniciID2, @parSonGiris)", Connection);

                Command3.Parameters.AddWithValue("parKullaniciID2", ID);
                Command3.Parameters.AddWithValue("parSonGiris", SonGiris);


                Connection.Open();

                using (var reader = Command.ExecuteReader())
                {
                    if (reader.HasRows == false)
                    {
                        temp++;
                    }

                }

                if (temp > 0)
                {
                    Command3.ExecuteNonQuery();
                }

                else
                {
                    Command2.ExecuteNonQuery();
                }

                Connection.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public void SonGirisZamaniOlustur(string kullaniciadi, int id)
        {
            try
            {
                DateTime date = new DateTime(1753, 1, 1, 12, 0, 0);

                SqlConnection Connection = new SqlConnection(ConnectionString);

                SqlCommand Command = new SqlCommand("Select * from tbl_KayitliKullanici" +
                                                    " Where KullaniciAdi = @parKullaniciAdi",
                                                    Connection);

                SqlCommand Command2 = new SqlCommand("Insert into tbl_KullaniciSonGiris " +
                                                     "(ID, SistemeSonGiris) values " +
                                                     "(@parID, @parOlustur)", Connection);

                Command.Parameters.AddWithValue("parKullaniciAdi", kullaniciadi);
                
                Command2.Parameters.AddWithValue("parOlustur", date);

                Connection.Open();

                using (var reader = Command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            id = reader.GetInt32(0);

                            reader.Close();

                            Command2.Parameters.AddWithValue("parID", id);
                            Command2.ExecuteNonQuery();

                            break;
                        }
                    }
                }

                Connection.Close();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}