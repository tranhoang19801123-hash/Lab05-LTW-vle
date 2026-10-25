using System.IO;
namespace PictureSlideShow
{
    public partial class Form1 : Form
    {
        private string[]? folderFile = null;
        private int selected = 0;
        private int begin = 0;
        private int end = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
        private void showImage(string path)
        {
            if (pictureBox1.Image != null)
            {
                pictureBox1.Image.Dispose();
            }

            using (Image imgtemp = Image.FromFile(path))
            {
                pictureBox1.Image = new Bitmap(imgtemp);
            }

            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        private void btnOpen_Click_1(object sender, EventArgs e)
        {
            using (FolderBrowserDialog folderBrowserDialog1 =
           new FolderBrowserDialog())
            {
                if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
                {
                    string[] files = Directory.GetFiles(
                             folderBrowserDialog1.SelectedPath);

                    folderFile = Array.FindAll(files, file =>
                    {
                        string ext = Path.GetExtension(file).ToLower();

                        return ext == ".jpg" ||
                               ext == ".jpeg" ||
                               ext == ".bmp" ||
                               ext == ".png";
                    });

                    selected = 0;
                    begin = 0;
                    end = folderFile.Length - 1;

                    if (folderFile.Length > 0)
                    {
                        showImage(folderFile[selected]);
                        btnPrevious.Enabled = true;
                        btnNext.Enabled = true;
                        btnStart.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show("Thu muc khong co hinh anh hop le.");
                        timer1.Stop();
                    }
                }
            }
        }
        private void prevImage()
        {
            if (folderFile == null || folderFile.Length == 0)
                return;

            if (selected == begin)
                selected = end;
            else
                selected--;

            showImage(folderFile[selected]);
        }
        private void nextImage()
        {
            if (folderFile == null || folderFile.Length == 0)
                return;

            if (selected == end)
                selected = begin;
            else
                selected++;

            showImage(folderFile[selected]);
        }
        private void btnPrevious_Click_1(object sender, EventArgs e)
        {
            prevImage();
        }

        private void btnNext_Click_1(object sender, EventArgs e)
        {
            nextImage();
        }

        private void btnStart_Click_1(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            nextImage();
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (folderFile == null || folderFile.Length == 0)
            {
                MessageBox.Show("Vui long mo thu muc hinh truoc.");
                return;
            }

            if (timer1.Enabled)
            {
                timer1.Stop();
                btnStart.Text = "Bat dau";
            }
            else
            {
                timer1.Start();
                btnStart.Text = "Ket thuc Slide Show";
            }
        }
    }
}
