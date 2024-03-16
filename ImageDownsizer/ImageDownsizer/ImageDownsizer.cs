using System.Drawing.Imaging;

namespace ImageDownsizer
{
    public partial class ImageDownsizer : Form
    {
        private Bitmap originalBitmap;
        private Bitmap downscaledBitmap;

        public ImageDownsizer() { 
            InitializeComponent();
            parallelCheck.Enabled = false;
        }


        private void openImageButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    originalBitmap = new Bitmap(openFileDialog.FileName);
                    originalPictureBox.Image = originalBitmap;
                   
                }
            }
        }
        private Bitmap DownscaleImage(Bitmap original, double scale)
        {
            int newWidth = (int)(original.Width * scale);
            int newHeight = (int)(original.Height * scale);
            return new Bitmap(original, new Size(newWidth, newHeight));
        }
        private async void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (originalPictureBox.Image != null && double.TryParse(downscaleF.Text, out double scaleFactor) && scaleFactor > 0 && scaleFactor <= 100)
            {
                scaleFactor /= 100.0; 
                int newWidth = (int)(originalPictureBox.Image.Width * scaleFactor);
                int newHeight = (int)(originalPictureBox.Image.Height * scaleFactor);
                if (scaleFactor > 0)
                {
                    parallelCheck.Enabled = true;
                }
                if (parallelCheck.Checked)
                {
                    downscaledBitmap = await DownscaleImageParallelAsync((Bitmap)originalPictureBox.Image, newWidth, newHeight);
                }
                else
                {
                    downscaledBitmap = DownscaleImage((Bitmap)originalPictureBox.Image, scaleFactor);
                }
                downscaledPictureBox.Image = downscaledBitmap;
            }
            else
            {
                MessageBox.Show("Please enter a valid downscaling percentage between 1 and 100.");
            }
        }
        private Task<Bitmap> DownscaleImageParallelAsync(Bitmap originalImage, int newWidth, int newHeight)
        {
            return Task.Run(() =>
            {
                Color[][] originalColorMatrix = ImageConverter.BitmapToPixelGrid(originalImage);
                Color[][] downscaledColorMatrix = Resizer.TransformImage(originalColorMatrix, (double)newWidth / originalImage.Width);
                return ImageConverter.PixelGridToBitmap(downscaledColorMatrix);
            });
        }
        private void saveImageButton_Click(object sender, EventArgs e)
        {
            if (downscaledBitmap != null)
            {
                using (SaveFileDialog saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "JPEG Image|*.jpeg";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        downscaledBitmap.Save(saveFileDialog.FileName, ImageFormat.Jpeg);
                        MessageBox.Show("Image saved successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        private async void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (originalPictureBox.Image != null && double.TryParse(downscaleF.Text, out double scaleFactor) && scaleFactor > 0 && scaleFactor <= 100)
            {
                scaleFactor /= 100.0;
                int newWidth = (int)(originalPictureBox.Image.Width * scaleFactor);
                int newHeight = (int)(originalPictureBox.Image.Height * scaleFactor);
                if (parallelCheck.Checked)
                {
                    downscaledBitmap = await DownscaleImageParallelAsync((Bitmap)originalPictureBox.Image, newWidth, newHeight);
                }
                else
                {
                    downscaledBitmap = DownscaleImage((Bitmap)originalPictureBox.Image, scaleFactor);
                }
                downscaledPictureBox.Image = downscaledBitmap;
            }
            else
            {
                MessageBox.Show("Please enter a valid downscaling percentage between 1 and 100.");
            }
        }
    }
}

