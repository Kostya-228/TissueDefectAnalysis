using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ConsoleApp
{
    class Program
    {
        public static string ProjectRoot = Directory.GetParent("./../..").FullName;
        public static string ImagesRoot = $"{ProjectRoot}\\Defect_images";

        static void Main(string[] args)
        {

            //var files = Directory.GetFiles(ImagesRoot);
            //FileStream stream = new FileStream(files[0], FileMode.Open, FileAccess.Read);
            ImageProcessing.Test();
            Console.ReadLine();
        }
    }
}
