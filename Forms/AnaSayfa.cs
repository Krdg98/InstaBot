﻿using InstaBot.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BotCodes;
using InstaBot.Codes;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using InstaBot.Database;

namespace InstaBot
{
    public partial class AnaSayfa : Form
    {
        public AnaSayfa()
        {
            InitializeComponent();
        }
        VeriTabani VeriTabani = VeriTabani.GetInstance();
        private void AnaSayfa_Load(object sender, EventArgs e)
        {
            string adres = "https://karadagyazilim.com/versiyon.php";
            string gelenHamVeri, apVer, guncellemeApVer;

            WebClient client = new WebClient();
            Stream stream = client.OpenRead(adres);
            StreamReader streamReader = new StreamReader(stream);

            gelenHamVeri = streamReader.ReadToEnd();

            apVer = gelenHamVeri.Split('/')[0].Split('=')[1]; // versiyonları aldık 
            guncellemeApVer = gelenHamVeri.Split('/')[1].Split('=')[1];

            var versiyonlar = VeriTabani.VersiyonlariAl();
            if (guncellemeApVer!= versiyonlar[0]) //Guncelleme uygulamanın versiyonu
            {
                Guncelleme guncelleme = new Guncelleme();
                guncelleme.versiyon = guncellemeApVer;
                guncelleme.Show();
            }

            if (guncellemeApVer != versiyonlar[1])
            {
            }

        }

        [Obsolete]
        private void pcCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            Komutlar komutlar = Komutlar.GetInstance();
            komutlar.Bitir();
        }

        private bool mouseDown; //Formu sürüklemek için
        private Point lastLocation; //Formu sürüklemek için
        private void pnlUst_MouseDown(object sender, MouseEventArgs e)
        {
            mouseDown = true; //Formu sürüklemek için
            lastLocation = e.Location; //Formu sürüklemek için
        }

        private void pnlUst_MouseMove(object sender, MouseEventArgs e)
        {
            if (mouseDown) //Formu sürüklemek için
            {
                this.Location = new Point((this.Location.X - lastLocation.X) + e.X, (this.Location.Y - lastLocation.Y) + e.Y);
                this.Update();
            }
        }

        private void pnlUst_MouseUp(object sender, MouseEventArgs e)
        {
            mouseDown = false; //Formu sürüklemek için
        }

        private Form activeForm = null; //Yeni Açılan formlar
        public void openChildForm(Form childForm)
        {
            if (activeForm != null)
                activeForm.Close();

            activeForm = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            pnlSag.Controls.Add(childForm);
            pnlSag.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }

        private void pcLogo_Click(object sender, EventArgs e)
        {
            openChildForm(new Islemler());
        }

        private void BtnIslem_Click(object sender, EventArgs e)
        {
            openChildForm(new Islemler());
        }

        private void btnList_Click(object sender, EventArgs e)
        {
            openChildForm(new Listeler());
        }

        private void btnGonderi_Click(object sender, EventArgs e)
        {
            openChildForm(new Gonderiler());
        }
    }
}


