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
    public partial class frmKullanici : Form
    {
        public frmKullanici()
        {
            InitializeComponent();
        }

        public static int[] ID = new int[1];
        public static DateTime SonGiris;

        public bool EkranKontrol(Control control)
        {
            int temp = 0;

            foreach (Control nesne in control.Controls)
            {
                if (nesne is TextBox && nesne.Text == "")
                {
                    if (temp == 0)
                    {
                        MessageBox.Show("Lütfen alanları eksiksiz " + "\n" +
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

        private void GirisYap()
        {
            KullaniciDBEntity KullaniciDB = new KullaniciDBEntity();

            bool temp = KullaniciDB.PaneleGirisVeIDtutucu(txtKullaniciAdi.Text,
                                                        txtSifre.Text, ID);

            if (temp)
            {
                frmKullaniciPaneli Kullanici = new frmKullaniciPaneli();
                Kullanici.MdiParent = this.MdiParent;

                SonGiris = DateTime.Now;

                KullaniciDB.SonGirisZamaniTut(ID[0], SonGiris);

                Kullanici.Show();
                this.Close();
            }

            else
            {
                MessageBox.Show("Geçersiz kullanıcı adı veya şifre.", "Hata", 
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnGirisYap_Click(object sender, EventArgs e)
        {
            if (EkranKontrol(this))
            {
                GirisYap();
            }
            
        }
    }
}
