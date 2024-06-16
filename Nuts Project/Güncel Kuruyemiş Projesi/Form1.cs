using MaterialSkin;
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using MaterialSkin.Controls;
using System.ComponentModel;

namespace Güncel_Kuruyemiş_Projesi
{
    public partial class Form1 : MaterialSkin.Controls.MaterialForm
    {
        public Form1()
        {
            InitializeComponent();
            InitializeMaterialSkin();
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MinimumSize = new Size(1220, 650);
            this.MaximumSize = new Size(1220, 650);
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
        private void materialButton1_Click_1(object sender, EventArgs e)
        {
            if (materialCheckbox1.Checked)
            {
                using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-JJFORKS\\SQLEXPRESS;Initial Catalog=Kuruyemiş;Integrated Security=True"))
                {
                    string sorgu = "SELECT * FROM Musteri_Bilgileri WHERE KullaniciAdi = @usr AND Sifre = @pwd";
                    using (SqlCommand cmd = new SqlCommand(sorgu, con))
                    {
                        cmd.Parameters.AddWithValue("@usr", materialTextBox1.Text);
                        cmd.Parameters.AddWithValue("@pwd", materialTextBox2.Text);

                        con.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                MessageBox.Show("Tebrikler! Başarılı bir şekilde giriş yaptınız.", "Giriş Yapıldı!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                Form fr2 = new Form2();
                                fr2.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Kullanıcı adını ve şifrenizi kontrol ediniz.", "Giriş Yapılamadı!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
            else if (materialCheckbox2.Checked)
            {
                using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-JJFORKS\\SQLEXPRESS;Initial Catalog=Kuruyemiş;Integrated Security=True"))
                {
                    string sorgu = "SELECT * FROM Personel_Bilgileri WHERE KullaniciAdi = @usr AND Sifre = @pwd";
                    using (SqlCommand cmd = new SqlCommand(sorgu, con))
                    {
                        cmd.Parameters.AddWithValue("@usr", materialTextBox1.Text);
                        cmd.Parameters.AddWithValue("@pwd", materialTextBox2.Text);

                        con.Open();
                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                MessageBox.Show("Tebrikler! Başarılı bir şekilde giriş yaptınız.", "Giriş Yapıldı!", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                                Form fr3 = new Form3();
                                fr3.Show();
                                this.Hide();
                            }
                            else
                            {
                                MessageBox.Show("Kullanıcı adınızı ve şifrenizi kontrol ediniz.", "Giriş Yapılamadı!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
        }
        private void materialButton2_Click(object sender, EventArgs e)
        {
            string Ad = materialTextBox3.Text;
            string Soyad = materialTextBox4.Text;
            string KullanıcıAdi = materialTextBox5.Text;
            string Sifre = materialTextBox6.Text;
            string Email = materialTextBox7.Text;

            if (string.IsNullOrWhiteSpace(Email) || string.IsNullOrWhiteSpace(KullanıcıAdi) || string.IsNullOrWhiteSpace(Sifre))
            {
                MessageBox.Show("Lütfen tüm alanları doldurun.", "Eksik Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            using (SqlConnection con = new SqlConnection("Data Source=DESKTOP-JJFORKS\\SQLEXPRESS;Initial Catalog=Kuruyemiş;Integrated Security=True"))
            {
                con.Open();

                string checkQuery = "SELECT * FROM Musteri_Bilgileri WHERE KullaniciAdi = @kullaniciAdi OR Email = @mail;";

                using (SqlCommand checkCmd = new SqlCommand(checkQuery, con))
                {
                    checkCmd.Parameters.AddWithValue("@kullaniciAdi", KullanıcıAdi);
                    checkCmd.Parameters.AddWithValue("@mail", Email);

                    int count = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (count > 0)
                    {
                        MessageBox.Show("Bu E-posta veya kullanıcı adı zaten kullanılıyor.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                string insertQuery = "INSERT INTO Musteri_Bilgileri (Ad, Soyad, KullaniciAdi, Sifre, Email) VALUES (@ad, @soyad, @kullaniciadi, @sifre, @mail)";

                using (SqlCommand cmd = new SqlCommand(insertQuery, con))
                {
                    cmd.Parameters.AddWithValue("@ad", Ad);
                    cmd.Parameters.AddWithValue("@soyad", Soyad);
                    cmd.Parameters.AddWithValue("@kullaniciadi", KullanıcıAdi);
                    cmd.Parameters.AddWithValue("@sifre", Sifre);
                    cmd.Parameters.AddWithValue("@mail", Email);

                    int affectedRows = cmd.ExecuteNonQuery();

                    if (affectedRows > 0)
                    {
                        MessageBox.Show("Kayıt Olundu, Giriş Yapabilirsiniz.", "Kayıt Olundu!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Kayıt sırasında bir hata oluştu.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}

