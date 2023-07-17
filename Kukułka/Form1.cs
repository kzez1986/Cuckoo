using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Kukułka
{
    public partial class Form1 : Form
    {
        private int godzina;
        public Form1()
        {
            InitializeComponent();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Hide();
            timer1.Enabled = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void oToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Show();
            timer1.Enabled = true;
        }

        private void zamknijToolStripMenuItem_Click(object sender, EventArgs e)
        {
            notifyIcon1.Visible = false;
            Close();
        }

        private void notifyIcon1_MouseMove(object sender, MouseEventArgs e)
        {
            notifyIcon1.Text = "Godziny .NET (" + DateTime.Now.ToShortTimeString() + ")";
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string s = "Aktualna data: " + DateTime.Today.ToLongDateString();
            string[] DniTygodnia = { "Niedziela", "Poniedziałek", "Wtorek", "Środa", "Czwartek", "Piątek", "Sobota" };
            byte numerDniaTygodnia = (byte)DateTime.Now.DayOfWeek;
            s += "\nDzień tygodnia: " + DniTygodnia[numerDniaTygodnia];
            s += "\nDzień roku: " + DateTime.Now.DayOfYear;
            s += "\nAktualny czas: " + DateTime.Now.ToLongTimeString();
            s += "\n\n(c) Jacek Matulewski 2008";
            notifyIcon1.BalloonTipText = s;
            notifyIcon1.ShowBalloonTip(1000);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DateTime nastepnaPelnaGodzina = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, (DateTime.Now.Hour + 1) % 24, 0, 0, 0);
            int ileMilisekundDoPelnejGodziny = (3600000 - (DateTime.Now.Minute * 60000 + DateTime.Now.Second * 1000 + DateTime.Now.Millisecond));
            //timer2.Interval = 13426; //(int)ileMilisekundDoPelnejGodziny;
            //int sek = (int)ileMilisekundDoPelnejGodziny;
            timer2.Interval = ileMilisekundDoPelnejGodziny;
            timer2.Enabled = true;
            //MessageBox.Show(timer2.Interval.ToString());
        }

        private int licznik;

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Interval = 3600000;
            if (DateTime.Now.Hour == 0) godzina = 12;
            else if (DateTime.Now.Hour < 13) godzina = DateTime.Now.Hour;
            else godzina = DateTime.Now.Hour - 12;
            licznik = 0;
            timer3.Enabled = true;
            notifyIcon1_MouseDoubleClick(null, null);
        }
        
        private void timer3_Tick(object sender, EventArgs e)
        {
            (new System.Media.SoundPlayer("Godziny.wav")).Play();
            licznik++;
            if(licznik == godzina) timer3.Enabled = false;
        }
    }
}
