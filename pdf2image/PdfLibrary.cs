using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using PDFiumSharp;

namespace pdf2image
{
    public static class Converter
    {
        private const double Scale = 4.166666666666667;

        private static byte[] BmpToPng(byte[] bmp)
        {
            using var stream = new MemoryStream(bmp);
            using var bitmap = Bitmap.FromStream(stream);
            using var outStream = new MemoryStream();
            bitmap.Save(outStream, ImageFormat.Png);
            return outStream.ToArray();
        }

        public static string[] PdfToPng(string pdfPath, string targetDirectory)
        {
            if (!Directory.Exists(targetDirectory))
            {
                Directory.CreateDirectory(targetDirectory);
            }

            var outputs = new List<string>();
            var index = 0;

            using var doc = new PdfDocument(pdfPath);

            foreach (var page in doc.Pages)
            {
                var width = (int)(page.Width * Scale);
                var heigh = (int)(page.Height * Scale);
                var outputPath = Path.Combine(targetDirectory, $"{index++}.png");

                using var bitmap = new PDFiumBitmap(width, heigh, true);
                using var stream = new MemoryStream();

                page.Render(bitmap);
                bitmap.Save(stream, 300, 300);

                byte[] png = BmpToPng(stream.ToArray());
                File.WriteAllBytes(outputPath, png);

                outputs.Add(outputPath);
            }
            return outputs.ToArray();
        }
    }
}
