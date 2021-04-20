using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OpenCvSharp;
using System.Data;
using System.Data.Linq;
using ConsoleApp.Models;
using ConsoleApp.Utils;

namespace ConsoleApp
{
    public class Program
    {
        public static string ProjectRoot = Directory.GetParent("./../..").FullName;
        public static string ImagesRoot = $"{ProjectRoot}\\Defect_images";

        static void Main(string[] args)
        {
            //DataSetDBPreparing.SaveImgsFromFolderToDB(ImagesRoot);
            //DataSetDBPreparing.SaveAreasFromImages();
            Console.WriteLine(DBConnector.GetList<ImageArea>().First().GenerateUpdateSql());
            Console.WriteLine("all");
            Console.ReadKey();
            return;

            //ImageFile file = new ImageFile() { FileName = "ls", Height = 23, Width = "sdsd", PatternId = 1 };
            ////file.Create(DBConnector.GetConnection());

            //foreach (var i in DBConnector.GetImageFiles())
            //    Console.WriteLine(i.FileName);
            //Console.WriteLine("sdc");
            //Console.ReadKey();


            //return;
            using (var conn = DBConnector.GetConnection())
            {
                DataContext db = new DataContext(conn);

                ImageFile file = new ImageFile() { FileName = "ls", Height = 23, Width = "sdsd", PatternId = 1 };
                //db.GetTable<ImageFile>().InsertOnSubmit(file);
                //db.SubmitChanges();

                foreach (var item in db.GetTable<ImageFile>()) {
                    Console.WriteLine(item.FileName);
                    //db.Refresh(System.Data.Linq.RefreshMode.KeepChanges, item);
                    item.Height++;
                    db.Refresh(System.Data.Linq.RefreshMode.KeepChanges, item);
                }
                db.SubmitChanges();
                //Console.WriteLine(file.FileName);
                //var files = db.GetTable<ImageFile>();
                //foreach (var file in files)
                //{
                //    file.Height += 1;
                //    Console.WriteLine(file.FileName);
                //}

                //file.Height = 12;
                //db.SubmitChanges();
                //Console.WriteLine(file);
            }
            Console.ReadKey();

            //for (int i=0; i < 25; i++)
            //    for (int j=0; j <= 4096; j+=128)
            //        for (int k = 0; k <= 256; j += 128)


            return;
            string file_name = DBConnector.GetList<ImageFile>().First().FileName;
            List<ImageArea> areas = DBConnector.GetList<ImageArea>().Where(area => area.FileName == file_name).ToList();

            var src = new Mat($"{ImagesRoot}\\{file_name}", ImreadModes.Grayscale);
            var indexer = src.GetGenericIndexer<Vec3b>();

            int A = DBConnector.GetParam("A").Min + 1,
                B = DBConnector.GetParam("B").Min, 
                count = DBConnector.GetParam("count").Min;

            Console.WriteLine($"{A} {B} {count}");
            var lbp = new LBPProcessor(A, B, count);

            List<Histogram<uint>> histogramm_list = new List<Histogram<uint>>();
            foreach (ImageArea row in areas)
            {
                //histogramm_list.Add(lbp.CalcHistogramForArea(row.X1, row.Y1, row.h, row.w, indexer));
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
