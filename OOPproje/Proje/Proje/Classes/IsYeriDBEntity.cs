using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Proje
{
    public class IsYeriDBEntity : DBEntity
    {
        public void Ekle(IsYeri isyeri)
        {
            try
            {
                SqlConnection Connection = new SqlConnection(ConnectionString);
                SqlCommand Command = new SqlCommand("Insert Into tbl_IsYeri (Isim, IsYeriTuru," +
                                                    "TelefonNumarasi, il, ilce, Cadde, Mahalle, " +
                                                    "Sokak) values (@parIsim, @parIsyeriTuru, " +
                                                    "@parTelefonNumarasi, @paril, @parilce, " +
                                                    "@parCadde, @parMahalle, @parSokak)", Connection);

                Command.Parameters.AddWithValue("parIsim", isyeri.Isim);
                Command.Parameters.AddWithValue("parIsyeriTuru", isyeri.IsYeriTuru);
                Command.Parameters.AddWithValue("parTelefonNumarasi", isyeri.TelefonNumarasi);
                Command.Parameters.AddWithValue("paril", isyeri.adres.Il);
                Command.Parameters.AddWithValue("parilce", isyeri.adres.Ilce);
                Command.Parameters.AddWithValue("parCadde", isyeri.adres.Cadde);
                Command.Parameters.AddWithValue("parMahalle", isyeri.adres.Mahalle);
                Command.Parameters.AddWithValue("parSokak", isyeri.adres.Sokak);

                Connection.Open();

                Command.ExecuteNonQuery();

                Connection.Close();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        
        public DataSet Listele()
        {
           
            try
            {
                SqlConnection Connection = new SqlConnection(ConnectionString);
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter();

                Connection.Open();
                da.SelectCommand = new SqlCommand("Select * From tbl_IsYeri", Connection);
                da.Fill(ds);

                Connection.Close();
                return ds;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        
        public void Guncelle(string isim, string isyerituru,
                                   long telefonnumarasi, string il, 
                                   string ilce, string cadde, int id,
                                   string mahalle, string sokak)
        {
            try
            {
                SqlConnection Connection = new SqlConnection(ConnectionString);
                SqlCommand Command = new SqlCommand("Update tbl_IsYeri Set Isim=@perIsim, " +
                                                    "IsYeriTuru=@perIsyeriTuru, " +
                                                    "TelefonNumarasi=@perTelefonNumarasi, il=@peril, " +
                                                    "ilce=@perilce, Cadde=@perCadde, " +
                                                    "Mahalle=@perMahalle, Sokak=@perSokak Where " +
                                                    "ID = @perID", Connection);

                Command.Parameters.AddWithValue("perID", id);
                Command.Parameters.AddWithValue("perIsim", isim);
                Command.Parameters.AddWithValue("perIsyeriTuru", isyerituru);
                Command.Parameters.AddWithValue("perTelefonNumarasi", telefonnumarasi);
                Command.Parameters.AddWithValue("peril", il);
                Command.Parameters.AddWithValue("perilce", ilce);
                Command.Parameters.AddWithValue("perCadde", cadde);
                Command.Parameters.AddWithValue("perMahalle", mahalle);
                Command.Parameters.AddWithValue("perSokak", sokak);

                Connection.Open();

                Command.ExecuteNonQuery();

                Connection.Close();

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
                SqlCommand Command = new SqlCommand("Delete From tbl_Isyeri Where " +
                                                    "ID = @perID", Connection);

                Command.Parameters.AddWithValue("perID", id);

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
