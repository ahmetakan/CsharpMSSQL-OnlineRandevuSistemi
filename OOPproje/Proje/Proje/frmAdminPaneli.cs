using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proje
{
    public partial class frmAdminPaneli : Form
    {
        public frmAdminPaneli()
        {
            InitializeComponent();
        }

        private void EkranTemizle(Control control)
        {
            foreach (Control nesne in control.Controls)
            {
                if (nesne is TextBox)
                {
                    ((TextBox)nesne).Clear();
                }
                if (nesne.Controls.Count > 0)
                {
                    EkranTemizle(nesne);
                }
                if (nesne is ComboBox)
                {
                    ((ComboBox)nesne).SelectedItem = null;
                }
                if (nesne.Controls.Count > 0)
                {
                    EkranTemizle(nesne);
                }
            }
            
        }

        public bool EkranKontrol(Control control, Control control2)
        {
            int temp = 0;

            foreach (Control nesne in control.Controls)
            {
                if (nesne is TextBox && nesne.Text == "")
                {
                    if (temp == 0)
                    {
                        MessageBox.Show("Lütfen formu eksiksiz " + "\n" +
                                        "bir şekilde doldurunuz.", "Uyarı",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        temp++;
                    }

                    nesne.Focus();
                }

                else if (nesne is ComboBox)
                {
                    if (((ComboBox)nesne).SelectedItem == null)
                    {
                        if (temp == 0)
                        {
                            MessageBox.Show("Lütfen formu eksiksiz " + "\n" +
                                            "bir şekilde doldurunuz.", "Uyarı",
                                            MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            temp++;
                        }

                        nesne.Focus();
                    }
                }
            }
            if (temp != 0)
            {
                return false;
            }

            foreach (Control nesne in control2.Controls)
            {
                if (nesne is TextBox && nesne.Text == "")
                {
                    if (temp == 0)
                    {
                        MessageBox.Show("Lütfen formu eksiksiz " + "\n" +
                                        "bir şekilde doldurunuz.", "Uyarı",
                                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        temp++;
                    }

                    nesne.Focus();
                }
            }

            if (temp != 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool EkranKontrol2(TextBox TelefonNo)
        {
            if (10000000000 > Convert.ToInt64(TelefonNo.Text) &&
                999999999 < Convert.ToInt64(TelefonNo.Text))
            {
                return true;
            }

            else
            {
                MessageBox.Show("Telefon numarası başında 0 " + "\n" +
                                "rakamı olmadan ve 10 haneli " + "\n" +
                                "olacak şekilde girilmelidir.", "Uyarı",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);

                TelefonNo.Focus();

                return false;
            }
        }

        private bool KayitKontrol(TextBox TelefonNo)
        {
            AdminDBEntity AdminDB = new AdminDBEntity();

            int temp = AdminDB.KayitKontrolu(TelefonNo.Text);

            if (temp == 1)
            {
                MessageBox.Show("Bu Telefon numarası halihazırda " + "\n" +
                                "kullanılmaktadır. Lütfen farklı " + "\n" +
                                "bir telefon numarası tercih ediniz.", "Uyarı",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);

                TelefonNo.Focus();

                return false;
            }

            else
            {
                return true;
            }
        }

        private void SadeceHarf(KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) &&
                        !char.IsControl(e.KeyChar) &&
                        !char.IsSeparator(e.KeyChar);
        }

        private void SadeceSayi(KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) &&
                        !char.IsControl(e.KeyChar);
        }

        private int iId, kId, yId, temp;

        private void IsYeriEkle(string isim, string isyerituru, 
                                long telefonnumarasi, string il, 
                                string ilce, string cadde, 
                                string mahalle, string sokak)
        {
            IsYeriDBEntity IsYeriDB = new IsYeriDBEntity();
            IsYeri isyeri = new IsYeri
            {
                Isim = isim,
                IsYeriTuru = isyerituru,
                TelefonNumarasi = telefonnumarasi
            };

            isyeri.adres.Il = il;
            isyeri.adres.Ilce = ilce;
            isyeri.adres.Cadde = cadde;
            isyeri.adres.Mahalle = mahalle;
            isyeri.adres.Sokak = sokak;

            IsYeriDB.Ekle(isyeri);

            EkranTemizle(tabIsYeriIslemleri);

            IsYeriListele();
        }

        private void IsYeriGuncelle(string isim, string isyerituru, 
                                    long telefonnumarasi, string il, 
                                    string ilce, string cadde, int id,
                                    string mahalle, string sokak)
        {
            IsYeriDBEntity IsYeriDB = new IsYeriDBEntity();

            IsYeriDB.Guncelle(isim, isyerituru, telefonnumarasi,
                              il, ilce, cadde, id, mahalle, sokak);

            EkranTemizle(tabIsYeriIslemleri);

            IsYeriListele();
        }

        private void IsYeriSil(int id)
        {
            IsYeriDBEntity IsYeriDB = new IsYeriDBEntity();

            IsYeriDB.Sil(id);

            EkranTemizle(tabIsYeriIslemleri);

            IsYeriListele();
        }

        private void IsYeriListele()
        {
            IsYeriDBEntity IsYeriDB = new IsYeriDBEntity();

            dgvIsYeriListesi.DataSource = IsYeriDB.Listele().Tables[0];

            EkranTemizle(tabIsYeriIslemleri);
        }

        private void KullaniciListele(string Tablo)
        {
            KullaniciDBEntity KullaniciDB = new KullaniciDBEntity();

            dgvKullaniciBasvuruListesi.DataSource = KullaniciDB.Listele(Tablo).Tables[0];

            EkranTemizle(tabKullaniciOnaylama);
        }


        private void KullaniciOnayla(string isim, string soyisim, long telefonnumarasi,
                                     string kullaniciadi, string sifre, string eposta,
                                     string il, string ilce, string cadde, string mahalle,
                                     string sokak, int id)
        {
            KullaniciDBEntity KullaniciDB = new KullaniciDBEntity();

            Kullanici kullanici1 = new Kullanici
            {
                Isim = isim,
                SoyIsim = soyisim,
                TelefonNumarasi = telefonnumarasi,
                KullaniciAdi = kullaniciadi,
                Sifre = sifre,
                Eposta = eposta
            };

            kullanici1.Adres.Il = il;
            kullanici1.Adres.Ilce = ilce;
            kullanici1.Adres.Cadde = cadde;
            kullanici1.Adres.Mahalle = mahalle;
            kullanici1.Adres.Sokak = sokak;

            KullaniciDB.KullaniciEkle(kullanici1, "tbl_KayitliKullanici");
            KullaniciDB.SonGirisZamaniOlustur(kullanici1.KullaniciAdi, temp);

            KullaniciDB.Sil(id);

            EkranTemizle(tabKullaniciOnaylama);

            KullaniciListele("tbl_KullaniciBasvuru");

            btnKullaniciOnayla.Enabled = false;
            btnKullaniciReddet.Enabled = false;
        }

        private void KullaniciReddet(int id)
        {
            KullaniciDBEntity KullaniciDB = new KullaniciDBEntity();

            KullaniciDB.Sil(id);

            EkranTemizle(tabKullaniciOnaylama);

            KullaniciListele("tbl_KullaniciBasvuru");

            btnKullaniciOnayla.Enabled = false;
            btnKullaniciReddet.Enabled = false;
        }

        private void YetkiliListele(string Tablo, Control control, 
                                    DataGridView datagridview)
        {
            YetkiliDBEntity YetkiliDB = new YetkiliDBEntity();

            datagridview.DataSource = YetkiliDB.Listele(Tablo).Tables[0];

            EkranTemizle(control);
        }

        private void YetkiliOnayla(string isim, string soyisim, long TCkimliknumarasi,
                                   long telefonnumarasi, DateTime dogumtarihi,
                                   string eposta, string kullaniciadi, int id,
                                   string sifre, string il, string ilce,
                                   string cadde, string mahalle, string sokak)
        {
            YetkiliDBEntity YetkiliDB = new YetkiliDBEntity();

            Yetkili yetkili = new Yetkili
            {
                Isim = isim,
                SoyIsim = soyisim,
                TCkimlikNumarasi = TCkimliknumarasi,
                TelefonNumarasi = telefonnumarasi,
                DogumTarihi = dogumtarihi,
                Eposta = eposta,
                KullaniciAdi = kullaniciadi,
                Sifre = sifre
            };

            yetkili.Adres.Il = il;
            yetkili.Adres.Ilce = ilce;
            yetkili.Adres.Cadde = cadde;
            yetkili.Adres.Mahalle = mahalle;
            yetkili.Adres.Sokak = sokak;

            YetkiliDB.YetkiliEkle(yetkili, "tbl_KayitliYetkili");

            YetkiliDB.Sil("tbl_YetkiliBasvuru", id);

            EkranTemizle(tabYetkiliOnaylama);

            YetkiliListele("tbl_YetkiliBasvuru", tabYetkiliOnaylama,
                           dgvYetkiliBasvuruListesi);

            btnYetkiliOnayla.Enabled = false;
            btnYetkiliReddet.Enabled = false;
        }

        private void YetkiliReddet(int id)
        {
            YetkiliDBEntity YetkiliDB = new YetkiliDBEntity();

            YetkiliDB.Sil("tbl_YetkiliBasvuru", id);

            EkranTemizle(tabYetkiliOnaylama);

            YetkiliListele("tbl_YetkiliBasvuru", tabYetkiliOnaylama,
                           dgvYetkiliBasvuruListesi);

            btnYetkiliOnayla.Enabled = false;
            btnYetkiliReddet.Enabled = false;
        }

        private void YetkiliTanimla(string isim, string soyisim, long TCkimlikNumarasi, 
                                    long telefonnumarasi, DateTime dogumtarihi, 
                                    string eposta, string kullaniciadi, string sifre, 
                                    string il, string ilce, string cadde, string mahalle, 
                                    string sokak, int yId, int iId)
        {
            YetkiliDBEntity YetkiliDB = new YetkiliDBEntity();

            Yetkili yetkili = new Yetkili
            {
                Isim = isim,
                SoyIsim = soyisim,
                TCkimlikNumarasi = TCkimlikNumarasi,
                TelefonNumarasi = telefonnumarasi,
                DogumTarihi = dogumtarihi,
                Eposta = eposta,
                KullaniciAdi = kullaniciadi,
                Sifre = sifre,
            };

            yetkili.Adres.Il = il;
            yetkili.Adres.Ilce = ilce;
            yetkili.Adres.Cadde = cadde;
            yetkili.Adres.Mahalle = mahalle;
            yetkili.Adres.Sokak = sokak;

            YetkiliDB.Tanimla(yetkili, "tbl_TanimliYetkili", yId, iId);

            YetkiliDB.Sil("tbl_KayitliYetkili", yId);

            EkranTemizle(pnlAtanacakKisiselBilgiler);

            YetkiliListele("tbl_KayitliYetkili", pnlAtanacakKisiselBilgiler,
                           dgvAtanacakYetkiliListesi);

            btnAtamayiGerceklestir.Enabled = false;
        }

        private void HedefIsYeriYetkiliListele()
        {
            IsYeriDBEntity IsYeriDB = new IsYeriDBEntity();

            dgvHedefIsYeriListesi.DataSource = IsYeriDB.Listele().Tables[0];

            YetkiliDBEntity YetkiliDB = new YetkiliDBEntity();

            dgvAtanacakYetkiliListesi.DataSource = YetkiliDB.Listele("tbl_KayitliYetkili").Tables[0];

            EkranTemizle(tabYetkiliTanimla);

            btnAtamayiGerceklestir.Enabled = false;
        }

        private void RaporListele(string tablo, DataGridView dgv)
        {
            YetkiliDBEntity YetkiliDB = new YetkiliDBEntity();

            dgv.DataSource = YetkiliDB.Listele(tablo).Tables[0];
        }

        private void SonGirisliKullaniciListe(DataGridView dgv)
        {
            AdminDBEntity AdminDB = new AdminDBEntity();

            dgv.DataSource = AdminDB.KullaniciSonGirisGoster().Tables[0];

        }

        private void btnIsYeriEkle_Click(object sender, EventArgs e)
        {
            if (EkranKontrol(pnlisYeriGenelBilgiler, pnlisYeriAdresBilgileri))
            {
                if (EkranKontrol2(txtIsYeriTelefonNumarasi))
                {
                    if (KayitKontrol(txtIsYeriTelefonNumarasi))
                    {
                        IsYeriEkle(txtIsYeriAdi.Text, cmbIsYeriTurleri.Text,
                                   Convert.ToInt64(txtIsYeriTelefonNumarasi.Text),
                                   txtil.Text, txtilce.Text, txtCadde.Text,
                                   txtMahalle.Text, txtSokak.Text);

                        btnIsYeriGuncelle.Enabled = false;
                        btnIsYeriSil.Enabled = false;
                    }
                }
            }
        }

        private void btnIsYeriGuncelle_Click(object sender, EventArgs e)
        {
            if (EkranKontrol(pnlisYeriGenelBilgiler, pnlisYeriAdresBilgileri))
            {
                if (EkranKontrol2(txtIsYeriTelefonNumarasi))
                {
                    IsYeriGuncelle(txtIsYeriAdi.Text, cmbIsYeriTurleri.Text,
                                   Convert.ToInt64(txtIsYeriTelefonNumarasi.Text),
                                   txtil.Text, txtilce.Text, txtCadde.Text,
                                   iId, txtMahalle.Text, txtSokak.Text);

                    btnIsYeriGuncelle.Enabled = false;
                    btnIsYeriSil.Enabled = false;
                }
            }
        }

        private void dgvIsYeriListesi_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dgvIsYeriListesi.SelectedCells[0].RowIndex;

            iId = Convert.ToInt32(dgvIsYeriListesi.Rows[secilen].Cells[0].Value);
            txtIsYeriAdi.Text = dgvIsYeriListesi.Rows[secilen].Cells[1].Value.ToString();
            cmbIsYeriTurleri.Text = dgvIsYeriListesi.Rows[secilen].Cells[2].Value.ToString();
            txtIsYeriTelefonNumarasi.Text = dgvIsYeriListesi.Rows[secilen].Cells[3].Value.ToString();
            txtil.Text = dgvIsYeriListesi.Rows[secilen].Cells[4].Value.ToString();
            txtilce.Text = dgvIsYeriListesi.Rows[secilen].Cells[5].Value.ToString();
            txtCadde.Text = dgvIsYeriListesi.Rows[secilen].Cells[6].Value.ToString();
            txtMahalle.Text = dgvIsYeriListesi.Rows[secilen].Cells[7].Value.ToString();
            txtSokak.Text = dgvIsYeriListesi.Rows[secilen].Cells[8].Value.ToString();

            btnIsYeriGuncelle.Enabled = true;
            btnIsYeriSil.Enabled = true;
        }

        private void btnIsYeriSil_Click(object sender, EventArgs e)
        {
            if (EkranKontrol(pnlisYeriGenelBilgiler, pnlisYeriAdresBilgileri))
            {
                if (EkranKontrol2(txtIsYeriTelefonNumarasi))
                {
                    IsYeriSil(iId);

                    btnIsYeriGuncelle.Enabled = false;
                    btnIsYeriSil.Enabled = false;
                }
            }
        }

        private void btnIsYeriListesiGoster_Click(object sender, EventArgs e)
        {
            IsYeriListele();

            btnIsYeriGuncelle.Enabled = false;
            btnIsYeriSil.Enabled = false;
        }

        private void btnKullaniciBasvuruListesi_Click(object sender, EventArgs e)
        {
            KullaniciListele("tbl_KullaniciBasvuru");

            btnKullaniciOnayla.Enabled = false;
            btnKullaniciReddet.Enabled = false;
        }

        private void dgvKullaniciBasvuruListesi_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dgvKullaniciBasvuruListesi.SelectedCells[0].RowIndex;

            kId = Convert.ToInt32(dgvKullaniciBasvuruListesi.Rows[secilen].Cells[0].Value);
            txtKullaniciIsim.Text = dgvKullaniciBasvuruListesi.Rows[secilen].Cells[1].Value.ToString();
            txtKullaniciSoyisim.Text = dgvKullaniciBasvuruListesi.Rows[secilen].Cells[2].Value.ToString();
            txtKullaniciTelefonNo.Text = dgvKullaniciBasvuruListesi.Rows[secilen].Cells[3].Value.ToString();
            txtKullaniciEposta.Text = dgvKullaniciBasvuruListesi.Rows[secilen].Cells[4].Value.ToString();
            txtKullaniciKullaniciAdi.Text = dgvKullaniciBasvuruListesi.Rows[secilen].Cells[5].Value.ToString();
            txtKullaniciSifre.Text = dgvKullaniciBasvuruListesi.Rows[secilen].Cells[6].Value.ToString();
            txtKullaniciIl.Text = dgvKullaniciBasvuruListesi.Rows[secilen].Cells[7].Value.ToString();
            txtKullaniciIlce.Text = dgvKullaniciBasvuruListesi.Rows[secilen].Cells[8].Value.ToString();
            txtKullaniciCadde.Text = dgvKullaniciBasvuruListesi.Rows[secilen].Cells[9].Value.ToString();
            txtKullaniciMahalle.Text = dgvKullaniciBasvuruListesi.Rows[secilen].Cells[10].Value.ToString();
            txtKullaniciSokak.Text = dgvKullaniciBasvuruListesi.Rows[secilen].Cells[11].Value.ToString();

            btnKullaniciOnayla.Enabled = true;
            btnKullaniciReddet.Enabled = true;
        }

        private void btnKullaniciOnayla_Click(object sender, EventArgs e)
        {
            KullaniciOnayla(txtKullaniciIsim.Text, txtKullaniciSoyisim.Text,
                            Convert.ToInt64(txtKullaniciTelefonNo.Text),
                            txtKullaniciKullaniciAdi.Text, txtKullaniciSifre.Text,
                            txtKullaniciEposta.Text, txtKullaniciIl.Text,
                            txtKullaniciIlce.Text, txtKullaniciCadde.Text,
                            txtKullaniciMahalle.Text, txtKullaniciSokak.Text, kId);
        }

        private void btnYetkiliBasvuruListesi_Click(object sender, EventArgs e)
        {
            YetkiliListele("tbl_YetkiliBasvuru", tabYetkiliOnaylama,
                           dgvYetkiliBasvuruListesi);

            btnYetkiliOnayla.Enabled = false;
            btnYetkiliReddet.Enabled = false;
        }

        private void dgvYetkiliBasvuruListesi_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dgvYetkiliBasvuruListesi.SelectedCells[0].RowIndex;

            yId = Convert.ToInt32(dgvYetkiliBasvuruListesi.Rows[secilen].Cells[0].Value);
            txtYetkiliIsim.Text = dgvYetkiliBasvuruListesi.Rows[secilen].Cells[1].Value.ToString();
            txtYetkiliSoyisim.Text = dgvYetkiliBasvuruListesi.Rows[secilen].Cells[2].Value.ToString();
            txtTCkimlikNumarasi.Text = dgvYetkiliBasvuruListesi.Rows[secilen].Cells[3].Value.ToString();
            txtYetkiliTelefonNumarasi.Text = dgvYetkiliBasvuruListesi.Rows[secilen].Cells[4].Value.ToString();
            txtDogumTarihi.Text = dgvYetkiliBasvuruListesi.Rows[secilen].Cells[5].Value.ToString();
            txtYetkiliEposta.Text = dgvYetkiliBasvuruListesi.Rows[secilen].Cells[6].Value.ToString();
            txtYetkiliKullaniciAdi.Text = dgvYetkiliBasvuruListesi.Rows[secilen].Cells[7].Value.ToString();
            txtYetkiliSifre.Text = dgvYetkiliBasvuruListesi.Rows[secilen].Cells[8].Value.ToString();
            txtYetkiliIl.Text = dgvYetkiliBasvuruListesi.Rows[secilen].Cells[9].Value.ToString();
            txtYetkiliIlce.Text = dgvYetkiliBasvuruListesi.Rows[secilen].Cells[10].Value.ToString();
            txtYetkiliCadde.Text = dgvYetkiliBasvuruListesi.Rows[secilen].Cells[11].Value.ToString();
            txtYetkiliMahalle.Text = dgvYetkiliBasvuruListesi.Rows[secilen].Cells[12].Value.ToString();
            txtYetkiliSokak.Text = dgvYetkiliBasvuruListesi.Rows[secilen].Cells[13].Value.ToString();

            btnYetkiliOnayla.Enabled = true;
            btnYetkiliReddet.Enabled = true;
        }

        private void btnYetkiliReddet_Click(object sender, EventArgs e)
        {
            YetkiliReddet(yId);
        }

        private void btnListeleriGöster_Click(object sender, EventArgs e)
        {
            HedefIsYeriYetkiliListele();
        }

        private void dgvHedefIsYeriListesi_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dgvHedefIsYeriListesi.SelectedCells[0].RowIndex;

            iId = Convert.ToInt32(dgvHedefIsYeriListesi.Rows[secilen].Cells[0].Value);
            txtHedefIsYeriTuru.Text = dgvHedefIsYeriListesi.Rows[secilen].Cells[2].Value.ToString();
            txtHedefIsYeriAdi.Text = dgvHedefIsYeriListesi.Rows[secilen].Cells[1].Value.ToString();
            txtHedefTelefonNumarasi.Text = dgvHedefIsYeriListesi.Rows[secilen].Cells[3].Value.ToString();
        }

        public string eposta, kullaniciadi, sifre, il, ilce, cadde, mahalle, sokak;

        private void txtil_KeyPress(object sender, KeyPressEventArgs e)
        {
            SadeceHarf(e);
        }

        private void txtilce_KeyPress(object sender, KeyPressEventArgs e)
        {
            SadeceHarf(e);
        }

        private void txtMahalle_KeyPress(object sender, KeyPressEventArgs e)
        {
            SadeceHarf(e);
        }

        private void txtIsYeriTelefonNumarasi_KeyPress(object sender, KeyPressEventArgs e)
        {
            SadeceSayi(e);
        }

        private void txtIsYeriAdi_KeyPress(object sender, KeyPressEventArgs e)
        {
            SadeceHarf(e);
        }

        private void btnRaporYetkili_Click(object sender, EventArgs e)
        {
            RaporListele("tbl_TanimliYetkili", dgvRaporYetkili);
        }

        private void btnRaporKullanici_Click(object sender, EventArgs e)
        {
            SonGirisliKullaniciListe(dgvRaporKullanici);
        }

        private void btnRaporIsYeri_Click(object sender, EventArgs e)
        {
            RaporListele("tbl_IsYeri", dgvRaporIsYeri);
        }

        private void btnAtamayiGerceklestir_Click(object sender, EventArgs e)
        {
            YetkiliTanimla(txtAtanacakIsim.Text, txtAtanacakSoyisim.Text, 
                           Convert.ToInt64(txtAtanacakTCkimlikNumarasi.Text), 
                           Convert.ToInt64(txtAtanacakTelefonNumarasi.Text), 
                           Convert.ToDateTime(txtAtanacakDogumTarihi.Text), 
                           eposta, kullaniciadi, sifre, il, ilce, cadde, 
                           mahalle, sokak, yId, iId);
        }

        private void dgvAtanacakYetkiliListesi_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dgvAtanacakYetkiliListesi.SelectedCells[0].RowIndex;

            yId = Convert.ToInt32(dgvAtanacakYetkiliListesi.Rows[secilen].Cells[0].Value);
            txtAtanacakIsim.Text = dgvAtanacakYetkiliListesi.Rows[secilen].Cells[1].Value.ToString();
            txtAtanacakSoyisim.Text = dgvAtanacakYetkiliListesi.Rows[secilen].Cells[2].Value.ToString();
            txtAtanacakTCkimlikNumarasi.Text = dgvAtanacakYetkiliListesi.Rows[secilen].Cells[3].Value.ToString();
            txtAtanacakTelefonNumarasi.Text = dgvAtanacakYetkiliListesi.Rows[secilen].Cells[4].Value.ToString();
            txtAtanacakDogumTarihi.Text = dgvAtanacakYetkiliListesi.Rows[secilen].Cells[5].Value.ToString();
            eposta = dgvAtanacakYetkiliListesi.Rows[secilen].Cells[6].Value.ToString();
            kullaniciadi = dgvAtanacakYetkiliListesi.Rows[secilen].Cells[7].Value.ToString();
            sifre = dgvAtanacakYetkiliListesi.Rows[secilen].Cells[8].Value.ToString();
            il = dgvAtanacakYetkiliListesi.Rows[secilen].Cells[9].Value.ToString();
            ilce = dgvAtanacakYetkiliListesi.Rows[secilen].Cells[10].Value.ToString();
            cadde = dgvAtanacakYetkiliListesi.Rows[secilen].Cells[11].Value.ToString();
            mahalle = dgvAtanacakYetkiliListesi.Rows[secilen].Cells[12].Value.ToString();
            sokak = dgvAtanacakYetkiliListesi.Rows[secilen].Cells[13].Value.ToString();

            btnAtamayiGerceklestir.Enabled = true;
        }

        private void btnYetkiliOnayla_Click(object sender, EventArgs e)
        {
            YetkiliOnayla(txtYetkiliIsim.Text, txtYetkiliSoyisim.Text, 
                          Convert.ToInt64(txtTCkimlikNumarasi.Text), 
                          Convert.ToInt64(txtYetkiliTelefonNumarasi.Text), 
                          Convert.ToDateTime(txtDogumTarihi.Text), 
                          txtYetkiliEposta.Text, txtYetkiliKullaniciAdi.Text, 
                          yId, txtYetkiliSifre.Text, txtYetkiliIl.Text, 
                          txtYetkiliIlce.Text, txtYetkiliCadde.Text, 
                          txtYetkiliMahalle.Text, txtYetkiliSokak.Text);
        }

        private void btnKullaniciReddet_Click(object sender, EventArgs e)
        {
            KullaniciReddet(kId);
        }
    }       
}           
            