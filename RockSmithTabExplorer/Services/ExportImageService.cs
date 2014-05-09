using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.IO;
using RockSmithTabExplorer.Controls;

namespace RockSmithTabExplorer.Services
{
    public class ExportImageService
    {
       public void SavePNGFromDialog(RockSmithTabExplorer.Controls.TabControl tabControl, string fileName)
       {
            var dialog = new Microsoft.Win32.SaveFileDialog();
            dialog.Filter = "Png image (*.png)|*.png";
            dialog.FileName = fileName;
            if (dialog.ShowDialog() == true)
            {
                ExportToPNG(dialog.FileName, tabControl);
            }
       }

        public void ExportToPNG(string imgpath, Image image)
        {
            Uri path = new Uri(imgpath);

            if (path == null)
                return;

            DoWithElementAtSize(image, new Size(image.Width, image.Height), () =>
            {
                RenderTargetBitmap renderBitmap = new RenderTargetBitmap((int)image.Width, (int)image.Height, 96d, 96d, PixelFormats.Pbgra32);
                renderBitmap.Render(image);

                using (FileStream outStream = new FileStream(path.LocalPath, FileMode.Create))
                {
                    PngBitmapEncoder encoder = new PngBitmapEncoder();
                    encoder.Frames.Add(BitmapFrame.Create(renderBitmap));
                    encoder.Save(outStream);
                }
            });
        }

        private void DoWithElementAtSize(FrameworkElement element, Size size, Action action)
        {
            Transform transform = element.LayoutTransform;
            element.LayoutTransform = null;

            element.Measure(size);
            element.Arrange(new Rect(size));

            action();

            element.LayoutTransform = transform;
        }
    }
}
