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
    public partial class frmSeri : Form
    {
        public frmSeri()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=.;Initial Catalog=araç_otopark;Integrated Security=True");
        private void frmSeri_Load(object sender, EventArgs e)
        {
            Marka();
        }

        private void Marka()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select marka from marka_bilgileri ", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                comboBox1.Items.Add(read["marka"].ToString());

            }
            baglanti.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            if (String.IsNullOrEmpty(textBox1.Text) || String.IsNullOrEmpty(comboBox1.Text))
            {
                MessageBox.Show("Lütfen Markanın Serisini Giriniz");
            }
            else
            {
                SqlCommand komut = new SqlCommand("insert into seribilgileri(marka,seri) values('" + comboBox1.Text + "','" + textBox1.Text + "') ", baglanti);
                komut.ExecuteNonQuery();
                MessageBox.Show("Markaya bağlı araç serisi eklendi");
                textBox1.Clear();
                comboBox1.Text = "";
                comboBox1.Items.Clear();
                frmAraçOtoparkKaydı otokayit = new frmAraçOtoparkKaydı();
                otokayit.Show();
                this.Close();
            }
            baglanti.Close();
            
        }
    }
}
