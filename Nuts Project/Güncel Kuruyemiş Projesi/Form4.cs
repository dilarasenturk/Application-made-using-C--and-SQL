using MaterialSkin;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Güncel_Kuruyemiş_Projesi
{
    public partial class Form4 : MaterialSkin.Controls.MaterialForm
    {
        public Form4()
        {
            InitializeComponent();
            InitializeMaterialSkin();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MinimumSize = new Size(1024, 600);
            this.MaximumSize = new Size(1024, 600);
        }
        private void InitializeMaterialSkin()
        {
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            materialSkinManager.ColorScheme = new ColorScheme(
                Primary.Orange700,
                Primary.Orange800,
                Primary.Orange400,
                Accent.Orange200,
                TextShade.WHITE);
            this.BackColor = Color.FromArgb(165, 42, 42);
        }
        private void StartBackgroundWorker()
        {
            backgroundWorker1.DoWork += BackgroundWorker1_DoWork;
            backgroundWorker1.RunWorkerCompleted += BackgroundWorker1_RunWorkerCompleted;

            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.RunWorkerAsync();
        }
        private void BackgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            backgroundWorker1.ReportProgress(100);
        }
        private void BackgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        private void Form4_Load(object sender, EventArgs e)
        {
            string connectionString = "Data Source=DESKTOP-JJFORKS\\SQLEXPRESS;Initial Catalog=Kuruyemiş;Integrated Security=True";

            string[] urunAdlari = { "Antep Fıstığı", "Siirt Fıstığı", "Yer Fıstığı", "Çekirdek", "Fındık", "Badem", "Ceviz", "Kabak Çekirdeği",
            "Soslu Mısır", "Leblebi", "Badem İçi", "Fındık İçi", "Kabuksuz Yer Fıstığı", "Kabuksuz Kaju", "Ceviz İçi", "Antep Fıstığı İçi",
            "Çiğ Badem", "Çiğ Kaju", "Çiğ Fındık", "Çiğ Çekirdek", "Çiğ Kabak İçi Çekirdeği","Fıstıklı Baklava", "Sütlü Nuriye", "Şekerpare", 
            "Tulumba Tatlısı", "Kadayıf", "Şöbiyet", "Halka Tatlısı", "Soğuk Baklava", "Profiterol", "Sütlaç", "Ekler", "Trileçe", "Tavuk Göğsü",
            "Muhallebi", "Kazandibi", "Ekmek Kadayıfı", "Çikolatalı Pasta", "Meyveli Pasta", "Tuzlu", "Tatlı", "Sade Lokum", "Fıstıklı Lokum", 
            "Güllü Lokum", "Cevizli Lokum", "Fındıklı Lokum", "Çikolatalı Lokum", "Fındıklı Draje", "Portakallı Draje", "Fıstıklı Draje", "Üzümlü Draje", 
            "Badem Draje",  "Hurma", "Üzüm", "Kayısı", "İncir", "Dut Kurusu", "İğde", "Türk Kahvesi", "Kahve Çekirdeği", "Granül Kahve", "Filtre Kahve"};


            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                for (int i = 0; i < urunAdlari.Length; i++)
                {
                    string urunAdi = urunAdlari[i];
                    string sorgu = "SELECT Fiyat FROM Urunler WHERE Urun_Adi = @urunAdi" + (i + 1);

                    using (SqlCommand cmd = new SqlCommand(sorgu, conn))
                    {
                        cmd.Parameters.AddWithValue("@urunAdi" + (i + 1), urunAdi);

                        try
                        {
                            conn.Open();
                            object result = cmd.ExecuteScalar();

                            if (result != null)
                            {
                                Label label = Controls.Find("label" + (i + 1), true).FirstOrDefault() as Label;
                                label.Text = "Product Name: " + urunAdi + "\nPrice: " + result.ToString() + " TL";
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Hata: " + ex.Message);
                        }
                        finally
                        {
                            conn.Close();
                        }
                    }
                }
            }
        }
        private void materialButton1_Click(object sender, EventArgs e)
        {
            Form fr2 = new Form2();
            fr2.Show();
            this.Hide();
        }
    }
}
