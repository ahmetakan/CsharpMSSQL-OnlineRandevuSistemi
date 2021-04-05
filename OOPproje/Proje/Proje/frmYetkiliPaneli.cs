using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Proje
{
	public partial class frmYetkiliPaneli : Form
	{
		public frmYetkiliPaneli()
		{
			InitializeComponent();
		}


		public DateTime tarih;
		public int kId, iId, rId, sId;
		public string[] Kisi = new string[2];
		public string[] SaatAraligi = new string[1];
		public bool[] Aktar = new bool[8];
		public DateTime TalepTarihi;

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
			}

			btnTalepKabulEt.Enabled = false;
			btnTalepReddet.Enabled = false;
		}

		private void RandevuAyari(int iID, DateTime tarih, 
								  bool saat8_9, bool saat9_10, 
								  bool saat10_11, bool saat11_12, 
								  bool saat13_14, bool saat14_15, 
								  bool saat15_16, bool saat16_17)
		{
			

			YetkiliDBEntity YetkiliDB = new YetkiliDBEntity();

			YetkiliDB.UygunRandevuAyari(iID, tarih, saat8_9,saat9_10, 
										saat10_11,saat11_12, saat13_14, 
										saat14_15, saat15_16,saat16_17);

			MessageBox.Show("Randevu uygun saat aralığı ile " + "\n" +
							"ilgili yaptığınız güncelleme " + "\n" +
							"başarıyla gerçekleştirildi.", "Bilgi", 
							MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void RandevuBilgiGetir(int id, string tablo,  string tablo2, 
									   string[] kisi, string[] saataraligi, int rid)
		{
			YetkiliDBEntity YetkiliDB = new YetkiliDBEntity();

			YetkiliDB.RandevuTalepBilgileri(id, tablo, tablo2, kisi, saataraligi, rid);
		}

		private void RandevuOnayla(int iId, int kId, DateTime taleptarihi, 
								   bool saat8_9, bool saat9_10, 
								   bool saat10_11, bool saat11_12, 
								   bool saat13_14, bool saat14_15, 
								   bool saat15_16, bool saat16_17)
		{
			YetkiliDBEntity YetkiliDB = new YetkiliDBEntity();

			YetkiliDB.RandevuOnayla(iId, kId, taleptarihi, saat8_9,
									saat9_10, saat10_11, saat11_12,
									saat13_14, saat14_15, saat15_16,
									saat16_17);

			RandevuTalepSil("tbl_RandevuTalebi", rId);
		}

		private void RandevuTalepSil(string tablo, int id)
		{
			YetkiliDBEntity YetkiliDB = new YetkiliDBEntity();

			YetkiliDB.SatirSil(tablo, id);

			dgvRandevuTalepListesi.DataSource = YetkiliDB.RandevuTalepListele
												(frmYetkili.ID[1], "tbl_RandevuTalebi").Tables[0];

			btnTalepKabulEt.Enabled = false;
			btnTalepReddet.Enabled = false;

			EkranTemizle(pnlTalepBilgileri);
		}

		private void SoruListesiGoster()
		{
			YetkiliDBEntity YetkiliDB = new YetkiliDBEntity();

			dgvSoruListesi.DataSource = YetkiliDB.SoruListele
										(frmYetkili.ID[1], 0, "IsYeriID = ").Tables[0];

			rtbSoru.Clear();
			rtbCevap.Clear();
		}
		private void SoruCevapla(int yetkiliId, string cevap, int soruId)
		{
			YetkiliDBEntity YetkiliDB = new YetkiliDBEntity();

			YetkiliDB.SoruCevapla(yetkiliId, cevap, soruId);

			SoruListesiGoster();

			MessageBox.Show("Cevabınız başarıyla iletildi. " + "\n" +
							"Kullanıcı, kendi panelinden sorduğu " + "\n" +
							"sorunun cevabını görüntüleyebilir.", "Bilgi", 
							MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void RaporListele(int id, string tablo, DataGridView dgv)
		{
			YetkiliDBEntity YetkiliDB = new YetkiliDBEntity();

			dgv.DataSource = YetkiliDB.RandevuTalepListele(id, tablo).Tables[0];
		}

		private void RaporListele2(int id, DataGridView dgv, string column)
		{
			YetkiliDBEntity YetkiliDB = new YetkiliDBEntity();

			dgv.DataSource = YetkiliDB.SoruListele(id, 0, column).Tables[0];
		}

		private void btnIlet_Click(object sender, EventArgs e)
		{
			SoruCevapla(frmYetkili.ID[0], rtbCevap.Text, sId);
		}

		private void btnRandevuSaatiAyarla_Click(object sender, EventArgs e)
		{
			tarih = mcRandevuTakvim.SelectionStart;

			RandevuAyari(frmYetkili.ID[1], tarih, chb8_9.Checked, 
						 chb9_10.Checked, chb10_11.Checked, 
						 chb11_12.Checked, chb13_14.Checked, 
						 chb14_15.Checked, chb15_16.Checked, 
						 chb16_17.Checked);

		}

		private void btnRandevuTalepListesi_Click(object sender, EventArgs e)
		{
			YetkiliDBEntity YetkiliDB = new YetkiliDBEntity();

			dgvRandevuTalepListesi.DataSource = YetkiliDB.RandevuTalepListele
												(frmYetkili.ID[1], "tbl_RandevuTalebi").Tables[0];

			EkranTemizle(pnlTalepBilgileri);
		}

		private void btnTalepKabulEt_Click(object sender, EventArgs e)
		{
			RandevuOnayla(iId, kId, TalepTarihi, Aktar[0], 
						  Aktar[1], Aktar[2], Aktar[3], 
						  Aktar[4], Aktar[5], Aktar[6], 
						  Aktar[7]);
		}

		private void btnSoruListesiGoster_Click(object sender, EventArgs e)
		{
			SoruListesiGoster();
		}

		private void btnRaporRandevular_Click(object sender, EventArgs e)
		{
			RaporListele(frmYetkili.ID[1], "tbl_KayitliRandevu", dgvRaporRandevular);
		}

		private void btnRaporSorular_Click(object sender, EventArgs e)
		{
			RaporListele(frmYetkili.ID[1], "tbl_SoruCevap", dgvRaporSorular);
		}

		private void btnRaporCevaplanmamisSorular_Click(object sender, EventArgs e)
		{
			RaporListele2(frmYetkili.ID[1], dgvRaporCevaplanmamisSorular, "IsYeriID = ");
		}

		private void dgvSoruListesi_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			int secilen = dgvSoruListesi.SelectedCells[0].RowIndex;

			sId = Convert.ToInt32(dgvSoruListesi.Rows[secilen].Cells[0].Value);

			rtbSoru.Text = dgvSoruListesi.Rows[secilen].Cells[3].Value.ToString();
		}

		private void btnTalepReddet_Click(object sender, EventArgs e)
		{
			RandevuTalepSil("tbl_RandevuTalebi", rId);
		}

		private void dgvRandevuTalepListesi_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
		{
			int secilen = dgvRandevuTalepListesi.SelectedCells[0].RowIndex;

			rId = Convert.ToInt32(dgvRandevuTalepListesi.Rows[secilen].Cells[0].Value);
			iId = Convert.ToInt32(dgvRandevuTalepListesi.Rows[secilen].Cells[1].Value);
			kId = Convert.ToInt32(dgvRandevuTalepListesi.Rows[secilen].Cells[2].Value);

			TalepTarihi = Convert.ToDateTime(dgvRandevuTalepListesi.Rows[secilen].Cells[3].Value);
			Aktar[0] = Convert.ToBoolean(dgvRandevuTalepListesi.Rows[secilen].Cells[4].Value);
			Aktar[1] = Convert.ToBoolean(dgvRandevuTalepListesi.Rows[secilen].Cells[5].Value);
			Aktar[2] = Convert.ToBoolean(dgvRandevuTalepListesi.Rows[secilen].Cells[6].Value);
			Aktar[3] = Convert.ToBoolean(dgvRandevuTalepListesi.Rows[secilen].Cells[7].Value);
			Aktar[4] = Convert.ToBoolean(dgvRandevuTalepListesi.Rows[secilen].Cells[8].Value);
			Aktar[5] = Convert.ToBoolean(dgvRandevuTalepListesi.Rows[secilen].Cells[9].Value);
			Aktar[6] = Convert.ToBoolean(dgvRandevuTalepListesi.Rows[secilen].Cells[10].Value);
			Aktar[7] = Convert.ToBoolean(dgvRandevuTalepListesi.Rows[secilen].Cells[11].Value);

			RandevuBilgiGetir(kId, "tbl_KayitliKullanici", "tbl_RandevuTalebi", Kisi, SaatAraligi, rId);

			txtIsim.Text = Kisi[0];
			txtSoyisim.Text = Kisi[1];
			txtTalepTarihi.Text = TalepTarihi.ToShortDateString();
			txtTalepSaati.Text = SaatAraligi[0];

			btnTalepKabulEt.Enabled = true;
			btnTalepReddet.Enabled = true;
		}
	}
}
