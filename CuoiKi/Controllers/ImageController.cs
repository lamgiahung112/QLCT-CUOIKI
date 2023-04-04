using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CuoiKi.Controllers
{
    public static class ImageController
    {
        private static readonly string _Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
        public static string? GetFileInputAsBase64()
        {
            var dlg = new Microsoft.Win32.OpenFileDialog() { Filter = _Filter };
            var result = dlg.ShowDialog();
            if (!result.HasValue || result.Value)
            {
                return null;
            }

            byte[] imageBytes = File.ReadAllBytes(dlg.FileName);
            string base64ImageRepresentation = Convert.ToBase64String(imageBytes);
            return base64ImageRepresentation;
        }

        public static BitmapImage ParseBase64ToImage(string base64Content)
        {
            using (var stream = new MemoryStream(Convert.FromBase64String(base64Content)))
            {
                var bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = stream;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                bitmap.Freeze();
                return bitmap;
            }
        }
    }
}
