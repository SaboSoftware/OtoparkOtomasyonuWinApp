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

namespace otopark
{
    public partial class frmAraçOtoparkKaydı : Form
    {
        public frmAraçOtoparkKaydı()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=.;Initial Catalog=araç_otopark;Integrated Security=True");
        private void frmAraçOtoparkKaydı_Load(object sender, EventArgs e)
        {
            BoşAraçlar();

            Marka();
            
            

        }

        private void Marka()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select marka from marka_bilgileri ", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboMarka.Items.Add(read["marka"].ToString());

            }
            baglanti.Close();
        }

        private void BoşAraçlar()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select*from araçdurumu WHERE durumu='BOŞ'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboParkYeri.Items.Add(read["parkyeri"].ToString());

            }
            baglanti.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtTC.Text) || String.IsNullOrEmpty(txtAD.Text) || String.IsNullOrEmpty(txtSOYAD.Text) || String.IsNullOrEmpty(txtTELEFON.Text) || String.IsNullOrEmpty(txtEmail.Text) || String.IsNullOrEmpty(txtPlaka.Text) || String.IsNullOrEmpty(comboMarka.Text) || String.IsNullOrEmpty(comboParkYeri.Text) || String.IsNullOrEmpty(txtRenk.Text))
            {
                MessageBox.Show("Veriler boş bırakılamaz.", "Uyarı");
            }
            else
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into araç_otopark_kaydı(tc,ad,soyad,telefon,eposta,plaka,marka,seri,renk,parkyeri,tarih) values(@tc,@ad,@soyad,@telefon,@eposta,@plaka,@marka,@seri,@renk,@parkyeri,@tarih)", baglanti);
                komut.Parameters.AddWithValue("@tc", txtTC.Text);
                komut.Parameters.AddWithValue("@ad", txtAD.Text);
                komut.Parameters.AddWithValue("@soyad", txtSOYAD.Text);
                komut.Parameters.AddWithValue("@telefon", txtTELEFON.Text);
                komut.Parameters.AddWithValue("@eposta", txtEmail.Text);
                komut.Parameters.AddWithValue("@plaka", txtPlaka.Text);
                komut.Parameters.AddWithValue("@marka", comboMarka.Text);
                komut.Parameters.AddWithValue("@seri", comboSeri.Text);
                komut.Parameters.AddWithValue("@renk", txtRenk.Text);
                komut.Parameters.AddWithValue("@parkyeri", comboParkYeri.Text);
                komut.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());

                komut.ExecuteNonQuery();


                SqlCommand komut2 = new SqlCommand("update araçdurumu set durumu='DOLU' where parkyeri='" + comboParkYeri.SelectedItem + "'", baglanti);
                komut2.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Araç Kaydı Oluşturuldu", "kayit");
                comboParkYeri.Items.Clear();
                BoşAraçlar();
                comboMarka.Items.Clear();
                Marka();
                comboSeri.Items.Clear();
                foreach (Control item in grupKişi.Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }
                }
                foreach (Control item in grupKişi.Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }
                }
                foreach (Control item in grupAraç.Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }
                }
                foreach (Control item in grupAraç.Controls)
                {
                    if (item is ComboBox)
                    {
                        item.Text = "";
                    }
                }
            }
            
            
        }

        private void btnMarka_Click(object sender, EventArgs e)
        {
            frmMarka marka=new frmMarka();
            marka.ShowDialog();
            this.Hide();
        }

        private void btnSeri_Click(object sender, EventArgs e)
        {
            frmSeri seri=new frmSeri();
            seri.ShowDialog();
            this.Hide();
        }

        private void comboMarka_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboSeri.Items.Clear();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select marka,seri from seribilgileri where marka='"+comboMarka.SelectedItem+"' ", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboSeri.Items.Add(read["seri"].ToString());
            
            }
          baglanti.Close();
           
        }
    } 
}
