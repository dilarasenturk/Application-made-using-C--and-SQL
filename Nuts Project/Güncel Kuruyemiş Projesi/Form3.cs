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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Güncel_Kuruyemiş_Projesi
{
    public partial class Form3 : MaterialSkin.Controls.MaterialForm
    {
        public Form3()
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
                string musteriIDValue = materialTextBox1.Text;

                // Musteri_Bilgileri tablosundan veri çekme sorgusu
                string musteriIDQuery = "SELECT Musteri_ID FROM Musteri_Bilgileri WHERE Musteri_ID = @m_ID";
                cmd = new SqlCommand(musteriIDQuery, con);
                cmd.Parameters.AddWithValue("@m_ID", musteriIDValue);
                int musteriID = 0; // Varsayılan olarak musteriID sıfır olarak ayarlanmıştır.

                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    musteriID = Convert.ToInt32(dr["Musteri_ID"]);
                }
                dr.Close();

                string kargoSorgusu = "SELECT Ad, Soyad, Email FROM Musteri_Bilgileri WHERE Musteri_ID = @m_ID";
                cmd = new SqlCommand(kargoSorgusu, con);
                cmd.Parameters.AddWithValue("@m_ID", musteriID);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                materialListView1.Items.Clear();
                foreach (DataRow row in dt.Rows)
                {
                    // Assuming you have columns named "Ad", "Soyad", and "Email" in your DataTable
                    ListViewItem item = new ListViewItem(row["Ad"].ToString());
                    item.SubItems.Add(row["Soyad"].ToString());
                    item.SubItems.Add(row["Email"].ToString());
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
            SqlConnection con;
            SqlCommand cmd;
            SqlDataReader dr;
            string connectionString = "Data Source=DESKTOP-JJFORKS\\SQLEXPRESS;Initial Catalog=Kuruyemiş;Integrated Security=True";
            con = new SqlConnection(connectionString);

            try
            {
                con.Open();
                string siparisIDValue = materialTextBox2.Text;
                int siparisID;

                if (int.TryParse(siparisIDValue, out siparisID))
                {
                    // Siparisler tablosundan veri çekme sorgusu
                    string siparisIDQuery = "SELECT Siparis_ID FROM Siparisler WHERE Siparis_ID = @s_ID";
                    cmd = new SqlCommand(siparisIDQuery, con);
                    cmd.Parameters.AddWithValue("@s_ID", siparisID);

                    dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        dr.Close();

                        // Siparis_Detaylari tablosundan veri çekme sorgusu
                        string sorgu = "SELECT Urun_Kodu, Birim_Fiyati, MiktarKG FROM SiparisDetaylari WHERE Siparis_ID = @s_ID";
                        cmd = new SqlCommand(sorgu, con);
                        cmd.Parameters.AddWithValue("@s_ID", siparisID);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        materialListView2.Items.Clear();
                        foreach (DataRow row in dt.Rows)
                        {
                            // Assuming you have columns named "Urun_Kodu", "Birim_Fiyati", and "MiktarKG" in your DataTable
                            ListViewItem item = new ListViewItem(row["Urun_Kodu"].ToString());
                            item.SubItems.Add(row["Birim_Fiyati"].ToString());
                            item.SubItems.Add(row["MiktarKG"].ToString());
                            materialListView2.Items.Add(item);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Sipariş ID bulunamadı.");
                    }
                }
                else
                {
                    MessageBox.Show("Geçerli bir sipariş ID giriniz.");
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

        private void materialButton3_Click(object sender, EventArgs e)
        {
            SqlConnection con;
            SqlCommand cmd;
            SqlDataReader dr;
            string connectionString = "Data Source=DESKTOP-JJFORKS\\SQLEXPRESS;Initial Catalog=Kuruyemiş;Integrated Security=True";
            con = new SqlConnection(connectionString);

            try
            {
                con.Open();
                string urunIDValue = materialTextBox3.Text;
                int urunID;

                if (int.TryParse(urunIDValue, out urunID))
                {
                    // Siparisler tablosundan veri çekme sorgusu
                    string siparisIDQuery = "SELECT Urun_Kodu FROM Urunler WHERE Urun_Kodu = @u_ID";
                    cmd = new SqlCommand(siparisIDQuery, con);
                    cmd.Parameters.AddWithValue("@u_ID", urunID);

                    dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        dr.Close();

                        // Siparis_Detaylari tablosundan veri çekme sorgusu
                        string sorgu = "SELECT Sube_ID, Miktar, Tarih, Islem_Turu FROM Stoklar WHERE Urun_Kodu = @u_ID";
                        cmd = new SqlCommand(sorgu, con);
                        cmd.Parameters.AddWithValue("@u_ID", urunID);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        da.Fill(dt);

                        materialListView3.Items.Clear();
                        foreach (DataRow row in dt.Rows)
                        {
                            // Assuming you have columns named "Urun_Kodu", "Birim_Fiyati", and "MiktarKG" in your DataTable
                            ListViewItem item = new ListViewItem(row["Sube_ID"].ToString());
                            item.SubItems.Add(row["Miktar"].ToString());
                            item.SubItems.Add(row["Tarih"].ToString());
                            item.SubItems.Add(row["Islem_Turu"].ToString());
                            materialListView3.Items.Add(item);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ürün ID bulunamadı.");
                    }
                }
                else
                {
                    MessageBox.Show("Geçerli bir Ürün ID giriniz.");
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
    }
}


