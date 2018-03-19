using System;
using System.Text;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;
using Sber_Kvit.Properties;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Globalization;
using Sber_Kvit.BLL;
using System.Threading.Tasks;
using System.Threading;
using Sber_Kvit.BLL.BaseFile;
using Sber_Kvit.BLL.Barcode;
using Sber_Kvit.BLL.Templates.Util;

namespace Sber_Kvit.BLL.Templates
{
    class StandartTemplate : Template
    {
        public StandartTemplate(TextBlock mainStat)
            : base(mainStat) { }

        override public void CreateBills()
        {
            List<KindergardenDataSet> dataSet = new Executor().Execute();
            if (!MainWindow.LikeCancellationTokenSource) return;

            using (FileStream stream = new FileStream(Path.Combine(Requisites.ResultPath, "kvit_sbrf.pdf"), FileMode.Create))
            {
                using (Document document = new Document(PageSize.A4, 30, 30, 10, 10))
                {
                    PdfWriter.GetInstance(document, stream);
                    document.Open();

                    for (int i = 0; i < dataSet.Count; i++)
                    {
                        if (!MainWindow.LikeCancellationTokenSource) return;

                        MainStat.Dispatcher.BeginInvoke((Action)(() => MainStat.Text = "Создание квитанций  " + (i + 1).ToString() + " из " + dataSet.Count + " - " + (DateTime.Now - StartTime).ToString()));
                        Thread.Sleep(10);

                        document.Add(GetMainBlock(dataSet[i]));

                        if (Requisites.KvitCount == 2 || Requisites.KvitCount == 3 || Requisites.KvitCount == 4)
                        {
                            if ((i + 1) % Requisites.KvitCount == 0)
                            {
                                document.NewPage();
                            }
                            else
                            {
                                if (Requisites.Frame != 0)
                                {
                                    PdfPTable emptyTable = new PdfPTable(1);
                                    PdfPCell emptyCell = new PdfPCell(new Phrase(" ", Fonts.Font3)) { Border = 0 };
                                    emptyTable.AddCell(emptyCell);
                                    document.Add(emptyTable);
                                }
                            }
                        }
                        else document.NewPage();
                    }
                }
            }
        }

