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
using System.Data.OleDb;

namespace ConsoleApp
{
    public class Program
    {
        public static string ProjectRoot = Directory.GetParent("./../..").FullName;
        public static string ImagesRoot = $"{ProjectRoot}\\Defect_images_2\\1";

        static void Main(string[] args)
        {
            //DBConnector.CreateItem(new ClothPattern() { Id = 1, Height = 256, Width = 4096, Name = "Хлопок_1" });
            //Utils.DataSetDBPreparing.SaveImgsFromFolderToDB(ImagesRoot);
            //Utils.DataSetDBPreparing.SaveAreasFromImages(32);
            //return;

            string file_name = "15.png"; //DBConnector.GetList<ImageFile>().First().FileName;

            List<ImageArea> areas = DBConnector.GetList<ImageArea>().Where(area => area.FileName == file_name).ToList();

            var src = new Mat($"{ImagesRoot}\\{file_name}", ImreadModes.Grayscale);
            var indexer = src.GetGenericIndexer<Vec3b>();

            RunExperimenets(areas, indexer, 3);
            
            //var exp = new Experement() { ExperimentNumber = 1, TestNubmer = 1 };
            //MakeExperemnt(4, 3, 10, areas, exp, indexer);


            Console.WriteLine($"все");
            Console.ReadLine();
        }

        static void RunExperimenets(List<ImageArea> areas, Mat.Indexer<Vec3b> indexer, int exp_num = 1)
        {
            Param A_ = DBConnector.GetParam("Больший радиус"),
                B_ = DBConnector.GetParam("Меньший радиус"),
                count_ = DBConnector.GetParam("Количество точек");

            A_.Max = 10;
            B_.Max = 8;
            count_.Max = 16;

            int test_num = 1;
            foreach (var a in A_.AsEnumerable())
                foreach (var b in B_.AsEnumerable())
                    foreach (var c in count_.AsEnumerable())
                    {
                        Console.WriteLine($"test {test_num}/{A_.count * B_.count * count_.count}");
                        var exp = new Experement() { ExperimentNumber = exp_num, TestNubmer = test_num };
                        DBConnector.CreateItem(exp);
                        DBConnector.CreateList(new List<ExperimentPlan>() {
                            new ExperimentPlan() { ExperimentNumber = exp.ExperimentNumber, TestNubmer = exp.TestNubmer, CodeParameter = A_.Code, ValueParameter = a },
                            new ExperimentPlan() { ExperimentNumber = exp.ExperimentNumber, TestNubmer = exp.TestNubmer, CodeParameter = B_.Code, ValueParameter = b },
                            new ExperimentPlan() { ExperimentNumber = exp.ExperimentNumber, TestNubmer = exp.TestNubmer, CodeParameter = count_.Code, ValueParameter = c },
                        });
                        var results = MakeExperemnt(a, b, c, areas, exp, indexer);
                        DBConnector.CreateList(results);
                        test_num++;
                    }
        }

        public static List<ExperimentResult> MakeExperemnt(int A, int B, int count, List<ImageArea> areas, Experement exp, Mat.Indexer<Vec3b> indexer)
        {
            

            Console.WriteLine($"{A} {B} {count}");
            var lbp = new LBPProcessor(A, B, count);

            List<Tuple<RGBHistogramm<uint>, ImageArea>> histogramm_list = new List<Tuple<RGBHistogramm<uint>, ImageArea>>();
            foreach (ImageArea row in areas)
            {
                histogramm_list.Add(new Tuple<RGBHistogramm<uint>, ImageArea>(lbp.CalcHistogramForArea(row.X1, row.Y1, row.h, row.w, indexer), row));
            }

            List<ExperimentResult> results = new List<ExperimentResult>();

            foreach (var histogram in histogramm_list)
            {
                var hist = histogram.Item1;
                var area = histogram.Item2;
                string defect = area.IsDefect ? "-" : "";
                var distanseR = hist.R.Distanse(histogramm_list[1].Item1.R);
                var distanseG = hist.G.Distanse(histogramm_list[1].Item1.G);
                var distanseB = hist.B.Distanse(histogramm_list[1].Item1.B);

                var summa = distanseR + distanseG + distanseB;
                results.Add(new ExperimentResult() { ExperimentNumber = exp.ExperimentNumber, TestNubmer = exp.TestNubmer, IdArea = area.Id, DefectPower = summa });
                //Console.WriteLine($"{area.X1,3}, {area.Y1,3}: {summa,3} {defect,2} {threshold,2}");
            }
            return results;
        }
    }
}
