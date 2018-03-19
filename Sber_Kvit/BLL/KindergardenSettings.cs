using Sber_Kvit.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sber_Kvit.BLL
{
    public class KindergardenSettings
    {
        public readonly string FioField;
        public readonly string FioChildField;
        public readonly string NaznField;
        public readonly string KbkField;
        public readonly string OktmoField;
        public readonly string SummaField;
        public readonly string PeriodField;
        public readonly string ClassNumField;
        public readonly string GroupField;

        public KindergardenSettings()
        {
            this.FioField = Settings.Default.tb_set_file_Fio;
            this.FioChildField = Settings.Default.tb_set_file_FioChild;
            this.NaznField = Settings.Default.tb_set_file_Nazn;
            this.KbkField = Settings.Default.tb_set_file_Kbk;
            this.OktmoField = Settings.Default.tb_set_file_Oktmo;
            this.SummaField = Settings.Default.tb_set_file_Summa;
            this.PeriodField = Settings.Default.tb_set_file_Period;
            this.ClassNumField = Settings.Default.tb_set_file_ClassNum;
            this.GroupField = Settings.Default.tb_set_file_Group;
        }

        public string[] GetArrayOfSettings(){
            return new string[] { this.FioField, this.FioChildField, this.NaznField, this.KbkField, this.OktmoField, this.SummaField, this.PeriodField, this.ClassNumField, this.GroupField };
        }
    }
}
