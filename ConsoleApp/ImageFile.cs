using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{

    [Table(Name = "Файлы изображений")]
    class ImageFile
    {
        [Column(Name = "Имя файла", IsPrimaryKey = true)]
        public string FileName { get; set; }
        [Column(Name = "Высота")]
        public int Height { get; set; }
        [Column(Name = "Длина")]
        public string Width { get; set; }
        [Column(Name = "Id образца")]
        public int PatternId { get; set; }
    }
}
