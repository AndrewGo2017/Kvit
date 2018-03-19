using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sber_Kvit.BLL.BaseFile
{
    public class KindergardenDataSet : DataSet
    {
        public string Fio { get; set; }
        public string FioChild { get; set; }
        public string Nazn { get; set; }
        public string Kbk { get; set; }
        public string Oktmo { get; set; }
        public string Period { get; set; }
        public string ClassNum { get; set; }
        public string Group { get; set; }
    }
}
