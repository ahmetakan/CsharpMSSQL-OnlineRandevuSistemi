using System;
using System.Data;
using System.Data.SqlClient;

namespace Proje
{
    public class YetkiliDBEntity : DBEntity
    {
        public void YetkiliEkle(Yetkili yetkili, string Tablo)
        {
            try
            {
                SqlConnection Connection = new SqlConnection(ConnectionString);
                SqlCommand Command = new SqlCommand("Insert Into " + Tablo + " (Isim, Soyisim, " +
                                                    "TCkimlikNumarasi, TelefonNumarasi, DogumTarihi, Eposta," +
                                                    "KullaniciAdi, Sifre, il, ilce, Cadde, Mahalle, Sokak)" +
                                                    "values (@parIsim, @parSoyisim, @parTCkimlikNumarasi," +
                                                    "@parTelefonNumarasi, @parDogumTarihi, @parEposta," +
                                                    "@parKullaniciAdi, @parSifre, @paril, @parilce, @parCadde," +
                                                    "@parMahalle, @parSokak)", Connection);

                Command.Parameters.AddWithValue("parIsim", yetkili.Isim);
                Command.Parameters.AddWithValue("parSoyisim", yetkili.SoyIsim);
                Command.Parameters.AddWithValue("parTCkimlikNumarasi", yetkili.TCkimlikNumarasi);
                Command.Parameters.AddWithValue("parTelefonNumarasi", yetkili.TelefonNumarasi);
                Command.Parameters.AddWithValue("parDogumTarihi", yetkili.DogumTarihi);
                Command.Parameters.AddWithValue("parEposta", yetkili.Eposta);
                Command.Parameters.AddWithValue("parKullaniciAdi", yetkili.KullaniciAdi);
                Command.Parameters.AddWithValue("parSifre", yetkili.Sifre);
                Command.Parameters.AddWithValue("paril", yetkili.Adres.Il);
                Command.Parameters.AddWithValue("parilce", yetkili.Adres.Ilce);
                Command.Parameters.AddWithValue("parCadde", yetkili.Adres.Cadde);
                Command.Parameters.AddWithValue("parMahalle", yetkili.Adres.Mahalle);
                Command.Parameters.AddWithValue("parSokak", yetkili.Adres.Sokak);

                Connection.Open();

                Command.ExecuteNonQuery();

                Connection.Close();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int KayitKontrolu(string KullaniciAdi, string Eposta, string TCkimlikNo)
        {
            try
            {
                int temp = 0;

                SqlConnection Connection = new SqlConnection(ConnectionString);

                SqlCommand Command = new SqlCommand("Select * from tbl_KayitliYetkili " +
                                                     "Where TCkimlikNumarasi = " +
                                                     "@parTCkimlikNumarasi", Connection);

                SqlCommand Command2 = new SqlCommand("Select * from tbl_TanimliYetkili " +
                                                     "Where TCkimlikNumarasi = " +
                                                     "@parTCkimlikNumarasi", Connection);

                SqlCommand Command3 = new SqlCommand("Select * from tbl_KayitliYetkili " +
                                                     "Where Eposta = @parEposta", Connection);

                SqlCommand Command4 = new SqlCommand("Select * from tbl_TanimliYetkili " +
                                                     "Where Eposta = @parEposta", Connection);

                SqlCommand Command5 = new SqlCommand("Select * from tbl_KayitliYetkili " +
                                                    "Where KullaniciAdi = " +
                                                    "@parKullaniciAdi", Connection);

                SqlCommand Command6 = new SqlCommand("Select * from tbl_TanimliYetkili " +
                                                    "Where KullaniciAdi = " +
                                                    "@parKullaniciAdi", Connection);


                Command.Parameters.AddWithValue("parTCkimlikNumarasi", TCkimlikNo);
                Command2.Parameters.AddWithValue("parTCkimlikNumarasi", TCkimlikNo);
                Command3.Parameters.AddWithValue("parEposta", Eposta);
                Command4.Parameters.AddWithValue("parEposta", Eposta);
                Command5.Parameters.AddWithValue("parKullaniciAdi", KullaniciAdi);
                Command6.Parameters.AddWithValue("parKullaniciAdi", KullaniciAdi);

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
                            temp = 1;
                        }
                    }
                }

                if (temp == 0)
                {
                    using (var reader = Command3.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            temp = 2;
                        }
                    }
                }

