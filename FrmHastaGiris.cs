﻿using System;
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
    public partial class FrmHastaGiris : Form
    {
        public FrmHastaGiris()
        {
            InitializeComponent();
        }

        private void LinkUyeol_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FrmUyeKayit fr=new FrmUyeKayit();
            fr.Show();
            

        }
        sqlbaglantisi bgl=new sqlbaglantisi();
        private void BtnGiris_Click(object sender, EventArgs e)
        {
           SqlCommand komut=new SqlCommand("Select * from Tbl_Hastalar Where HastaTc=@p1 and HastaSifre=@p2",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", MskTc.Text);
            komut.Parameters.AddWithValue("@p2", TxtSifre.Text);
            SqlDataReader dr=komut.ExecuteReader();
            if(dr.Read())
            {
                FrmHastaDetay fr=new FrmHastaDetay();
                fr.tc=MskTc.Text;

                fr.Show();
                this.Hide();
            }else
            {
                MessageBox.Show("Hatalı Tc veya Şifre Gİrdiniz");

            }
            bgl.baglanti().Close();


        }
    }
}
