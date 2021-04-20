using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Models
{

    [Table(Name = "Файлы изображений")]
    public class ImageFile : AccessModelProxy
    {
        [Column(Name = "Имя файла", IsPrimaryKey = true, CanBeNull = false, UpdateCheck = UpdateCheck.WhenChanged)]
        public string FileName;
        [Column(Name = "Высота", CanBeNull = true, UpdateCheck = UpdateCheck.Never)]
        public int Height;
        [Column(Name = "Длина", CanBeNull = true, UpdateCheck = UpdateCheck.Never)]
        public string Width;
        [Column(Name = "Id образца", CanBeNull = true, UpdateCheck = UpdateCheck.Never)]
        public int PatternId;
    }
}
