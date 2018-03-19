using Sber_Kvit.BLL.Barcode;
using Sber_Kvit.BLL.BaseFile;
using Sber_Kvit.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Sber_Kvit.BLL.BaseFile
{
    class DBFExecutor : IBaseKindergardenHandler
    {
        private string FileBase = Settings.Default.tb_set_addSet_ResourcePath;
        public List<KindergardenDataSet> Go()
        {
            if (Path.GetFileNameWithoutExtension(FileBase).Length > 8)
            {
                File.Copy(FileBase, Path.Combine(Path.GetDirectoryName(FileBase), "tempFile.dbf"), true);
                FileBase = Path.Combine(Path.GetDirectoryName(FileBase), "tempFile.dbf");
            }

            List<KindergardenDataSet> listOfDataSet = GetKindergardenData();

            if (FileBase.EndsWith("tempFile.dbf"))
                File.Delete(Path.Combine(Path.GetDirectoryName(FileBase), "tempFile.dbf"));

            return listOfDataSet;
        }

        private List<KindergardenDataSet> GetKindergardenData()
        {
            List<KindergardenDataSet> listOfDataSet = new List<KindergardenDataSet>();
            KindergardenSettings settings = new KindergardenSettings();
           
            List<string> noFields = new List<string>();
            bool allFieldsFlag = true;

            string currentDir = System.IO.Directory.GetCurrentDirectory();
            System.IO.Directory.SetCurrentDirectory(Path.GetDirectoryName(FileBase));
            using (OdbcConnection Connection = new OdbcConnection())
            {
                List<string> fileFields;
                OdbcDataReader reader = ExcecuteDBF(Connection, out fileFields);

                //check file
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
            }
            return listOfDataSet;
        }

        private OdbcDataReader ExcecuteDBF(OdbcConnection connection, out List<string> fileFields)
        {
            try
            {
                connection.ConnectionString = @"Driver={Microsoft dBASE Driver (*.dbf)};datasource=" + FileBase;
                connection.Open();
            }
            catch (ExternalException)
            {
                try
                {
                    connection.ConnectionString = @"Driver={Microsoft Access dBASE Driver (*.dbf, *.ndx, *.mdx)};datasource=" + FileBase;
                    connection.Open();
                }
                catch (ExternalException)
                {
                    throw new Exception("Не найдены драйверы Microsoft dBASE Driver и Microsoft Access dBASE Driver.");
                }
            }

            OdbcCommand command = connection.CreateCommand();
            command.CommandText = "select * from " + Path.GetFileName(FileBase);
            OdbcDataReader reader = command.ExecuteReader();
            DataTable structureTable = reader.GetSchemaTable();

            fileFields = new List<string>();
            foreach (DataRow drStruct in structureTable.Rows)
            {
                fileFields.Add(drStruct.ItemArray[0].ToString());
            }

            return reader;
        }
    }
}
