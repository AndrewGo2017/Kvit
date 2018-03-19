using Sber_Kvit.BLL.Barcode;
using Sber_Kvit.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sber_Kvit.BLL.BaseFile
{
    class TextExecutor : IBaseKindergardenHandler
    {
        private string FileBase = Settings.Default.tb_set_addSet_ResourcePath;
        private const char DELIMITER = ';';
        public List<KindergardenDataSet> Go()
        {
            return GetKindergardenData();
        }

        private void CheckFieldInFile(Exception ex, string SumChargeInFile, int num)
        {
            if (ex is FormatException)
            {
                throw new Exception("Неверный формат поля (" + SumChargeInFile + "). Поле должно быть числом.");
            }
            else if (ex is IndexOutOfRangeException)
            {
                throw new Exception("Отсутствует поле в файле. Номер поля: " + SumChargeInFile + ". Номер строки: " + num);
            }
        }

        private List<KindergardenDataSet> GetKindergardenData()
        {
            List<KindergardenDataSet> listOfDataSet = new List<KindergardenDataSet>();
            KindergardenSettings settings = new KindergardenSettings();

            using (StreamReader Reader = new StreamReader(FileBase))
            {
                int num = 0;
                while (!Reader.EndOfStream)
                {
                    num++;
                    string line = Reader.ReadLine();
                    if (line == "") continue;
                    string[] sublines = line.Split(DELIMITER);

                    KindergardenDataSet dataSet = new KindergardenDataSet();

                    if (settings.FioField != "")
                    {
                        try
                        {
                            dataSet.Fio = sublines[int.Parse(settings.FioField)];
                        }
                        catch (Exception ex)
                        {
                            CheckFieldInFile(ex, settings.FioField, num);
                        }
                    }

                    if (settings.FioChildField != "")
                    {
                        try
                        {
                            dataSet.FioChild = sublines[int.Parse(settings.FioChildField)];
                        }
                        catch (Exception ex)
                        {
                            CheckFieldInFile(ex, settings.FioChildField, num);
                        }
                    }

                    if (settings.NaznField != "")
                    {
                        try
                        {
                            dataSet.Nazn = sublines[int.Parse(settings.NaznField)];
                        }
                        catch (Exception ex)
                        {
                            CheckFieldInFile(ex, settings.NaznField, num);
                        }
                    }

                    if (settings.KbkField != "")
                    {
                        try
                        {
                            dataSet.Kbk = sublines[int.Parse(settings.KbkField)];
                        }
                        catch (Exception ex)
                        {
                            CheckFieldInFile(ex, settings.KbkField, num);
                        }
                    }

                    if (settings.OktmoField != "")
                    {
                        try
                        {
                            dataSet.Oktmo = sublines[int.Parse(settings.OktmoField)];
                        }
                        catch (Exception ex)
                        {
                            CheckFieldInFile(ex, settings.OktmoField, num);
                        }
                    }

                    if (settings.SummaField != "")
                    {
                        try
                        {
                            dataSet.Summa = ExecuteHelper.ConvertStringToSum(sublines[int.Parse(settings.SummaField)], num, settings.SummaField);
                        }
                        catch (Exception ex)
                        {
                            CheckFieldInFile(ex, settings.SummaField, num);
                        }
                    }

                    if (settings.PeriodField != "")
                    {
                        try
                        {
                            dataSet.Period = sublines[int.Parse(settings.PeriodField)];
                        }
                        catch (Exception ex)
                        {
                            CheckFieldInFile(ex, settings.PeriodField, num);
                        }
                    }

                    if (settings.ClassNumField != "")
                    {
                        try
                        {
                            dataSet.ClassNum = sublines[int.Parse(settings.ClassNumField)];
                        }
                        catch (Exception ex)
                        {
                            CheckFieldInFile(ex, settings.ClassNumField, num);
                        }
                    }

                    if (settings.GroupField != "")
                    {
                        try
                        {
                            dataSet.Group = sublines[int.Parse(settings.GroupField)];
                        }
                        catch (Exception ex)
                        {
                            CheckFieldInFile(ex, settings.GroupField, num);
                        }
                    }

                    listOfDataSet.Add(dataSet);
                }
            }

            return listOfDataSet;
        }
    }
}