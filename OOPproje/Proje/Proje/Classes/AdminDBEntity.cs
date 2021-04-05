using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Proje
{
    public class AdminDBEntity : DBEntity
    {
        public bool PaneleGiris(string kullaniciadi, string sifre)
        {

            try
            {
                SqlConnection Connection = new SqlConnection(ConnectionString);
                SqlCommand Command = new SqlCommand("Select * From tbl_GenelAdmin " +
                                                    "Where (KullaniciAdi = @parKullaniciAdi) " +
                                                    "and (Sifre = @parSifre)", Connection);

                Command.Parameters.AddWithValue("parKullaniciAdi", kullaniciadi);
                Command.Parameters.AddWithValue("parSifre", sifre);

                Connection.Open();

                using (var reader = Command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
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

        public int KayitKontrolu(string TelefonNo)
        {
            try
            {
                int temp = 0;

                SqlConnection Connection = new SqlConnection(ConnectionString);

                SqlCommand Command = new SqlCommand("Select * from tbl_IsYeri " +
                                                    "Where TelefonNumarasi = @parTelefonNumarasi", Connection);

                Command.Parameters.AddWithValue("parTelefonNumarasi", TelefonNo);

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

                Connection.Close();

                return temp;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataSet KullaniciSonGirisGoster()
        {
            try
            {
                SqlConnection Connection = new SqlConnection(ConnectionString);
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();

                da.SelectCommand = new SqlCommand("Select t1.ID, Isim, Soyisim, TelefonNumarasi, " +
                                                  "Eposta, KullaniciAdi, Sifre, il, ilce, Cadde, " +
                                                  "Mahalle, Sokak, SistemeSonGiris from " +
                                                  "tbl_KayitliKullanici t1 Inner Join " +
                                                  "tbl_KullaniciSonGiris t2 ON t1.ID = t2.ID", Connection);

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
    }
}
