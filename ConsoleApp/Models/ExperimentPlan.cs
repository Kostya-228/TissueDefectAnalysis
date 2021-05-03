using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Models
{

    [Table(Name = "План эксперимента")]
    public class ExperimentPlan: AccessModelProxy
    {
        [Column(Name = "Номер эксперимента", IsPrimaryKey = true, CanBeNull = false)]
        public int ExperimentNumber;
        [Column(Name = "Номер опыта", IsPrimaryKey = true, CanBeNull = false)]
        public int TestNubmer;
        [Column(Name = "Код параметра", IsPrimaryKey = true, CanBeNull = false)]
        public string CodeParameter;
        [Column(Name = "Значение параметра")]
        public int ValueParameter;
    }
}
