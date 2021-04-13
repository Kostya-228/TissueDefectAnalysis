using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OpenCvSharp;
using System.Data;
using System.Data.Linq;

namespace ConsoleApp
{
    class Program
    {
        public static string ProjectRoot = Directory.GetParent("./../..").FullName;
        public static string ImagesRoot = $"{ProjectRoot}\\Defect_images";

        static void Main(string[] args)
        {
            string file_name = DBConnector.GetImageFiles().First().FileName;
            List<ImageArea> areas = DBConnector.GetImageAreas(file_name);

            var src = new Mat($"{ImagesRoot}\\{file_name}", ImreadModes.Grayscale);
            var indexer = src.GetGenericIndexer<Vec3b>();

            int A = DBConnector.GetParam("A").Min + 1,
                B = DBConnector.GetParam("B").Min, 
                count = DBConnector.GetParam("count").Min;

            Console.WriteLine($"{A} {B} {count}");
            var lbp = new ImageProcessing(A, B, count);

            List<Histogram<uint>> histogramm_list = new List<Histogram<uint>>();
            foreach (ImageArea row in areas)
            {
                histogramm_list.Add(lbp.CalcHistogramForArea(row.X1, row.Y1, row.h, row.w, indexer));
            }

            foreach (Histogram<uint> histogram in histogramm_list)
            {
                histogram.Print();
                Console.WriteLine(histogram.Distanse(histogramm_list[10]));
            }
            Console.WriteLine($"все");
            Console.ReadLine();
        }
    }
}
