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
    public partial class frmSatış : Form
    {
        public frmSatış()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=.;Initial Catalog=araç_otopark;Integrated Security=True");
        DataSet ds = new DataSet();
        private void frmSatış_Load(object sender, EventArgs e)
        {
            SatislariListele();
            Hesapla();
        }

        private void Hesapla()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select sum(tutar) from satis", baglanti);
            label1.Text = "Toplam tutar=" + komut.ExecuteScalar() + "TL";
            baglanti.Close();
        }

        private void SatislariListele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select*from satis", baglanti);
            adtr.Fill(ds, "satis");
            dataGridView1.DataSource = ds.Tables["satis"];
            baglanti.Close();
        }
    }
}
