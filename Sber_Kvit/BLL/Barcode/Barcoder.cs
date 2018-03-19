using MessagingToolkit.QRCode.Codec;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sber_Kvit.BLL.Barcode
{
    class Barcoder 
    {
        protected string text;
        protected QrStructure qrStructure;

        public Barcoder(QrStructure qrStructure)
        {
            string KPP = qrStructure.KPP != "" ? "Kpp=" + qrStructure.KPP + qrStructure.Delimiter : "";
            this.text =
                qrStructure.Version_Encoding + qrStructure.Delimiter +
                "Name=" + qrStructure.Name + qrStructure.Delimiter +
                "PersonalAcc=" + qrStructure.PersonalAcc + qrStructure.Delimiter +
                "BankName=" + qrStructure.BankName + qrStructure.Delimiter +
                "BIC=" + qrStructure.BIC + qrStructure.Delimiter +
                "CorrespAcc=" + qrStructure.CorrespAcc + qrStructure.Delimiter +
                "PayeeINN=" + qrStructure.PayeeINN + qrStructure.Delimiter +
                KPP;

            this.qrStructure = qrStructure;

        }

        public byte[] GetQR()
        {
            QRCodeEncoder encoder = new QRCodeEncoder();

            Bitmap qrcode = new Bitmap(encoder.Encode(text, Encoding.UTF8));
            ImageConverter imgConverter = new ImageConverter();
            return (byte[])imgConverter.ConvertTo(qrcode, typeof(byte[]));
        }
    }
}
