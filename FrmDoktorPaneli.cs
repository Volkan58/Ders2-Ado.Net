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

namespace Proje_Hastane
{
    public partial class FrmDoktorPaneli : Form
    {
        public FrmDoktorPaneli()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl=new sqlbaglantisi();
        private void FrmDoktorPaneli_Load(object sender, EventArgs e)
        {
           
            DataTable dt=new DataTable();
            SqlDataAdapter da=new SqlDataAdapter("Select * from Tbl_Doktorlar",bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource=dt;
            //Branşlar

            SqlCommand komut2 = new SqlCommand("Select DoktorBrans from Tbl_Doktorlar ", bgl.baglanti());
            SqlDataReader dr=komut2.ExecuteReader();
            while (dr.Read())
            {
                CmbBrans.Items.Add(dr[0].ToString());
            }bgl.baglanti().Close();



        }

        private void BtnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into Tbl_Doktorlar (DoktorAd,DoktorSoyad,DoktorBrans,DoktorTc,DoktorSifre) values (@p1,@p2,@p3,@p4,@p5)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3",CmbBrans.Text);
            komut.Parameters.AddWithValue("@p4",MskTC.Text);
            komut.Parameters.AddWithValue("@p5", TxtSifre.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Doktor Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }


        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Önemli Satır İçeriği Data Gridde Cells(Tek TIklanan) Verileri Aktar
            int secilen=dataGridView1.SelectedCells[0].RowIndex;
            TxtAd.Text=dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            TxtSoyad.Text=dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            CmbBrans.Text=dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            MskTC.Text=dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            TxtSifre.Text=dataGridView1.Rows[secilen].Cells[5].Value.ToString();



        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            //Sil Butonu Sql Komut Satırları
            SqlCommand komut3=new SqlCommand("Delete from Tbl_Doktorlar where DoktorTc=@p1",bgl.baglanti());
            komut3.Parameters.AddWithValue("@p1", MskTC.Text);
            komut3.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kayıt Silindi", "Uyarı",MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("update Tbl_Doktorlar set DoktorAd=@d1,DoktorSoyad=@d2,DoktorBrans=@d3,DoktorSifre=@d4 where DoktorTc=@d5", bgl.baglanti());

            komut.Parameters.AddWithValue("@d1", TxtAd.Text);
            komut.Parameters.AddWithValue("@d2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@d3", CmbBrans.Text);
            komut.Parameters.AddWithValue("@d4", TxtSifre.Text);
            komut.Parameters.AddWithValue("@d5",MskTC.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kayıt Güncellendi", "Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);

        }
    }
}
