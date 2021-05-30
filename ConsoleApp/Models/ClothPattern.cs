using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Models
{
    [Table(Name = "Образец ткани")]
    public class ClothPattern
    {
        [Column(Name = "Id образца", IsPrimaryKey = true, CanBeNull = false)]
        public int Id;
        [Column(Name = "Длина")]
        public int Height;
        [Column(Name = "Ширина")]
        public int Width;
        [Column(Name = "Наименование")]
        public string Name;
        [Column(Name = "Адрес папки")]
        public string Root;
    }
}
