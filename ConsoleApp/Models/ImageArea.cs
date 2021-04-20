using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Models
{
    [Table(Name = "Область изображение")]
    public class ImageArea : AccessModelProxy
    {
        [Column(Name = "Id области", IsPrimaryKey = true, CanBeNull = false)]
        public int Id;
        [Column(Name = "Имя файла")]
        public string FileName;
        [Column(Name = "X1")]
        public int X1;
        [Column(Name = "Y1")]
        public int Y1;
        [Column(Name = "X2")]
        public int X2;
        [Column(Name = "Y2")]
        public int Y2;
        [Column(Name = "Наличие дефекта", UpdateCheck = UpdateCheck.Always)]
        public bool IsDefect = false;


        public int h { get { return X2 - X1; } }
        public int w { get { return Y2 - Y1; } }

        public void Print()
        {
            Console.WriteLine($"{FileName} {X1} {Y1} : {X2} {Y2}");
        }
        public bool Contains(int x, int y)
        {
            return x >= X1 && y >= Y1 && x <= X2 && y <= Y2;
        }

    }
}
