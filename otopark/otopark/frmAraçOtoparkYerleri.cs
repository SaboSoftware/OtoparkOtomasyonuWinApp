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
namespace otopark
{
    public partial class frmAraçOtoparkYerleri : Form
    {
        public frmAraçOtoparkYerleri()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=.;Initial Catalog=araç_otopark;Integrated Security=True");
        private void frmAraçOtoparkYerleri_Load(object sender, EventArgs e)
        {
            BoşParkYerleri();
            DoluParkYerleri();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from araç_otopark_kaydı", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                foreach (Control item in Controls)
                {
                    if (item is Button)
                    {
                        if (item.Text == read["parkyeri"].ToString())
                        {
                            item.Text = read["plaka"].ToString();
                        }
                    }
                }

            }
            baglanti.Close();
        }

        private void DoluParkYerleri()
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select *from araçdurumu", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                foreach (Control item in Controls)
                {
                    if (item is Button)
                    {
                        if (item.Text == read["parkyeri"].ToString() && read["durumu"].ToString() == "DOLU")
                        {
                            item.BackColor = Color.Red;
                        }
                    }
                }

            }
            baglanti.Close();
        }

        private void BoşParkYerleri()
            
        {
            int sayac = 1;
            foreach (Control item in Controls)
            {
                if (item is Button)
                {
                    item.Text = "P-" +sayac;
                    item.Name = "P-" +sayac;
                    sayac++;
                }
            }
        }
    }
}
