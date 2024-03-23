using LiveCharts.Wpf;
using LiveCharts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using ToastNotifications.Messages;

namespace lab10
{
    /// <summary>
    /// Логика взаимодействия для ResultWindow.xaml
    /// </summary>
    public partial class ResultWindow : Window
    {
        public UserModel user { get; set; }
        public ResultWindow(UserModel user)
        {
            InitializeComponent();
            Maintext.Content = $"Игра оконченна, {user.UserName}!";
            SeriesCollection = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Правильные ответы",
                    Values = new ChartValues<int> { user.NumOfCorrect }
                },
                new ColumnSeries
                {
                    Title = "Не правильные ответы",
                    Values = new ChartValues<int> { user.NumOfInCorrect }
                },
            };

            Labels = new[] { "Правильные ответы", "Не правильные ответы"};
            Formatter = value => value.ToString("N");

            DataContext = this;
        }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

                if (saveFileDialog.ShowDialog() == true)
                {
                    string newFilePath = saveFileDialog.FileName;

                    using (StreamWriter sw = new StreamWriter(newFilePath))
                    {
                        sw.WriteLine($"Имя: {user.UserName}\nКоличество правильных ответов: {user.NumOfCorrect}\nКоличество не правильных ответов: {user.NumOfInCorrect}");
                    }

                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show ("Error saving file: " + ex.Message);
            }
        }
    }
}
