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
    public partial class frmMarka : Form
    {
        public frmMarka()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=.;Initial Catalog=araç_otopark;Integrated Security=True");

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Lütfen Markayı Giriniz");
            }else
            {
                SqlCommand komut = new SqlCommand("insert into marka_bilgileri(marka) values('" + textBox1.Text + "') ", baglanti);
                komut.ExecuteNonQuery();
                MessageBox.Show("Marka Eklendi");
                textBox1.Clear();
                frmAraçOtoparkKaydı otokayit = new frmAraçOtoparkKaydı();
                otokayit.Show();
            }
            this.Close();
            baglanti.Close();
        }
    }
}
