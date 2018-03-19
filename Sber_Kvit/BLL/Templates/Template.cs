using iTextSharp.text.pdf;
using Sber_Kvit.BLL.BaseFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Sber_Kvit.BLL.Templates
{
    abstract public class Template
    {
        protected RequisitesAndSettings Requisites = new RequisitesAndSettings();
        protected TextBlock MainStat;
        protected DateTime StartTime;

        public Template(TextBlock mainStat)
        {
            this.MainStat = mainStat;
            this.StartTime = DateTime.Now;
        }

        public abstract void CreateBills();
    }
}
