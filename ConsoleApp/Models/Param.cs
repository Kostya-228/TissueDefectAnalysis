using System.Data.Linq.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace ConsoleApp.Models
{
    [Table(Name = "Параметры")]
    public class Param: AccessModelProxy
    {
        [Column(Name = "Код параметра", IsPrimaryKey = true)]
        public string Code { get; set; }

        [Column(Name = "Наименование")]
        public string Name { get; set; }

        [Column(Name = "Нижняя граница")]
        public int Min { get; set; }

        [Column(Name = "Верхняя граница")]
        public int Max { get; set; }

        [Column(Name = "Шаг")]
        public int Step { get; set; }

        private IEnumerable<int> AsEnumerable()
        {
            int x = Min;
            do
            {
                yield return x;
                x += Step;
                if (Step < 0 && x <= Max || 0 < Step && Max <= x)
                    break;
            }
            while (true);
        }
    }
}
