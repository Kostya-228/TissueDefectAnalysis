using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OpenCvSharp;

namespace ConsoleApp
{
    class Program
    {
        public static string ProjectRoot = Directory.GetParent("./../..").FullName;
        public static string ImagesRoot = $"{ProjectRoot}\\Defect_images";

        static void Main(string[] args)
        {
            DBConnector.Test();
            Console.ReadLine();
            return;
            var files = Directory.GetFiles(ImagesRoot);
            var src = new Mat(files[0], ImreadModes.Grayscale);

            int A = 2, B = 1, count = 6;
            new ImageProcessing().Test(src, A, B, count);
            Console.ReadLine();
        }
    }
}
