using Sber_Kvit.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sber_Kvit.BLL.Barcode
{
    class BarcoderKindergarden : Barcoder
    {
        public BarcoderKindergarden(QrStructure qrStructure) :
            base(qrStructure)
        {
            QrStructureKindergarden qr = (QrStructureKindergarden)qrStructure;

            this.text +=
                Settings.Default.tb_set_file_ClassNumBar + "=" + qr.classNum + qrStructure.Delimiter +
                Settings.Default.tb_set_file_FioChildBar + "=" + qr.fio_child + qrStructure.Delimiter +
                Settings.Default.tb_set_file_FioBar + "=" + qr.fio + qrStructure.Delimiter +
                Settings.Default.tb_set_file_GroupBar + "=" + qr.group + qrStructure.Delimiter +
                Settings.Default.tb_set_file_PeriodBar + "=" + qr.period + qrStructure.Delimiter +
                Settings.Default.tb_set_file_NaznBar + "=" + qr.nazn + qrStructure.Delimiter +
                Settings.Default.tb_set_file_KbkBar + "=" + qr.kbk + qrStructure.Delimiter +
                Settings.Default.tb_set_file_OktmoBar + "=" + qr.oktmo + qrStructure.Delimiter +
                Settings.Default.tb_set_file_SummaBar + "=" + qr.summa + qrStructure.Delimiter;
        }
    }
}