        private PdfPTable GetMainBlock(KindergardenDataSet dataSet)
        {
            PdfPTable tableL0 = new PdfPTable(2);
            tableL0.PaddingTop = 0;
            tableL0.HorizontalAlignment = 0;
            tableL0.SetWidths(new float[] { 3, 7 });
            tableL0.DefaultCell.Border = Rectangle.NO_BORDER;
            tableL0.DefaultCell.Padding = 0;

            PdfPTable tableL1 = new PdfPTable(3);
            tableL1.HorizontalAlignment = 0;
            tableL1.SetWidths(new float[] { 4.5f, 1, 4.5f });
            tableL1.DefaultCell.Border = Rectangle.NO_BORDER;
            tableL1.DefaultCell.Padding = 1;
            tableL1.DefaultCell.HorizontalAlignment = 1;

            foreach (string addinfostr in Requisites.OrgName)
            {
                Paragraph addInfoPar = new Paragraph(new Phrase(" " + addinfostr, Fonts.Font8));
                addInfoPar.PaddingTop = 0;
                addInfoPar.SpacingBefore = 0;
                addInfoPar.SpacingAfter = 0;

                tableL1.AddCell(new PdfPCell(addInfoPar) { Border = 0, Colspan = 3 });
            }


            tableL1.AddCell(new PdfPCell(new Phrase(Requisites.INN + " / " + Requisites.KPP, Fonts.Font10)) { Border = Rectangle.BOTTOM_BORDER, HorizontalAlignment = 1 });
            tableL1.AddCell(new PdfPCell(new Phrase(" ", Fonts.Font10)) { Border = 0 });
            tableL1.AddCell(new PdfPCell(new Phrase(Requisites.PayAcc, Fonts.Font10)) { Border = Rectangle.BOTTOM_BORDER, HorizontalAlignment = 1 });
            tableL1.AddCell(new Phrase("(инн / кпп получателя плателя)", Fonts.Font7));
            tableL1.AddCell(new Phrase(" ", Fonts.Font7));
            tableL1.AddCell(new Phrase("(номер счета получателя)", Fonts.Font7));

            tableL1.AddCell(new PdfPCell(new Phrase(Requisites.Bank, Fonts.Font10)) { Border = Rectangle.BOTTOM_BORDER, HorizontalAlignment = 1 });
            tableL1.AddCell(new PdfPCell(new Phrase(" ", Fonts.Font10)) { Border = 0 });
            tableL1.AddCell(new PdfPCell(new Phrase(Requisites.BIC, Fonts.Font10)) { Border = Rectangle.BOTTOM_BORDER, HorizontalAlignment = 1 });
            tableL1.AddCell(new Phrase("(наименование банка получателя платежа)", Fonts.Font7));
            tableL1.AddCell(new Phrase(" ", Fonts.Font7));
            tableL1.AddCell(new Phrase("(бик)", Fonts.Font7));

            tableL1.AddCell(new PdfPCell(new Phrase(dataSet.Kbk, Fonts.Font10)) { Border = Rectangle.BOTTOM_BORDER, HorizontalAlignment = 1 });
            tableL1.AddCell(new PdfPCell(new Phrase(" ", Fonts.Font10)) { Border = 0 });
            tableL1.AddCell(new PdfPCell(new Phrase(dataSet.Oktmo, Fonts.Font10)) { Border = Rectangle.BOTTOM_BORDER, HorizontalAlignment = 1 });
            tableL1.AddCell(new Phrase("(кбк)", Fonts.Font7));
            tableL1.AddCell(new Phrase(" ", Fonts.Font7));
            tableL1.AddCell(new Phrase("(октмо)", Fonts.Font7));

            tableL1.AddCell(new PdfPCell(new Phrase(dataSet.Nazn, Fonts.Font10)) { Border = Rectangle.BOTTOM_BORDER, HorizontalAlignment = 1 });
            tableL1.AddCell(new PdfPCell(new Phrase(" ", Fonts.Font10)) { Border = 0 });
            tableL1.AddCell(new PdfPCell(new Phrase(dataSet.Period, Fonts.Font10)) { Border = Rectangle.BOTTOM_BORDER, HorizontalAlignment = 1 });
            tableL1.AddCell(new Phrase("(наименование платежа)", Fonts.Font7));
            tableL1.AddCell(new Phrase(" ", Fonts.Font7));
            tableL1.AddCell(new Phrase("(период)", Fonts.Font7));


            PdfPTable tableL2 = new PdfPTable(2);
            tableL2.HorizontalAlignment = 0;
            tableL2.SetWidths(new float[] { 3, 7 });
            tableL2.DefaultCell.Border = Rectangle.NO_BORDER;
            tableL2.DefaultCell.Padding = 1;
            tableL2.DefaultCell.HorizontalAlignment = 0;

            tableL2.AddCell(new Phrase("ФИО плательщика", Fonts.Font10));
            tableL2.AddCell(new PdfPCell(new Phrase(dataSet.Fio, Fonts.Font10)) { Border = Rectangle.BOTTOM_BORDER, HorizontalAlignment = 1 });

            tableL2.AddCell(new Phrase("ФИО ребенка", Fonts.Font10));
            tableL2.AddCell(new PdfPCell(new Phrase(dataSet.FioChild + " " + dataSet.Group, Fonts.Font10)) { Border = Rectangle.BOTTOM_BORDER, HorizontalAlignment = 1 });

            tableL2.AddCell(new Phrase("Сумма платежа", Fonts.Font10));
            try
            {
                if (dataSet.Summa == "")
                {
                    tableL2.AddCell(new PdfPCell(new Phrase("__________", Fonts.Font10)) { Border = 0, HorizontalAlignment = 0 });
                }
                else
                {
                    tableL2.AddCell(new PdfPCell(new Phrase(double.Parse(dataSet.Summa).ToString("n", new CultureInfo("nb-NO")) ?? "0" + " руб.", Fonts.Font10_underlined)) { Border = 0, HorizontalAlignment = 0 });
                }
            }
            catch (Exception)
            {
                tableL2.AddCell(new PdfPCell(new Phrase(double.Parse("0").ToString("n", new CultureInfo("nb-NO")) ?? "0" + " руб.", Fonts.Font10_underlined)) { Border = 0, HorizontalAlignment = 0 });
            }

            tableL2.AddCell(new PdfPCell(new Phrase(" ", Fonts.Font3)) { Border = 0, Colspan = 2 });

            foreach (string addinfostr in Requisites.AddInfo)
            {
                Paragraph addInfoPar = new Paragraph(new Phrase("  " + addinfostr, Fonts.Font5));
                addInfoPar.PaddingTop = 0;
                addInfoPar.SpacingBefore = 0;
                addInfoPar.SpacingAfter = 0;

                tableL2.AddCell(new PdfPCell(addInfoPar) { Border = 0, Colspan = 2 });
            }

            if (Requisites.PrintDateSign)
            {
                tableL2.AddCell(new PdfPCell(new Phrase("Дата ________________", Fonts.Font8)) { Border = 0, HorizontalAlignment = 2 });
                tableL2.AddCell(new PdfPCell(new Phrase("Подпись плательщика _______________", Fonts.Font8)) { Border = 0, HorizontalAlignment = 2 });
            }

            iTextSharp.text.Image imgQR = iTextSharp.text.Image.GetInstance(new BarcoderKindergarden(
                new QrStructureKindergarden(
                    "",
                    dataSet.Fio,
                    dataSet.FioChild,
                    dataSet.ClassNum,
                    dataSet.Nazn,
                    dataSet.Kbk,
                    dataSet.Oktmo,
                    dataSet.Summa == "" ? "0" : dataSet.Summa,
                    dataSet.Period,
                    dataSet.Group)).GetQR());


            imgQR.ScaleAbsolute(70, 70);
            PdfPTable tableWithQR = new PdfPTable(1);
            tableWithQR.WidthPercentage = 50;
            tableWithQR.DefaultCell.Border = 0;
            tableWithQR.HorizontalAlignment = 0;
            tableWithQR.AddCell(new PdfPCell(tableL1) { Border = 0, HorizontalAlignment = 0, VerticalAlignment = 0 });

            tableL0.AddCell(new PdfPCell(imgQR) { Border = Rectangle.RIGHT_BORDER, Rowspan = 1, HorizontalAlignment = 1, VerticalAlignment = 1, PaddingTop = 15 });
            tableL0.AddCell(tableL1);
            tableL0.AddCell(new PdfPCell() { Border = Rectangle.RIGHT_BORDER });
            tableL0.AddCell(tableL2);

            Paragraph mainBlock = new Paragraph();
            mainBlock.Add(tableL0);

            PdfPTable borderTable = new PdfPTable(1);
            borderTable.WidthPercentage = 100;
            PdfPCell borderTableCell = new PdfPCell() { BorderWidth = Requisites.Frame };
            borderTableCell.AddElement(mainBlock);
            borderTable.AddCell(borderTableCell);

            return borderTable;
        }
    }
}
