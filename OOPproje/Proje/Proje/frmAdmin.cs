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
    public partial class frmAdmin : Form
    {
        public frmAdmin()
        {
            InitializeComponent();
        }

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
            AdminDBEntity AdminDB = new AdminDBEntity();

            bool temp = AdminDB.PaneleGiris(txtKullaniciAdi.Text,
                                            txtSifre.Text);

            if (temp)
            {
                frmAdminPaneli IsYeri = new frmAdminPaneli();
                IsYeri.MdiParent = this.MdiParent;
                IsYeri.Show();
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
