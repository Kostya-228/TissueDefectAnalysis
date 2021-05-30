using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConsoleApp;
using ConsoleApp.Models;

namespace ConsoleApp.Utils
{
    public class DataSetDBPreparing
    {

        /// <summary>
        /// Сохранить изображения из папки в базу
        /// </summary>
        /// <param name="ImagesRoot"></param>
        public static void SaveImgsFromFolderToDB(string ImagesRoot, int pattern_id)
        {
            List<ImageFile> files = Directory.GetFiles(ImagesRoot).Select(file_path =>
                new ImageFile()
                {
                    FileName = Path.GetFileName(file_path),
                    Height = 256,
                    Width = 512,
                    PatternId = pattern_id
                }
            ).ToList();
            DBConnector.CreateList(files);
        }

        /// <summary>
        /// Каждое изобржение из базы поделить на зоны
        /// </summary>
        /// <param name="step">шаг зоны</param>
        public static void SaveAreasFromImages(int pattern_id, int step = 128)
        {
            List<ImageArea> areas = new List<ImageArea>();
            int index = DBConnector.GetList<ImageArea>().Max(img => img.Id) + 1;
            foreach (var img_file in DBConnector.GetList<ImageFile>().Where(img => img.PatternId == pattern_id))
            {
                for (int i = 0; i < img_file.Height; i += step)
                    for (int j = 0; j < img_file.Width; j += step)
                    {
                        var area = new ImageArea()
                        {
                            // в аксесе не получилось имполльзовать IsDbGenerated, так как запрос на всатвку с этим атрибутом падал
                            Id = index,
                            FileName = img_file.FileName,
                            X1 = j,
                            Y1 = i,
                            X2 = j + step,
                            Y2 = i + step,
                            IsDefect=false
                        };
                        //area.Print();
                        index++;
                        areas.Add(area);
                    }
            }
            DBConnector.CreateList(areas);
        }
    }
}