                if (temp == 0)
                {
                    using (var reader = Command4.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            temp = 2;
                        }
                    }
                }

                if (temp == 0)
                {
                    using (var reader = Command5.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            temp = 3;
                        }
                    }
                }

                if (temp == 0)
                {
                    using (var reader = Command6.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            temp = 3;
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

        public DataSet RandevuTalepListele(int iId, string tablo)
        {
            try
            {
                SqlConnection Connection = new SqlConnection(ConnectionString);
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();

                Connection.Open();
                da.SelectCommand = new SqlCommand("Select * From " + tablo +
                                                  " Where IsYeriID = " + iId, Connection);
                da.Fill(ds);

                Connection.Close();

                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Sil(string Tablo, int id)
        {
            try
            {
                SqlConnection Connection = new SqlConnection(ConnectionString);
                SqlCommand Command = new SqlCommand("Delete From " + Tablo +
                                                    " Where ID = @parID", Connection);

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

        public void SatirSil(string Tablo, int id)
        {
            try
            {
                SqlConnection Connection = new SqlConnection(ConnectionString);
                SqlCommand Command = new SqlCommand("Delete From " + Tablo +
                                                    " Where RandevuID = @parRandevuID", Connection);

                Command.Parameters.AddWithValue("parRandevuID", id);

                Connection.Open();

                Command.ExecuteNonQuery();

                Connection.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Tanimla(Yetkili yetkili, string Tablo, int yId, int iId)
        {
            try
            {
                SqlConnection Connection = new SqlConnection(ConnectionString);
                SqlCommand Command = new SqlCommand("Insert Into " + Tablo + " (Isim, Soyisim, " +
                                                    "TCkimlikNumarasi, TelefonNumarasi, DogumTarihi, Eposta," +
                                                    "KullaniciAdi, Sifre, il, ilce, Cadde, Mahalle, Sokak, ID, IsYeriID)" +
                                                    "values (@parIsim, @parSoyisim, @parTCkimlikNumarasi," +
                                                    "@parTelefonNumarasi, @parDogumTarihi, @parEposta," +
                                                    "@parKullaniciAdi, @parSifre, @paril, @parilce, @parCadde," +
                                                    "@parMahalle, @parSokak, @parID, @parIsYeriID)", Connection);

                Command.Parameters.AddWithValue("parIsim", yetkili.Isim);
                Command.Parameters.AddWithValue("parSoyisim", yetkili.SoyIsim);
                Command.Parameters.AddWithValue("parTCkimlikNumarasi", yetkili.TCkimlikNumarasi);
                Command.Parameters.AddWithValue("parTelefonNumarasi", yetkili.TelefonNumarasi);
                Command.Parameters.AddWithValue("parDogumTarihi", yetkili.DogumTarihi);
                Command.Parameters.AddWithValue("parEposta", yetkili.Eposta);
                Command.Parameters.AddWithValue("parKullaniciAdi", yetkili.KullaniciAdi);
                Command.Parameters.AddWithValue("parSifre", yetkili.Sifre);
                Command.Parameters.AddWithValue("paril", yetkili.Adres.Il);
                Command.Parameters.AddWithValue("parilce", yetkili.Adres.Ilce);
                Command.Parameters.AddWithValue("parCadde", yetkili.Adres.Cadde);
                Command.Parameters.AddWithValue("parMahalle", yetkili.Adres.Mahalle);
                Command.Parameters.AddWithValue("parSokak", yetkili.Adres.Sokak);
                Command.Parameters.AddWithValue("parID", yId);
                Command.Parameters.AddWithValue("parIsYeriID", iId);

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
                SqlCommand Command = new SqlCommand("Select ID, IsYeriID From tbl_TanimliYetkili " +
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
                            ID[1] = reader.GetInt32(1);
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

        public void UygunRandevuAyari(int iID, DateTime tarih, bool saat8_9,
                                      bool saat9_10, bool saat10_11,
                                      bool saat11_12, bool saat13_14,
                                      bool saat14_15, bool saat15_16,
                                      bool saat16_17)
        {
            try
            {
                SqlConnection Connection = new SqlConnection(ConnectionString);
                SqlCommand Command = new SqlCommand("Insert into tbl_UygunRandevu (IsYeriID, SecilenTarih, " +
                                                    "[08.00 - 09.00], [09.00 - 10.00], " +
                                                    "[10.00 - 11.00], [11.00 - 12.00], " +
                                                    "[13.00 - 14.00], [14.00 - 15.00], " +
                                                    "[15.00 - 16.00], [16.00 - 17.00]) " +
                                                    "values (@parIsYeriID, @parSecilenTarih, " +
                                                    "@par8_9, @par9_10, @par10_11, @par11_12, " +
                                                    "@par13_14, @par14_15, @par15_16, " +
                                                    "@par16_17)", Connection);

                Command.Parameters.AddWithValue("parIsYeriID", iID);
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

        public void RandevuTalepBilgileri(int kId, string KullaniciTablo, 
                                          string TalepTablo, string[] kisi, 
                                          string[] saataraligi, int id)
        {
            
            try
            {
                SqlConnection Connection = new SqlConnection(ConnectionString);

                SqlCommand Command = new SqlCommand("Select * from " + KullaniciTablo +
                                                    " Where ID = @parID", Connection);

                Command.Parameters.AddWithValue("parID", kId);

                Connection.Open();

                using (var reader = Command.ExecuteReader())
                {
                    if(reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            kisi[0] = reader.GetString(1);
                            kisi[1] = reader.GetString(2);
                        }
                    }
                }

                SqlCommand Command2 = new SqlCommand("Select * From " + TalepTablo + 
                                                     " Where RandevuID = @parRandevuID", Connection);

                Command2.Parameters.AddWithValue("parRandevuID", id);

                using(var reader = Command2.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            for (int i = 4; i < 12; i++)
                            {
                                if (reader.GetBoolean(i) == true)
                                {
                                    saataraligi[0] = reader.GetName(i);
                                }
                            }
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

        public void RandevuOnayla(int iID, int kId, DateTime tarih,
                                  bool saat8_9, bool saat9_10,
                                  bool saat10_11, bool saat11_12,
                                  bool saat13_14, bool saat14_15,
                                  bool saat15_16, bool saat16_17)
        {
            try
            {
                SqlConnection Connection = new SqlConnection(ConnectionString);
                SqlCommand Command = new SqlCommand("Insert into tbl_KayitliRandevu " +
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

        public DataSet SoruListele(int id, int durum, string column)
        {
            try
            {
                SqlConnection Connection = new SqlConnection(ConnectionString);
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();

                Connection.Open();
                da.SelectCommand = new SqlCommand("Select * From tbl_SoruCevap " +
                                                  "Where " + column + id + " and " +
                                                  "Durum = "+ durum, Connection);

                da.Fill(ds);

                Connection.Close();
                return ds;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void SoruCevapla(int yId, string cevap, int soruId)
        {
            try
            {
                SqlConnection Connection = new SqlConnection(ConnectionString);
                SqlCommand Command = new SqlCommand("Update tbl_SoruCevap Set " +
                                                    "CevaplayanID = @parCevaplayanID, " +
                                                    "Cevap = @parCevap, Durum = 1 Where " +
                                                    "SoruID = @parSoruID", Connection);

                Command.Parameters.AddWithValue("parCevaplayanID", yId);
                Command.Parameters.AddWithValue("parCevap", cevap);
                Command.Parameters.AddWithValue("parSoruID", soruId);

                Connection.Open();

                Command.ExecuteNonQuery();

                Connection.Close();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
