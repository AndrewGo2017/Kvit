using Sber_Kvit.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sber_Kvit.BLL.Barcode
{
    class QrStructure
    {
        public readonly string Delimiter;
        public readonly string Version_Encoding;
        public readonly string Name;
        public readonly string PersonalAcc;
        public readonly string BankName;
        public readonly string BIC;
        public readonly string CorrespAcc;
        public readonly string PayeeINN;
        public readonly string KPP;

        protected QrStructure()
        {
            this.Delimiter = "|";
            this.Version_Encoding = "ST00012";
            this.Name = Settings.Default.tb_set_req_OrgName;
            this.PersonalAcc = Settings.Default.tb_set_req_PayAcc;
            this.BankName = Settings.Default.tb_set_req_Bank;
            this.BIC = Settings.Default.tb_set_req_Bic;
            this.CorrespAcc = "0";
            this.PayeeINN = Settings.Default.tb_set_req_Inn;
            this.KPP = Settings.Default.tb_set_req_Kpp;
        }
    }
}
