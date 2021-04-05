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
    public partial class frmKullaniciKayitOl : Form
    {
        public frmKullaniciKayitOl()
        {
            InitializeComponent();
        }

        public bool EkranKontrol(Control control, Control control2, 
                                 Control control3)
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

            foreach (Control nesne in control3.Controls)
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

        public bool EkranKontrol2(TextBox TelefonNo, TextBox Sifre, 
                                  TextBox SifreOnay)
        {
            if (10000000000 > Convert.ToInt64(TelefonNo.Text) && 
                999999999 < Convert.ToInt64(TelefonNo.Text))
            {
                if (Sifre.Text == SifreOnay.Text)
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Şifre ile şifre tekrarı aynı olmalıdır.", 
                                    "Uyarı", MessageBoxButtons.OK, 
                                    MessageBoxIcon.Warning);

                    Sifre.Focus();

                    return false;
                }
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

        private bool KayitKontrol(TextBox kullaniciadi, TextBox eposta)
        {
            KullaniciDBEntity KullaniciDB = new KullaniciDBEntity();

            int temp = KullaniciDB.KayitKontrolu(kullaniciadi.Text, eposta.Text);

            if (temp == 1)
            {
                MessageBox.Show("Bu eposta halihazırda kullanılmaktadır." + "\n" +
                                "Lütfen farklı bir eposta tercih ediniz.", "Uyarı",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);

                eposta.Focus();
                
                return false;
            }

            else if (temp == 2)
            {
                MessageBox.Show("Bu kullanıcı adı halihazırda " + "\n" +
                                "kullanılmaktadır. Lütfen farklı " + "\n" +
                                "bir kullanıcı adı tercih ediniz.", "Uyarı",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);

                kullaniciadi.Focus();

                return false;
            }

            else
            {
                return true;
            }
        }

        private void EkranTemizle(Control control, Control control2, 
                                  Control control3, TextBox txt)
        {
            foreach (Control nesne in control.Controls)
            {
                if (nesne is TextBox)
                {
                    ((TextBox)nesne).Clear();
                }
            }

            foreach (Control nesne in control2.Controls)
            {
                if (nesne is TextBox)
                {
                    ((TextBox)nesne).Clear();
                }
            }

            foreach (Control nesne in control3.Controls)
            {
                if (nesne is TextBox)
                {
                    ((TextBox)nesne).Clear();
                }
            }

            txt.Focus();
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

        private void BasvuruEkle(string isim, string soyisim, long telefonnumarasi, 
                                 string kullaniciadi, string sifre, string eposta, 
                                 string il, string ilce, string cadde, string mahalle, 
                                 string sokak)

        {
            KullaniciDBEntity KullaniciDB = new KullaniciDBEntity();

            Kullanici kullanici = new Kullanici
            {
                Isim = isim,
                SoyIsim = soyisim,
                TelefonNumarasi = telefonnumarasi,
                KullaniciAdi = kullaniciadi,
                Sifre = sifre,
                Eposta = eposta
            };

            kullanici.Adres.Il = il;
            kullanici.Adres.Ilce = ilce;
            kullanici.Adres.Cadde = cadde;
            kullanici.Adres.Mahalle = mahalle;
            kullanici.Adres.Sokak = sokak;

            KullaniciDB.KullaniciEkle(kullanici, "tbl_KullaniciBasvuru");

            MessageBox.Show("Kullanıcı kayıt başvurusu " + "\n" +
                            "başarıyla gerçekleştirildi. " + "\n" +
                            "Sisteme giriş yapabilmeniz için " + "\n" +
                            "başvurunuzun yetkililer tarafından " + "\n" +
                            "onaylanması gerekmektedir.", "Bilgi",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

            EkranTemizle(pnlKisiselBilgiler, pnlGirisBilgileri,
                         pnlAdresBilgileri, txtIsim);
        }

        private void btnKayıtOl_Click(object sender, EventArgs e)
        {
            if (EkranKontrol(pnlKisiselBilgiler, pnlGirisBilgileri, pnlAdresBilgileri))
            {
                if (EkranKontrol2(txtTelefonNo, txtSifre, txtSifreOnay))
                {
                    if (KayitKontrol(txtKullaniciAdi, txtEposta))
                    {
                        BasvuruEkle(txtIsim.Text, txtSoyisim.Text,
                                    Convert.ToInt64(txtTelefonNo.Text),
                                    txtKullaniciAdi.Text, txtSifre.Text,
                                    txtEposta.Text, txtil.Text, txtilce.Text,
                                    txtCadde.Text, txtMahalle.Text, txtSokak.Text);
                    }
                }
            }
        }

        private void txtIsim_KeyPress(object sender, KeyPressEventArgs e)
        {
            SadeceHarf(e);
        }

        private void txtSoyisim_KeyPress(object sender, KeyPressEventArgs e)
        {
            SadeceHarf(e);
        }

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

        private void txtTelefonNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            SadeceSayi(e);
        }
    }
}
