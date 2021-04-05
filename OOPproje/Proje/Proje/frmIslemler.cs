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
    public partial class frmIslemler : Form
    {
        public frmIslemler()
        {
            InitializeComponent();
        }

        public void FormHazirla(Form frm)
        {

            if (this.MdiChildren.Count() == 0)
            {
                frm.MdiParent = this;
                frm.Show();
            }

            else
            {
                MessageBox.Show("Aynı anda birden fazla pencere" + "\n" +
                                "açılamaz. Lütfen açık olan pencereyi" + "\n" +
                                "kapatarak tekrar deneyiniz.", "Uyarı",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public void AcilisBildirim()
        {
            ntfAcilis.BalloonTipTitle = "Seyrek A.Ş. Yönetim Paneli";
            ntfAcilis.BalloonTipText = "Randevu Yönetim Paneline Hoşgeldiniz.";
            ntfAcilis.BalloonTipIcon = ToolTipIcon.Info;
            ntfAcilis.ShowBalloonTip(3000);
        }


        private void adminGirişiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAdmin Admin = new frmAdmin();

            FormHazirla(Admin);
        }

        private void kullanıcıGirişiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmKullanici Kullanici = new frmKullanici();

            FormHazirla(Kullanici);
        }

        private void kullanıcıKaydıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmKullaniciKayitOl KayitOl = new frmKullaniciKayitOl();

            FormHazirla(KayitOl);
        }

        private void yetkiliGirişiToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmYetkili Yetkili = new frmYetkili();

            FormHazirla(Yetkili);
        }

        private void yetkiliKaydıToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            frmYetkiliKayitOl KayitOl = new frmYetkiliKayitOl();

            FormHazirla(KayitOl);
        }

        private void frmIslemler_Shown(object sender, EventArgs e)
        {
            AcilisBildirim();
        }

    }
    
}
