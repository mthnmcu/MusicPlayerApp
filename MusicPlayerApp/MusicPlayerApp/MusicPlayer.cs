using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusicPlayerApp
{
    public partial class MusicPlayerApp : Form
    {
        public MusicPlayerApp()
        {
            InitializeComponent();
        }

        //Create Global Variables of String Type Array to save the titles or name of the Tracks and path of the track
        String[] paths, files;

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Filter = "Media Files|*.mp3;*.wav;*.aac;*.ogg;*.mp4;*.avi;*.wmv;*.mov"; // Audio ve video dosyalarını göster

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                files = openFileDialog.SafeFileNames; // Dosya adlarını files dizisine kaydet
                paths = openFileDialog.FileNames; // Dosya yollarını paths dizisine kaydet

                // Listbox'ta seçilen müzikleri göster
                foreach (string file in files)
                {
                    listBoxSongs.Items.Add(file);
                }
            }
        }

        private void listBoxSongs_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Write a code to play music 
            axWindowsMediaPlayer1.URL = paths[listBoxSongs.SelectedIndex];
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }
       private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {
            try
            {
                if (listBoxSongs.SelectedItem != null)
                {
                    axWindowsMediaPlayer1.URL = paths[listBoxSongs.SelectedIndex];
                    axWindowsMediaPlayer1.Ctlcontrols.play();
                }
                else
                {
                    MessageBox.Show("Lütfen Bir Şarkı Seçin.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Şarkı çalınırken bir hata oluştu: " + ex.Message);
            }
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            // Formun simge durumuna küçültme
            this.WindowState = FormWindowState.Minimized;
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            // Kullanıcıdan onay al
            DialogResult result = MessageBox.Show("Seçili öğeleri silmek istediğinizden emin misiniz?", "Silme İşlemi", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            // Eğer kullanıcı evet derse
            if (result == DialogResult.Yes)
            {
                // Listbox'ta seçilen tüm öğeleri kaldır
                for (int i = listBoxSongs.SelectedIndices.Count - 1; i >= 0; i--)
                {
                    int selectedIndex = listBoxSongs.SelectedIndices[i];
                    listBoxSongs.Items.RemoveAt(selectedIndex);

                    // paths ve files dizilerinden de seçilen öğeleri kaldır
                    paths = paths.Where((source, index) => index != selectedIndex).ToArray();
                    files = files.Where((source, index) => index != selectedIndex).ToArray();
                }
            }
            else
            {
                // Kullanıcı hayır derse işlemi iptal et
                return;
            }
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //Code to close app
            this.Close();
        }
    }
}
        


