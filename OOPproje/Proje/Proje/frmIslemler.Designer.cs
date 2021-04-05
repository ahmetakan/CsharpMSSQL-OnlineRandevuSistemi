
namespace Proje
{
    partial class frmIslemler
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmIslemler));
            this.msIslemMenusu = new System.Windows.Forms.MenuStrip();
            this.kullaniciGirişiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kullanıcıGirişiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kullanıcıKaydıToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yetkiliGirişiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.yetkiliGirişiToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.yetkiliKaydıToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.adminGirişiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ntfAcilis = new System.Windows.Forms.NotifyIcon(this.components);
            this.msIslemMenusu.SuspendLayout();
            this.SuspendLayout();
            // 
            // msIslemMenusu
            // 
            this.msIslemMenusu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kullaniciGirişiToolStripMenuItem,
            this.yetkiliGirişiToolStripMenuItem,
            this.adminGirişiToolStripMenuItem});
            this.msIslemMenusu.Location = new System.Drawing.Point(0, 0);
            this.msIslemMenusu.Name = "msIslemMenusu";
            this.msIslemMenusu.Size = new System.Drawing.Size(800, 24);
            this.msIslemMenusu.TabIndex = 1;
            this.msIslemMenusu.Text = "İşlem Menüsü";
            // 
            // kullaniciGirişiToolStripMenuItem
            // 
            this.kullaniciGirişiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.kullanıcıGirişiToolStripMenuItem,
            this.kullanıcıKaydıToolStripMenuItem});
            this.kullaniciGirişiToolStripMenuItem.Name = "kullaniciGirişiToolStripMenuItem";
            this.kullaniciGirişiToolStripMenuItem.Size = new System.Drawing.Size(111, 20);
            this.kullaniciGirişiToolStripMenuItem.Text = "Kullanıcı İşlemleri";
            // 
            // kullanıcıGirişiToolStripMenuItem
            // 
            this.kullanıcıGirişiToolStripMenuItem.Name = "kullanıcıGirişiToolStripMenuItem";
            this.kullanıcıGirişiToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.kullanıcıGirişiToolStripMenuItem.Text = "Kullanıcı Girişi";
            this.kullanıcıGirişiToolStripMenuItem.Click += new System.EventHandler(this.kullanıcıGirişiToolStripMenuItem_Click);
            // 
            // kullanıcıKaydıToolStripMenuItem
            // 
            this.kullanıcıKaydıToolStripMenuItem.Name = "kullanıcıKaydıToolStripMenuItem";
            this.kullanıcıKaydıToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.kullanıcıKaydıToolStripMenuItem.Text = "Kullanıcı Kaydı";
            this.kullanıcıKaydıToolStripMenuItem.Click += new System.EventHandler(this.kullanıcıKaydıToolStripMenuItem_Click);
            // 
            // yetkiliGirişiToolStripMenuItem
            // 
            this.yetkiliGirişiToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.yetkiliGirişiToolStripMenuItem1,
            this.yetkiliKaydıToolStripMenuItem});
            this.yetkiliGirişiToolStripMenuItem.Name = "yetkiliGirişiToolStripMenuItem";
            this.yetkiliGirişiToolStripMenuItem.Size = new System.Drawing.Size(97, 20);
            this.yetkiliGirişiToolStripMenuItem.Text = "Yetkili İşlemleri";
            // 
            // yetkiliGirişiToolStripMenuItem1
            // 
            this.yetkiliGirişiToolStripMenuItem1.Name = "yetkiliGirişiToolStripMenuItem1";
            this.yetkiliGirişiToolStripMenuItem1.Size = new System.Drawing.Size(137, 22);
            this.yetkiliGirişiToolStripMenuItem1.Text = "Yetkili Girişi";
            this.yetkiliGirişiToolStripMenuItem1.Click += new System.EventHandler(this.yetkiliGirişiToolStripMenuItem1_Click);
            // 
            // yetkiliKaydıToolStripMenuItem
            // 
            this.yetkiliKaydıToolStripMenuItem.Name = "yetkiliKaydıToolStripMenuItem";
            this.yetkiliKaydıToolStripMenuItem.Size = new System.Drawing.Size(137, 22);
            this.yetkiliKaydıToolStripMenuItem.Text = "Yetkili Kaydı";
            this.yetkiliKaydıToolStripMenuItem.Click += new System.EventHandler(this.yetkiliKaydıToolStripMenuItem_Click);
            // 
            // adminGirişiToolStripMenuItem
            // 
            this.adminGirişiToolStripMenuItem.Name = "adminGirişiToolStripMenuItem";
            this.adminGirişiToolStripMenuItem.Size = new System.Drawing.Size(84, 20);
            this.adminGirişiToolStripMenuItem.Text = "Admin Girişi";
            this.adminGirişiToolStripMenuItem.Click += new System.EventHandler(this.adminGirişiToolStripMenuItem_Click);
            // 
            // ntfAcilis
            // 
            this.ntfAcilis.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.ntfAcilis.BalloonTipText = "Randevu Yönetim Paneline Hoşgeldiniz.";
            this.ntfAcilis.BalloonTipTitle = "Seyrek A.Ş. Yönetim Paneli";
            this.ntfAcilis.Icon = ((System.Drawing.Icon)(resources.GetObject("ntfAcilis.Icon")));
            this.ntfAcilis.Text = "DENEME";
            this.ntfAcilis.Visible = true;
            // 
            // frmIslemler
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 728);
            this.Controls.Add(this.msIslemMenusu);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.msIslemMenusu;
            this.Name = "frmIslemler";
            this.Text = "Islemler";
            this.Shown += new System.EventHandler(this.frmIslemler_Shown);
            this.msIslemMenusu.ResumeLayout(false);
            this.msIslemMenusu.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip msIslemMenusu;
        private System.Windows.Forms.ToolStripMenuItem kullaniciGirişiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yetkiliGirişiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem adminGirişiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kullanıcıGirişiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kullanıcıKaydıToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem yetkiliGirişiToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem yetkiliKaydıToolStripMenuItem;
        private System.Windows.Forms.NotifyIcon ntfAcilis;
    }
}