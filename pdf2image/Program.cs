using System;
using System.Collections.Generic;
using System.IO;

namespace pdf2image
{
    class Program
    {
        static void Main(string[] args)
        {
            Byte[] getpdf = File.ReadAllBytes(Path.Combine(System.IO.Directory.GetParent(@"./").Parent.Parent.Parent.ToString(),"test.pdf"));
            string path = Path.Combine(System.IO.Directory.GetParent(@"./").Parent.Parent.Parent.ToString(), "test.pdf");
            var outputs = Converter.PdfToPng(path, "__output__");
            foreach (var item in outputs)
            {
                Console.WriteLine(item);
            }

        }
    }
}
