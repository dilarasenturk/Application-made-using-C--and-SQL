using MaterialSkin;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace Güncel_Kuruyemiş_Projesi
{
    public partial class Form2 : MaterialSkin.Controls.MaterialForm
    {
        public Form2()
        {
            InitializeComponent();
            InitializeMaterialSkin();
            panel1.Visible = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MinimumSize = new Size(1240, 665);
            this.MaximumSize = new Size(1240, 665);
            numericUpDown1.Value = 1;
            numericUpDown1.Minimum = 1;
            numericUpDown1.Maximum = 10;
            numericUpDown1.Increment = 1;
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
        private void tabPage1_Click(object sender, EventArgs e)
        {
            Form fr4 = new Form4();
            fr4.Show();
        }

        private void materialButton3_Click(object sender, EventArgs e)
        {
            materialListView2.Items.Clear();
        }

        private void materialButton4_Click(object sender, EventArgs e)
        {
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataReader dr = null;
            string connectionString = "Data Source=DESKTOP-JJFORKS\\SQLEXPRESS;Initial Catalog=Kuruyemiş;Integrated Security=True";

            try
            {
                con = new SqlConnection(connectionString);
                con.Open();

                foreach (var selectedItem in listBox1.SelectedItems)
                {
                    string urunAdi = selectedItem.ToString();

                    string urunadiQuery = "SELECT Fiyat FROM Urunler WHERE Urun_Adi = @urunadi";
                    cmd = new SqlCommand(urunadiQuery, con);
                    cmd.Parameters.AddWithValue("@urunadi", urunAdi);

                    dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        string fiyat = dr["Fiyat"].ToString();

                        ListViewItem item = new ListViewItem(urunAdi); 
                        item.SubItems.Add(fiyat); 

                        decimal kgQuantity = numericUpDown1.Value;
                        item.SubItems.Add(kgQuantity.ToString("0.##")); 

                        decimal urunTutari = decimal.Parse(fiyat) * kgQuantity;
                        item.SubItems.Add(urunTutari.ToString("C"));

                        materialListView2.Items.Add(item);
                    }
                    else
                    {
                        MessageBox.Show("Ürün bulunamadı.");
                    }

                    dr.Close();
                    cmd.Dispose();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        public void UrunEkle(string urunAdi, decimal kgMiktarı, decimal fiyat, decimal toplamTutar)
        {
            ListViewItem item = new ListViewItem(urunAdi);
            item.SubItems.Add(fiyat.ToString());
            item.SubItems.Add(kgMiktarı.ToString());
            item.SubItems.Add(toplamTutar.ToString());
            materialListView2.Items.Add(item);
        }

        private void materialButton5_Click(object sender, EventArgs e)
        {
            if (materialListView2.SelectedItems.Count > 0)
            {
                materialListView2.Items.Remove(materialListView2.SelectedItems[0]);
            }
            else
            {
                MessageBox.Show("Lütfen silinecek bir satır seçin.");
            }
        }

        private void materialButton6_Click(object sender, EventArgs e)
        {
            decimal toplamSiparisTutari = 0;

            foreach (ListViewItem item in materialListView2.Items)
            {
                string toplamTutarStr = item.SubItems[3].Text;
                decimal toplamTutar = decimal.Parse(toplamTutarStr, NumberStyles.Currency);

                toplamSiparisTutari += toplamTutar;
            }

            materialLabel3.Text = toplamSiparisTutari.ToString("C");
        }

        private void materialComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            if (materialComboBox1.SelectedIndex == 0)
            {
                listBox1.Items.Add("Antep Fıstığı");
                listBox1.Items.Add("Siirt Fıstığı");
                listBox1.Items.Add("Yer Fıstığı");
                listBox1.Items.Add("Çekirdek");
                listBox1.Items.Add("Fındık");
                listBox1.Items.Add("Badem");
                listBox1.Items.Add("Ceviz");
                listBox1.Items.Add("Kabak Çekirdeği");

            }
            else if (materialComboBox1.SelectedIndex == 1)
            {
                listBox1.Items.Add("Soslu Mısır");
                listBox1.Items.Add("Leblebi");
                listBox1.Items.Add("Badem İçi");
                listBox1.Items.Add("Fındık");
                listBox1.Items.Add("Yer Fıstığı");
                listBox1.Items.Add("Kabuksuz Kaju");
                listBox1.Items.Add("Ceviz");
                listBox1.Items.Add("Antep Fıstığı");
            }
            else if (materialComboBox1.SelectedIndex == 2)
            {
                listBox1.Items.Add("Çiğ Badem");
                listBox1.Items.Add("Çiğ Kaju");
                listBox1.Items.Add("Çiğ Fındık");
                listBox1.Items.Add("Çiğ Çekirdek");
                listBox1.Items.Add("Çiğ Kabak İçi Çekirdeği");
            }
            else if (materialComboBox1.SelectedIndex == 3)
            {
                listBox1.Items.Add("Fıstıklı Baklava");
                listBox1.Items.Add("Sütlü Nuriye");
                listBox1.Items.Add("Şekerpare");
                listBox1.Items.Add("Tulumba Tatlısı");
                listBox1.Items.Add("Kadayıf");
                listBox1.Items.Add("Şöbiyet");
                listBox1.Items.Add("Halka Tatlısı");
            }
            else if (materialComboBox1.SelectedIndex == 4)
            {
                listBox1.Items.Add("Soğuk Baklava");
                listBox1.Items.Add("Profiterol");
                listBox1.Items.Add("Sütlaç");
                listBox1.Items.Add("Ekler");
                listBox1.Items.Add("Trileçe");
                listBox1.Items.Add("Tavuk Göğsü");
                listBox1.Items.Add("Muhallebi");
                listBox1.Items.Add("Kazandibi");
                listBox1.Items.Add("Ekmek Kadayıfı");
            }
            else if (materialComboBox1.SelectedIndex == 5)
            {
                listBox1.Items.Add("Çikolatalı Pasta");
                listBox1.Items.Add("Meyveli Pasta");
                listBox1.Items.Add("Kuru Pasta");
                listBox1.Items.Add("Yaş Pasta");
            }
            else if (materialComboBox1.SelectedIndex == 6)
            {
                listBox1.Items.Add("Sade Lokum");
                listBox1.Items.Add("Fıstıklı Lokum");
                listBox1.Items.Add("Güllü Lokum");
                listBox1.Items.Add("Cevizli Lokum");
                listBox1.Items.Add("Fındıklı Lokum");
                listBox1.Items.Add("Çikolatalı Draje");
                listBox1.Items.Add("Fındıklı Draje");
                listBox1.Items.Add("Portakallı Draje");
                listBox1.Items.Add("Fıstıklı Draje");
                listBox1.Items.Add("Üzümlü Draje");
                listBox1.Items.Add("Renkli Badem Draje");
            }
            else if (materialComboBox1.SelectedIndex == 7)
            {
                listBox1.Items.Add("Hurma");
                listBox1.Items.Add("Üzüm");
                listBox1.Items.Add("Kayısı");
                listBox1.Items.Add("İncir");
                listBox1.Items.Add("Dut Kurusu");
                listBox1.Items.Add("İğde");
            }
            else if (materialComboBox1.SelectedIndex == 8)
            {
                listBox1.Items.Add("Türk Kahvesi");
                listBox1.Items.Add("Kahve Çekirdeği");
                listBox1.Items.Add("Granül Kahve");
                listBox1.Items.Add("Filtre Kahve");
            }
        }
        private void materialButton1_Click(object sender, EventArgs e)
        {
            SqlConnection con;
            SqlCommand cmd;
            SqlDataReader dr;
            string connectionString = "Data Source=DESKTOP-JJFORKS\\SQLEXPRESS;Initial Catalog=Kuruyemiş;Integrated Security=True";
            con = new SqlConnection(connectionString);

            try
            {
                con.Open();
                string siparisKodu = materialTextBox1.Text;

                string siparisSorgusu = "SELECT Siparis_ID FROM Siparisler WHERE Siparis_ID = @siparisKodu";
                cmd = new SqlCommand(siparisSorgusu, con);
                cmd.Parameters.AddWithValue("@siparisKodu", siparisKodu);
                int siparisID = 0;

                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    siparisID = Convert.ToInt32(dr["Siparis_ID"]);
                }
                dr.Close();

                string kargoSorgusu = "SELECT Kargoya_Verilme_Tarihi, Kargo_Teslim_Tarihi, Durum FROM Kargo WHERE Siparis_Kodu = @siparisID";
                cmd = new SqlCommand(kargoSorgusu, con);
                cmd.Parameters.AddWithValue("@siparisID", siparisID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                materialListView1.Items.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    ListViewItem item = new ListViewItem(row["Kargoya_Verilme_Tarihi"].ToString());
                    item.SubItems.Add(row["Kargo_Teslim_Tarihi"].ToString());
                    item.SubItems.Add(row["Durum"].ToString());
                    materialListView1.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        private void materialButton2_Click(object sender, EventArgs e)
        {
            panel1.Visible = true;
        }
        private void materialButton7_Click(object sender, EventArgs e)
        {
            string Telefon = materialTextBox2.Text;
            string Ulke = materialTextBox3.Text;
            string Sehir = materialTextBox4.Text;
            string Ilce = materialTextBox5.Text;
            string PostaKodu = materialTextBox6.Text;
            string Acik_Adres = materialTextBox7.Text;

            string connectionString = "Data Source=DESKTOP-JJFORKS\\SQLEXPRESS;Initial Catalog=Kuruyemiş;Integrated Security=True";

            string insertQuery = "INSERT INTO Adres_Bilgileri (Telefon, Ulke, Sehir, Ilce, PostaKodu, Acik_Adres) VALUES (@Telefon, @Ulke, @Sehir, @Ilce, @PostaKodu, @Acik_Adres)";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    using (SqlCommand cmd = new SqlCommand(insertQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@Telefon", Telefon);
                        cmd.Parameters.AddWithValue("@Ulke", Ulke);
                        cmd.Parameters.AddWithValue("@Sehir", Sehir);
                        cmd.Parameters.AddWithValue("@Ilce", Ilce);
                        cmd.Parameters.AddWithValue("@PostaKodu", PostaKodu);
                        cmd.Parameters.AddWithValue("@Acik_Adres", Acik_Adres);


                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Kayıt Başarıyla Oluşturuldu.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Kayıt Oluşturulurken Bir Hata Oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Hata: " + ex.Message);
                }
                panel1.Visible = false;
            }
        }

        private void materialButton8_Click(object sender, EventArgs e)
        {
            Form fr1 = new Form1();
            fr1.Show();
            this.Hide();
        }
    }
}

        