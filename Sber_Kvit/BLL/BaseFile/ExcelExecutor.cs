using Sber_Kvit.BLL.Barcode;
using Sber_Kvit.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sber_Kvit.BLL.BaseFile
{
    class ExcelExecutor : IBaseKindergardenHandler
    {
        private string FileBase = Settings.Default.tb_set_addSet_ResourcePath;
        private const string SHEET_NAME = "Лист1";

        public List<KindergardenDataSet> Go()
        {
            return GetKindergardenData();
        }

        private List<KindergardenDataSet> GetKindergardenData()
        {
            List<KindergardenDataSet> listOfDataSet = new List<KindergardenDataSet>();
            KindergardenSettings settings = new KindergardenSettings();

            List<string> noFields = new List<string>();
            bool allFieldsFlag = true;

            using (OleDbConnection connection = new OleDbConnection())
            {
                List<string> fileFields;
                OleDbDataReader reader = ExcecuteExcel(connection, out fileFields);

                foreach (string field in settings.GetArrayOfSettings())
                {
                    if (field == "") continue;

                    if (!fileFields.Contains(field))
                    {
                        allFieldsFlag = false;
                        noFields.Add(field);
                    }
                }

                if (allFieldsFlag)
                {
                    int num = 0;
                    while (reader.Read())
                    {
                        num++;

                        KindergardenDataSet dataSet = new KindergardenDataSet();
                        if (fileFields.Contains(settings.FioField))
                            dataSet.Fio = reader[settings.FioField].ToString();

                        if (fileFields.Contains(settings.FioChildField))
                            dataSet.FioChild = reader[settings.FioChildField].ToString();

                        if (fileFields.Contains(settings.NaznField))
                            dataSet.Nazn = reader[settings.NaznField].ToString();

                        if (fileFields.Contains(settings.KbkField))
                            dataSet.Kbk = reader[settings.KbkField].ToString();

                        if (fileFields.Contains(settings.OktmoField))
                            dataSet.Oktmo = reader[settings.OktmoField].ToString();

                        if (fileFields.Contains(settings.SummaField))
                            dataSet.Summa = ExecuteHelper.ConvertStringToSum(reader[settings.SummaField], num, settings.SummaField);

                        if (fileFields.Contains(settings.PeriodField))
                            dataSet.Period = reader[settings.PeriodField].ToString();

                        if (fileFields.Contains(settings.ClassNumField))
                            dataSet.ClassNum = reader[settings.ClassNumField].ToString();

                        if (fileFields.Contains(settings.GroupField))
                            dataSet.Group = reader[settings.GroupField].ToString();

                        listOfDataSet.Add(dataSet);
                    }
                }
                else
                {
                    ExecuteHelper.NoFieldsAction(noFields);
                }

                return listOfDataSet;
            }
        }

        private OleDbDataReader ExcecuteExcel(OleDbConnection connection, out List<string> fileFields)
        {
            try
            {
                connection.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FileBase + ";Extended Properties=Excel 8.0;HDR=Yes;IMEX=1";
                connection.Open();
            }
            catch (Exception)
            {
                try
                {
                    connection.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileBase + ";" + "Extended Properties='Excel 12.0 Xml;HDR=YES;IMEX=1;MAXSCANROWS=0'";
                    connection.Open();
                }
                catch (Exception)
                {
                    throw new Exception("Не найдены драйверы Microsoft.ACE.OLEDB.12.0 и Microsoft.Jet.OLEDB.4.0.");
                }
            }

            fileFields = new List<string>();
            DataTable dt = new DataTable();
            OleDbCommand command = new OleDbCommand();
            command.CommandText = "Select * from [" + SHEET_NAME + "$]";
            command.Connection = connection;

            OleDbDataReader reader = command.ExecuteReader();
            DataTable structureTable = reader.GetSchemaTable();
            foreach (DataRow drStruct in structureTable.Rows)
            {
                fileFields.Add(drStruct.ItemArray[0].ToString());
            }

            return reader;
        }
    }
}
