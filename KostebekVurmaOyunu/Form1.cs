using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KostebekVurmaOyunu
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
        }
        Random rnd = new Random();
        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = !timer1.Enabled;
        }
        List<int> indeks = new List<int>() //Gereksiz beklemeleri önlemek için..
        {
           0,1,2,3,4,5,6,7,8,9,10,11,12,13,14
        };
        private async void timer1_Tick(object sender, EventArgs e)
        {
            int tutulan_index =indeks[rnd.Next(0, indeks.Count - 1)];
            var tutulan = Controls[tutulan_index];
            if (tutulan is PictureBox)
            {
                if ((string)tutulan.Tag != "X") //Köstebeğin vurulup vurulmadığını kontrol etmek için..
                {
                    ((PictureBox)tutulan).ImageLocation = Environment.CurrentDirectory + "\\Res\\kostebek.png";
                    await Task.Delay(750);
                    if (((PictureBox)tutulan).ImageLocation != Environment.CurrentDirectory + "\\Res\\x.png")
                    {
                        ((PictureBox)tutulan).ImageLocation = null;
                    }
                }
            }
        }
        int puan = 0;
        private void pictureBox9_MouseClick(object sender, MouseEventArgs e)
        {
            /*
            Event ismi pictureBox9 olsa bile designer ekranından hepsine ekledim. 
            İlk başta tek olarak test ettiğim için böyle isim aldı.
            */
            if (((PictureBox)sender).ImageLocation != Environment.CurrentDirectory + "\\Res\\x.png" &&
              ((PictureBox)sender).ImageLocation == Environment.CurrentDirectory + "\\Res\\kostebek.png")
            {
                ((PictureBox)sender).Tag = "X";
                indeks.Remove(Controls.IndexOf(((PictureBox)sender)));
                ((PictureBox)sender).ImageLocation = Environment.CurrentDirectory + "\\Res\\x.png";
                label1.Text = string.Format("Puan: {0}", (puan += 5).ToString());
                if (puan == 60) {
                    timer1.Enabled = false;
                 MessageBox.Show("TEBRİKLER KAZANDINIZ!", "OYUN SONU", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }
        }

        private void pictureBox9_MouseMove(object sender, MouseEventArgs e)
        {
            if (((PictureBox)sender).ImageLocation == Environment.CurrentDirectory + "\\Res\\kostebek.png")
            {
                Bitmap bmp = new Bitmap(Image.FromFile(Environment.CurrentDirectory + "\\Res\\hammer_icon.ico"),
                    new Size(48, 48));
                bmp.MakeTransparent();
                ((PictureBox)sender).Cursor = new Cursor(bmp.GetHicon());
            }
            else
            {
                ((PictureBox)sender).Cursor = Cursors.Default;
            }
        }
    }
}
