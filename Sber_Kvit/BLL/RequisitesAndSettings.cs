using Sber_Kvit.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sber_Kvit.BLL
{
    public class RequisitesAndSettings
    {
        public readonly string[] OrgName;
        public readonly string INN;
        public readonly string KPP;
        public readonly string PayAcc;
        public readonly string Bank;
        public readonly string BIC;
        public readonly string Period;

        public readonly string ResultPath;
        public readonly string ResourceFile;
        public readonly bool PrintDateSign;
        public readonly int FontSize;
        public readonly int KvitCount;
        public readonly int Frame;
        public readonly string[] AddInfo;

        public RequisitesAndSettings()
        {
            this.OrgName = Settings.Default.tb_set_req_OrgName.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);

            this.INN = Settings.Default.tb_set_req_Inn;
            this.KPP = Settings.Default.tb_set_req_Kpp;
            this.PayAcc = Settings.Default.tb_set_req_PayAcc;
            this.Bank = Settings.Default.tb_set_req_Bank;
            this.BIC = Settings.Default.tb_set_req_Bic;
            if (Settings.Default.tb_set_file_Period == "")
            {
                string month = DateTime.Now.Month < 10 ? "0" + DateTime.Now.Month.ToString() : DateTime.Now.Month.ToString();
                this.Period = month + " " + DateTime.Now.Year;
            }
            else
                this.Period = Settings.Default.tb_set_file_Period;

            this.ResultPath = Settings.Default.tb_set_addSet_ResultPath;
            this.ResourceFile = Settings.Default.tb_set_addSet_ResourcePath;
            this.PrintDateSign = Settings.Default.cb_set_addSet_PrintDateSign;
            this.FontSize = Settings.Default.tb_set_addSet_FontSize;
            this.KvitCount = Settings.Default.combo_set_addSet_KvitCount;
            this.Frame = Settings.Default.combo_set_addSet_Frame;
            string[] AddInfo_ = Settings.Default.tb_set_addSet_AddInfo.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            this.AddInfo = AddInfo_;
        }
    }
}
