using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Models
{
    [Table(Name = "Эксперименты")]
    public class Experement
    {
        [Column(Name = "Номер эксперимента", IsPrimaryKey = true, CanBeNull = false)]
        public int ExperimentNumber;
        [Column(Name = "Номер опыта", IsPrimaryKey = true, CanBeNull = false)]
        public int TestNubmer;
    }
}
