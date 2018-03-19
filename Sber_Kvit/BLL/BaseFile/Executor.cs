using Sber_Kvit.BLL.Barcode;
using Sber_Kvit.BLL.BaseFile;
using Sber_Kvit.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sber_Kvit.BLL.BaseFile
{
    class Executor
    {
        public List<KindergardenDataSet> Execute()
        {
            try
            {
                return GetClassToExecute().Go();
            }
            catch
            {
                throw;
            }
        }

        private IBaseKindergardenHandler GetClassToExecute()
        {
            string format = Path.GetExtension(Settings.Default.tb_set_addSet_ResourcePath).ToUpper();
            IBaseKindergardenHandler baseHandler = null;

            switch (format)
            {
                case ".DBF":
                    baseHandler = new DBFExecutor();
                    break;
                case ".XLS":
                case ".XLSX":
                    baseHandler = new ExcelExecutor();
                    break;
                case ".TXT":
                case ".CSV":
                    baseHandler = new TextExecutor();
                    break;
            }

            return baseHandler;
        }
    }
}
