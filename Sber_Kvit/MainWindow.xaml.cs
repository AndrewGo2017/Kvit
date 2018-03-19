using Sber_Kvit.BLL.Templates;
using Sber_Kvit.Properties;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace Sber_Kvit
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SetParametrs();
            progressBar.IsIndeterminate = false;
        }

        private bool IsChecked()
        {
            if (Settings.Default.tb_set_addSet_ResourcePath == "")
            {
                System.Windows.MessageBox.Show("Не выбран Файл-база, либо не сохранен результат (кнопка Сохранить). \nВкладка 'Настройка' -> 'Доп настройки'.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (Settings.Default.tb_set_addSet_ResultPath == "")
            {
                System.Windows.MessageBox.Show("Не выбран Каталог результата, либо не сохранен результат (кнопка Сохранить). \nВкладка 'Настройка' -> 'Доп настройки'.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (Settings.Default.tb_set_file_Fio == "" ||  
                Settings.Default.tb_set_file_FioChild == "" ||
                Settings.Default.tb_set_file_Nazn == "" ||
                Settings.Default.tb_set_file_ClassNum == "" ||
                Settings.Default.tb_set_file_Group == "" ||
                Settings.Default.tb_set_file_Kbk == "" ||
                Settings.Default.tb_set_file_Oktmo == "" ||
                Settings.Default.tb_set_file_Summa == "" ||
                Settings.Default.tb_set_file_Period == "")
            {
                 System.Windows.MessageBox.Show("Не заполнены наименования одного или нескольких полей, либо не сохранен результат (кнопка Сохранить). \nВкладка 'Настройка' -> 'Файл'.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                 return false;
            }

            if (!File.Exists(Settings.Default.tb_set_addSet_ResourcePath))
            {
                System.Windows.MessageBox.Show("Выбранный Файл-база не существует. \nВкладка 'Настройка' -> 'Доп настройки'.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            if (!Directory.Exists(Settings.Default.tb_set_addSet_ResultPath))
            {
                System.Windows.MessageBox.Show("Выбранный Каталог результата не существует. \nВкладка 'Настройка' -> 'Доп настройки'.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            return true;
        }

        private void SetParametrs()
        {
            tb_set_req_OrgName.Text = Settings.Default.tb_set_req_OrgName;
            tb_set_req_Inn.Text = Settings.Default.tb_set_req_Inn;
            tb_set_req_Kpp.Text = Settings.Default.tb_set_req_Kpp;
            tb_set_req_PayAcc.Text = Settings.Default.tb_set_req_PayAcc;
            tb_set_req_Bic.Text = Settings.Default.tb_set_req_Bic;
            tb_set_req_Bank.Text = Settings.Default.tb_set_req_Bank;

            tb_set_file_Fio.Text = Settings.Default.tb_set_file_Fio;
            tb_set_file_FioChild.Text = Settings.Default.tb_set_file_FioChild;
            tb_set_file_Nazn.Text = Settings.Default.tb_set_file_Nazn;
            tb_set_file_Kbk.Text = Settings.Default.tb_set_file_Kbk;
            tb_set_file_Oktmo.Text = Settings.Default.tb_set_file_Oktmo;
            tb_set_file_Summa.Text = Settings.Default.tb_set_file_Summa;
            tb_set_file_Period.Text = Settings.Default.tb_set_file_Period;
            tb_set_file_ClassNum.Text = Settings.Default.tb_set_file_ClassNum;
            tb_set_file_Group.Text = Settings.Default.tb_set_file_Group;

            tb_set_file_FioBar.Text = Settings.Default.tb_set_file_FioBar;
            tb_set_file_FioChildBar.Text = Settings.Default.tb_set_file_FioChildBar;
            tb_set_file_NaznBar.Text = Settings.Default.tb_set_file_NaznBar;
            tb_set_file_KbkBar.Text = Settings.Default.tb_set_file_KbkBar;
            tb_set_file_OktmoBar.Text = Settings.Default.tb_set_file_OktmoBar;
            tb_set_file_SummaBar.Text = Settings.Default.tb_set_file_SummaBar;
            tb_set_file_PeriodBar.Text = Settings.Default.tb_set_file_PeriodBar;
            tb_set_file_ClassNumBar.Text = Settings.Default.tb_set_file_ClassNumBar;
            tb_set_file_GroupBar.Text = Settings.Default.tb_set_file_GroupBar;

            tb_set_addSet_ResourcePath.Text = Settings.Default.tb_set_addSet_ResourcePath;
            tb_set_addSet_ResultPath.Text = Settings.Default.tb_set_addSet_ResultPath;
            cb_set_addSet_PrintDateSign.IsChecked = Settings.Default.cb_set_addSet_PrintDateSign;
            combo_set_addSet_KvitCount.SelectedIndex = Settings.Default.combo_set_addSet_KvitCount - 1;
            combo_set_addSet_Frame.SelectedIndex = Settings.Default.combo_set_addSet_Frame;
            tb_set_addSet_FontSize.SelectedIndex = Settings.Default.tb_set_addSet_FontSize;
            tb_set_addSet_AddInfo.Text = Settings.Default.tb_set_addSet_AddInfo;

        }

        private void btn_set_req_Save_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.tb_set_addSet_AddInfo = tb_set_addSet_AddInfo.Text.Trim();
            Settings.Default.tb_set_req_OrgName = tb_set_req_OrgName.Text.Trim();
            Settings.Default.tb_set_req_Inn = tb_set_req_Inn.Text.Trim();
            Settings.Default.tb_set_req_Kpp = tb_set_req_Kpp.Text.Trim();
            Settings.Default.tb_set_req_PayAcc = tb_set_req_PayAcc.Text.Trim();
            Settings.Default.tb_set_req_Bic = tb_set_req_Bic.Text.Trim();
            Settings.Default.tb_set_req_Bank = tb_set_req_Bank.Text.Trim();
            Settings.Default.Save();

            System.Windows.MessageBox.Show("Сохранено!", "Сообщение");
        }

        private void btn_set_kvit_Save_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.tb_set_file_Fio = tb_set_file_Fio.Text.Trim();
            Settings.Default.tb_set_file_FioChild = tb_set_file_FioChild.Text.Trim();
            Settings.Default.tb_set_file_Nazn = tb_set_file_Nazn.Text.Trim();
            Settings.Default.tb_set_file_Kbk = tb_set_file_Kbk.Text.Trim();
            Settings.Default.tb_set_file_Oktmo = tb_set_file_Oktmo.Text.Trim();
            Settings.Default.tb_set_file_Summa = tb_set_file_Summa.Text.Trim();
            Settings.Default.tb_set_file_Period = tb_set_file_Period.Text.Trim();
            Settings.Default.tb_set_file_ClassNum = tb_set_file_ClassNum.Text.Trim();
            Settings.Default.tb_set_file_Group = tb_set_file_Group.Text.Trim();

            Settings.Default.tb_set_file_FioBar = tb_set_file_FioBar.Text.Trim();
            Settings.Default.tb_set_file_FioChildBar = tb_set_file_FioChildBar.Text.Trim();
            Settings.Default.tb_set_file_NaznBar = tb_set_file_NaznBar.Text.Trim();
            Settings.Default.tb_set_file_KbkBar = tb_set_file_KbkBar.Text.Trim();
            Settings.Default.tb_set_file_OktmoBar = tb_set_file_OktmoBar.Text.Trim();
            Settings.Default.tb_set_file_SummaBar = tb_set_file_SummaBar.Text.Trim();
            Settings.Default.tb_set_file_PeriodBar = tb_set_file_PeriodBar.Text.Trim();
            Settings.Default.tb_set_file_ClassNumBar = tb_set_file_ClassNumBar.Text.Trim();
            Settings.Default.tb_set_file_GroupBar = tb_set_file_GroupBar.Text.Trim();

            Settings.Default.Save();
            System.Windows.MessageBox.Show("Сохранено!", "Сообщение");
        }

        private void btn_set_addSet_Save_Click(object sender, RoutedEventArgs e)
        {
            Settings.Default.tb_set_addSet_ResourcePath = tb_set_addSet_ResourcePath.Text.Trim();
            Settings.Default.tb_set_addSet_ResultPath = tb_set_addSet_ResultPath.Text.Trim();
            Settings.Default.cb_set_addSet_PrintDateSign = cb_set_addSet_PrintDateSign.IsChecked ?? false;
            Settings.Default.combo_set_addSet_KvitCount = int.Parse(((ComboBoxItem)combo_set_addSet_KvitCount.SelectedValue).Content.ToString());
            Settings.Default.combo_set_addSet_Frame = combo_set_addSet_Frame.SelectedIndex;
            Settings.Default.tb_set_addSet_FontSize = tb_set_addSet_FontSize.SelectedIndex;
            try
            {
                Settings.Default.Save();
                System.Windows.MessageBox.Show("Сохранено!", "Сообщение");
            }
            catch (FormatException)
            {
                System.Windows.MessageBox.Show("Неверно указан размер таблицы доп реквизитов!", "Ошибка сохранения", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btn_set_addSet_Browse_ResourcePath_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.Filter = "Файлы Excel(.xls/.xlsx)|*.xls*|Файлы TXT (.txt)|*.txt|Файлы CSV (.csv)|*.csv|Файлы DBF (.dbf)|*.dbf";
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tb_set_addSet_ResourcePath.Text = dialog.FileName;
            }
        }

        private void btn_set_addSet_Browse_ResultPath_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                tb_set_addSet_ResultPath.Text = dialog.SelectedPath;
            }
        }


        public static bool LikeCancellationTokenSource;
        private void btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke((Action)(() =>
            {
                tb_Main_Stat.Text = "Прерывание запроса...";
                btn_Cancel.IsEnabled = false;
            }));
            LikeCancellationTokenSource = false;
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!IsChecked()) return;

            btn_Cancel.IsEnabled = true;
            btn_Go.IsEnabled = false;
            LikeCancellationTokenSource = true;
            tb_Main_Stat.Text = "Обработка данных из файла.";
            Task task = new Task(() =>
            {
                progressBar.Dispatcher.BeginInvoke((Action)(() => progressBar.IsIndeterminate = true));
                new StandartTemplate(tb_Main_Stat).CreateBills();
            });

            Task continueTask = task.ContinueWith((antecedent) =>
            {
                var ex = antecedent.Exception;
                if (ex != null)
                {
                    Dispatcher.BeginInvoke((Action)(() =>
                    {
                        tb_Main_Stat.Text = "Ошибка.";
                        progressBar.IsIndeterminate = false;
                        btn_Cancel.IsEnabled = false;
                        btn_Go.IsEnabled = true;
                    }));

                    if (ex.InnerException != null)
                    {
                        System.Windows.MessageBox.Show(ex.InnerException.Message, "Ошибка");
                    }
                    else
                    {
                        System.Windows.MessageBox.Show(ex.Message, "Ошибка");
                    }
                }
                else
                {
                    if (LikeCancellationTokenSource)
                    {
                        System.Windows.MessageBox.Show("Готово", "Сообщение");
                    }
                    else
                    {
                        Dispatcher.BeginInvoke((Action)(() => tb_Main_Stat.Text = "Запрос прерван."));
                    }
                }

                Dispatcher.BeginInvoke((Action)(() =>
                {
                    progressBar.IsIndeterminate = false;
                    btn_Go.IsEnabled = true;
                    btn_Cancel.IsEnabled = false;
                }));
            });

            task.Start();
        }

        private bool ShowPasswordWindow()
        {
            Windows.PasswordWindow passWindow = new Windows.PasswordWindow();
            passWindow.ShowDialog();
            return passWindow.DialogResult ?? false;
        }
    }
}
