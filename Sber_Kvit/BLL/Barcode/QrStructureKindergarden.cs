using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sber_Kvit.BLL.Barcode
{
    class QrStructureKindergarden : QrStructure
    {
        public readonly string address;
        public readonly string fio;
        public readonly string fio_child;
        public readonly string nazn;
        public readonly string kbk;
        public readonly string oktmo;
        public readonly long summa;
        public readonly string period;
        public readonly string classNum;
        public readonly string group;

        public QrStructureKindergarden(string address, string fio, string fio_child, string classNum, string nazn, string kbk, string oktmo, string summa, string period, string group)
            : base()
        {
            this.address = address;
            this.fio = fio;
            this.fio_child = fio_child;
            this.nazn = nazn;
            this.kbk = kbk;
            this.oktmo = oktmo;
            this.summa = (long)(double.Parse(summa) * 100);
            this.period = period;
            this.classNum = classNum;
            this.group = group;
        }
    }
}
