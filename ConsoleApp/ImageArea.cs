using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    [Table(Name = "Область изображение")]
    class ImageArea
    {
        [Column(Name = "Id области", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column(Name = "Имя файла")]
        public string FileName { get; set; }
        [Column]
        public int X1 { get; set; }
        [Column]
        public int Y1 { get; set; }
        [Column]
        public int X2 { get; set; }
        [Column]
        public int Y2 { get; set; }
        [Column(Name ="Наличие дефекта")]
        public bool IsDefect { get; set; }


        public int h { get { return X2 - X1; } }
        public int w { get { return Y2 - Y1; } }
    }
}
