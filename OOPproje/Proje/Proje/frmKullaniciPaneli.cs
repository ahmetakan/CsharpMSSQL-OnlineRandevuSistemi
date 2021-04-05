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
    public partial class frmKullaniciPaneli : Form
    {
        public frmKullaniciPaneli()
        {
            InitializeComponent();
        }

        public int iId, temp, tempsayi, tempsayi2;
        public DateTime[] tarih;
        public DateTime secilentarih;
        public bool[,] Bool;
        public DateTime[] temptarih;
        public bool[,] Bool2;
        public int SatirSayisi, SatirSayisi2;


        private void IsYeriListesi(DataGridView dgv, Control control)
        {
            KullaniciDBEntity KullaniciDB = new KullaniciDBEntity();
            dgv.DataSource = KullaniciDB.Listele("tbl_IsYeri").Tables[0];

            EkranTemizle(control);
        }

        private void RadiobtnSıfırla(Control control)
        {
            foreach (Control nesne in control.Controls)
            {
                if (nesne is RadioButton)
                {
                    ((RadioButton)nesne).Checked = false;
                }
                if (nesne.Controls.Count > 0)
                {
                    RadiobtnSıfırla(nesne);
                }
            }
        }

        public int RadiobtnKontrol(Control control, int temp)
        {
            foreach (Control nesne in control.Controls)
            {
                if (nesne is RadioButton)
                {
                    if(((RadioButton)nesne).Checked == true)
                    {
                        temp++;
                    }
                }
            }

            return temp;
        }

        public void EkranTemizle(Control control)
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
                if (nesne is RichTextBox)
                {
                    ((RichTextBox)nesne).Clear();
                }
                if (nesne.Controls.Count > 0)
                {
                    EkranTemizle(nesne);
                }
            }
        }

        public bool EkranKontrol(RichTextBox rtb)
        {
            if (rtb.Text != "")
            {
                return true;
            }

            else
            {
                MessageBox.Show("Lütfen sorunuzu giriniz.", "Uyarı",
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return false;
            }
        }

        private void RandevuAraligiOku(int iId, DateTime[] tarih1, bool[,] Bool)
        {
            secilentarih = mcRandevuTakvim.SelectionStart;

            KullaniciDBEntity KullaniciDB = new KullaniciDBEntity();

            tempsayi2 = KullaniciDB.RandevuAraligi
                        (iId, tarih1, Bool, "tbl_UygunRandevu", SatirSayisi2).Item3;

            Bool = new bool[tempsayi2, 8];
            Bool = KullaniciDB.RandevuAraligi
                   (iId, tarih1, Bool, "tbl_UygunRandevu", SatirSayisi2).Item2;


            tarih1 = new DateTime[tempsayi2];
            tarih1 = KullaniciDB.RandevuAraligi
                     (iId, tarih1, Bool, "tbl_UygunRandevu", SatirSayisi2).Item1;

            tempsayi = KullaniciDB.KayitliRandevuAraligi
                       (iId, temptarih, "tbl_KayitliRandevu", Bool2, SatirSayisi).Item1;

            Bool2 = new bool[tempsayi,8];
            Bool2 = KullaniciDB.KayitliRandevuAraligi
                    (iId, temptarih, "tbl_KayitliRandevu", Bool2, SatirSayisi).Item2;

            temptarih = new DateTime[tempsayi];
            temptarih = KullaniciDB.KayitliRandevuAraligi
                        (iId, temptarih, "tbl_KayitliRandevu", Bool2, SatirSayisi).Item3;

            RadiobtnSıfırla(grpRandevuAlinabilecekSaatler);

            int temp = 0;

            for (int i = 0; i < tempsayi2; i++)
            {
                if (secilentarih == tarih1[i])
                {
                    rb8_9.Enabled = Bool[i,0];
                    rb9_10.Enabled = Bool[i,1];
                    rb10_11.Enabled = Bool[i,2];
                    rb11_12.Enabled = Bool[i,3];
                    rb13_14.Enabled = Bool[i,4];
                    rb14_15.Enabled = Bool[i,5];
                    rb15_16.Enabled = Bool[i,6];
                    rb16_17.Enabled = Bool[i,7];

                    temp++;
                }
            }

            if(temp == 0)
            {
                rb8_9.Enabled = true;
                rb9_10.Enabled = true;
                rb10_11.Enabled = true;
                rb11_12.Enabled = true;
                rb13_14.Enabled = true;
                rb14_15.Enabled = true;
                rb15_16.Enabled = true;
                rb16_17.Enabled = true;
            }

            for (int i = 0; i < tempsayi; i++)
            {
                if (secilentarih == temptarih[i])
                {
                    if (Bool2[i, 0] == true) { rb8_9.Enabled = false; }
                    else if (Bool2[i, 1] == true) { rb9_10.Enabled = false; }
                    else if (Bool2[i, 2] == true) { rb10_11.Enabled = false; }
                    else if (Bool2[i, 3] == true) { rb11_12.Enabled = false; }
                    else if (Bool2[i, 4] == true) { rb13_14.Enabled = false; }
                    else if (Bool2[i, 5] == true) { rb14_15.Enabled = false; }
                    else if (Bool2[i, 6] == true) { rb15_16.Enabled = false; }
                    else if (Bool2[i, 7] == true) { rb16_17.Enabled = false; }
                }
            }
        }

        private void RandevuTalebiGonder(int iID, int kId, DateTime tarih,
                                         bool saat8_9, bool saat9_10,
                                         bool saat10_11, bool saat11_12,
                                         bool saat13_14, bool saat14_15,
                                         bool saat15_16, bool saat16_17)
        {
            KullaniciDBEntity KullaniciDB = new KullaniciDBEntity();

            if (RadiobtnKontrol(grpRandevuAlinabilecekSaatler, temp) > 0)
            {
                KullaniciDB.RandevuTalebi(iID, kId, tarih, saat8_9, saat9_10,
                                          saat10_11, saat11_12, saat13_14,
                                          saat14_15, saat15_16, saat16_17);

                MessageBox.Show("Randevu talebiniz başarıyla " + "\n" +
                                "oluşturuldu. Randevu talebinizin " + "\n" +
                                "onaylanması durumunda randevunuzu " + "\n" +
                                "'Randevularım' sekmesinde " + "\n" +
                                "görüntüleyebilirsiniz.", "Bilgi", 
                                MessageBoxButtons.OK, MessageBoxIcon.Information);

                RadiobtnSıfırla(grpRandevuAlinabilecekSaatler);
            }

            else
            {
                MessageBox.Show("Lütfen saat aralığını belirleyiniz.", "Uyarı", 
                                MessageBoxButtons.OK, MessageBoxIcon.Warning);

                RadiobtnSıfırla(grpRandevuAlinabilecekSaatler);
            }
        }

        private void SoruSor(int isyeriId, int kullaniciId, string soru, bool durum)
        {
            KullaniciDBEntity KullaniciDB = new KullaniciDBEntity();

            KullaniciDB.SoruSor(isyeriId, kullaniciId, soru, durum);

            MessageBox.Show("Sorunuz başarıyla iletildi." + "\n" +
                            "Yetkilinin, sorunuzu cevaplaması " + "\n" +
                            "durumunda cevabı 'Sorularım' " + "\n" +
                            "sekmesinden öğrenebilirsiniz."  , "Bilgi", 
                            MessageBoxButtons.OK, MessageBoxIcon.Information);

            rtbSoru.Clear();

            EkranTemizle(pnlSoruSorIsYeriGenelBilgiler);
        }

        private void RandevularimiGoster(int KullaniciID, DataGridView dgv)
        {
            KullaniciDBEntity KullaniciDB = new KullaniciDBEntity();

            dgv.DataSource = KullaniciDB.Randevularim(KullaniciID).Tables[0];
        }

        private void IsYeriFiltrele(string isyeriadi, string isyerituru, DataGridView dgv)
        {
            KullaniciDBEntity KullaniciDB = new KullaniciDBEntity();

            dgv.DataSource = KullaniciDB.IsYeriFiltrele(isyeriadi, isyerituru).Tables[0];
        }

        private void SorularimiGoster(int id, DataGridView dgv, int durum, string column)
        {
            YetkiliDBEntity YetkiliDB = new YetkiliDBEntity();

            dgv.DataSource = YetkiliDB.SoruListele(id, durum, column).Tables[0];
        }

        private void btnRandevuIsYeriListesi_Click(object sender, EventArgs e)
        {
            IsYeriListesi(dgvIsYeriListesi, pnlisYeriGenelBilgiler);
        }

        private void dgvIsYeriListesi_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dgvIsYeriListesi.SelectedCells[0].RowIndex;

            iId = Convert.ToInt32(dgvIsYeriListesi.Rows[secilen].Cells[0].Value);
            txtIsYeriAdi.Text = dgvIsYeriListesi.Rows[secilen].Cells[1].Value.ToString();
            txtIsYeriTuru.Text = dgvIsYeriListesi.Rows[secilen].Cells[2].Value.ToString();
            txtIsYeriTelefonNumarasi.Text = dgvIsYeriListesi.Rows[secilen].Cells[3].Value.ToString();

            mcRandevuTakvim.Enabled = true;
            btnRandevuSaatiAyarla.Enabled = true;

            RandevuAraligiOku(iId, tarih, Bool);
        }

        private void btnRandevuSaatiAyarla_Click(object sender, EventArgs e)
        {
            RandevuTalebiGonder(iId, frmKullanici.ID[0], secilentarih, 
                                rb8_9.Checked, rb9_10.Checked, 
                                rb10_11.Checked, rb11_12.Checked, 
                                rb13_14.Checked, rb14_15.Checked, 
                                rb15_16.Checked, rb16_17.Checked);
        }

        private void btnSoruIsYeriListesi_Click(object sender, EventArgs e)
        {
            IsYeriListesi(dgvSoruSorIsYeriListesi, tabSoruSor);

            rtbSoru.Enabled = false;
            btnilet.Enabled = false;
        }

        private void dgvSoruSorIsYeriListesi_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dgvSoruSorIsYeriListesi.SelectedCells[0].RowIndex;

            iId = Convert.ToInt32(dgvSoruSorIsYeriListesi.Rows[secilen].Cells[0].Value);
            txtSoruIsYeriAdi.Text = dgvSoruSorIsYeriListesi.Rows[secilen].Cells[1].Value.ToString();
            txtSoruIsYeriTuru.Text = dgvSoruSorIsYeriListesi.Rows[secilen].Cells[2].Value.ToString();
            txtSoruIsYeriTelefonNumarasi.Text = dgvSoruSorIsYeriListesi.Rows[secilen].Cells[3].Value.ToString();

            rtbSoru.Clear();

            rtbSoru.Enabled = true;
            btnilet.Enabled = false;
        }

        private void btnCevaplanmisSorulariGöster_Click(object sender, EventArgs e)
        {
            EkranTemizle(pnlCevaplanmisSorular);

            SorularimiGoster(frmKullanici.ID[0], dgvCevaplanmisSorular, 1, "SoranID = ");
        }

        private void btnCevaplanmamisSoruGoster_Click(object sender, EventArgs e)
        {
            EkranTemizle(pnlCevaplanmamisSorular);

            SorularimiGoster(frmKullanici.ID[0], dgvCevaplanmamisSorular, 0, "SoranID = ");
        }

        private void dgvCevaplanmisSorular_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dgvCevaplanmisSorular.SelectedCells[0].RowIndex;

            rtbCevaplanmisSorularSoru.Text = dgvCevaplanmisSorular.Rows[secilen].Cells[3].Value.ToString();
            rtbCevaplanmisSorularCevap.Text = dgvCevaplanmisSorular.Rows[secilen].Cells[5].Value.ToString();
        }

        private void btnIsYeriAraTumunuListele_Click(object sender, EventArgs e)
        {
            IsYeriListesi(dgvIsYeriAraListe, pnlIsYeriAra);
        }

        private void txtIsYeriAraTur_TextChanged(object sender, EventArgs e)
        {
            IsYeriFiltrele(txtIsYeriAraAd.Text, txtIsYeriAraTur.Text, dgvIsYeriAraListe);
        }

        private void txtIsYeriAraAd_TextChanged(object sender, EventArgs e)
        {
            IsYeriFiltrele(txtIsYeriAraAd.Text, txtIsYeriAraTur.Text, dgvIsYeriAraListe);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            RandevularimiGoster(frmKullanici.ID[0], dgvRandevularim);
        }

        private void dgvCevaplanmamisSorular_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dgvCevaplanmamisSorular.SelectedCells[0].RowIndex;

            rtbCevaplanmamisSorularSoru.Text = dgvCevaplanmamisSorular.Rows[secilen].Cells[3].Value.ToString();
        }

        private void btnilet_Click(object sender, EventArgs e)
        {
            if (EkranKontrol(rtbSoru))
            {
                SoruSor(iId, frmKullanici.ID[0], rtbSoru.Text, false);
                rtbSoru.Enabled = false;
                btnilet.Enabled = false;
            }
        }

        private void rtbSoru_TextChanged(object sender, EventArgs e)
        {
            btnilet.Enabled = true;
        }

        private void mcRandevuTakvim_DateChanged(object sender, DateRangeEventArgs e)
        {
            RandevuAraligiOku(iId, tarih, Bool);
        }

    }
}
