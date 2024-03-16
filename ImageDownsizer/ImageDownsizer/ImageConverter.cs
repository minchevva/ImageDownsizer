using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageDownsizer
{
    public static class ImageConverter
    {
        public static Color[][] BitmapToPixelGrid(Bitmap bitmap)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;

            var pixelGrid = new Color[bitmap.Height][];

            BitmapData bitmapData = null;
            try
            {
                bitmapData = bitmap.LockBits(
                    new Rectangle(0, 0, bitmap.Width, bitmap.Height),
                    ImageLockMode.ReadOnly,
                    PixelFormat.Format32bppArgb);

                unsafe
                {
                    for (int row = 0; row < bitmap.Height; row++)
                    {
                        pixelGrid[row] = new Color[bitmap.Width];
                        byte* pixelRow = (byte*)bitmapData.Scan0 + (row * bitmapData.Stride);

                        for (int col = 0; col < bitmap.Width; col++)
                        {
                            int b = pixelRow[col * 4];
                            int g = pixelRow[col * 4 + 1];
                            int r = pixelRow[col * 4 + 2];
                            int a = pixelRow[col * 4 + 3];

                            pixelGrid[row][col] = Color.FromArgb(a, r, g, b);
                        }
                    }
                }
            }
            finally
            {
                if (bitmapData != null)
                {
                    bitmap.UnlockBits(bitmapData);
                }
            }
            return pixelGrid;
        }

        public static Bitmap PixelGridToBitmap(Color[][] grid)
        {
            int gridHeight = grid.Length;
            int gridWidth = grid[0].Length;

            Bitmap newBitmap = new Bitmap(gridWidth, gridHeight, PixelFormat.Format32bppArgb);

            BitmapData newBitmapData = newBitmap.LockBits(
                new Rectangle(0, 0, gridWidth, gridHeight),
                ImageLockMode.WriteOnly,
                PixelFormat.Format32bppArgb);
            unsafe
            {
                for (int row = 0; row < gridHeight; row++)
                {
                    byte* pixelRow = (byte*)newBitmapData.Scan0 + (row * newBitmapData.Stride);

                    for (int col = 0; col < gridWidth; col++)
                    {
                        Color color = grid[row][col];
                        pixelRow[col * 4] = color.B;
                        pixelRow[col * 4 + 1] = color.G;
                        pixelRow[col * 4 + 2] = color.R;
                        pixelRow[col * 4 + 3] = color.A;
                    }
                }
            }

            newBitmap.UnlockBits(newBitmapData);
            return newBitmap;
        }
    }
}

