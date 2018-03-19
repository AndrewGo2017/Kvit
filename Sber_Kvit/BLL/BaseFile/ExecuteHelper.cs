using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sber_Kvit.BLL.BaseFile
{
    class ExecuteHelper
    {
        public static string ConvertStringToSum(object str, int strNum, string field)
        {
            string convertedStr = str.ToString().Trim().Replace(" ", "").Replace(".", ",");
            double sum;
            bool isCorrecr = double.TryParse(convertedStr, out sum);

            if (!isCorrecr)
            {
                ThrowException("Неверное значение в поле типа Сумма. Наименование(номер) поля: " + field + ". Номер строки: " + strNum);
            }

            return convertedStr;
        }

        public static void NoFieldsAction(List<string> noFields)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < noFields.Count; i++)
            {
                sb.Append(noFields[i]).Append(",");
            }

            ThrowException("В файле не найдеды следующие поля: " + sb.ToString().Substring(0, sb.Length - 1));
        }

        public static void ThrowException(string message)
        {
            throw new Exception(message);
        }
    }
}
