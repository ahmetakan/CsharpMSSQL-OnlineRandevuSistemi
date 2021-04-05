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
    public partial class frmYetkili : Form
    {
        public frmYetkili()
        {
            InitializeComponent();
        }

        public static int[] ID = new int[2];

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
            YetkiliDBEntity YetkiliDB = new YetkiliDBEntity();

            bool temp = YetkiliDB.PaneleGirisVeIDtutucu(txtKullaniciAdi.Text, 
                                                        txtSifre.Text, ID);

            if (temp)
            {
                frmYetkiliPaneli Yetkili = new frmYetkiliPaneli();
                Yetkili.MdiParent = this.MdiParent;

                Yetkili.Show();
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
