using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace lab10
{
    /// <summary>
    /// Логика взаимодействия для GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        Notifier notifier = new Notifier(cfg =>
        {
            cfg.PositionProvider = new WindowPositionProvider(
                parentWindow: Application.Current.MainWindow,
                corner: Corner.TopRight,
                offsetX: 10,
                offsetY: 10);

            cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                notificationLifetime: TimeSpan.FromSeconds(5),
                maximumNotificationCount: MaximumNotificationCount.FromCount(5));

            cfg.Dispatcher = Application.Current.Dispatcher;
        });

        public UserModel user = new UserModel();
        public Question question = new Question();
        private int timeLeft = 60;
        private string categoryName;

        public GameWindow(string name, string category)
        {
            InitializeComponent();

            categoryName = category;
            user.UserName = name;
            user.NumOfCorrect = 0;
            user.NumOfInCorrect = 0;
        }

        private void Play_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                question.FillAll();
                question.FilterByCategory(categoryName);

                var timer = new System.Windows.Threading.DispatcherTimer();
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Tick += (s, ev) =>
                {
                    timeLeft--;
                    Timer_label.Content = $"Времени осталось: {timeLeft} сек";
                    progress.Value = (60 - timeLeft) / 60.0 * 100;
                    if (timeLeft <= 0)
                    {
                        timer.Stop();

                        ResultWindow window = new ResultWindow(user);
                        window.user = user;
                        window.Show();

                        Play_btn.IsEnabled = true;
                        Image.Source = null;
                    }
                };
                timer.Start();

                Play_btn.IsEnabled = false;

                NextQuestion();
            }
            catch (Exception ex)
            {
                notifier.ShowError($"Error: {ex.Message}");
            }
        }

        private void Stop_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                timeLeft = 1;
                progress.Value = 100;
            }
            catch (Exception ex)
            {
                notifier.ShowError($"Error: {ex.Message}");
            }
        }

        private void Rules_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            }
            catch (Exception ex)
            {
                notifier.ShowError($"Error: {ex.Message}");
            }
        }

        private void Menu_btn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void Sumbit_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (timeLeft != 60 && timeLeft != 0)
                {
                    string text = Answer_tb.Text;
                    if (question.CheckAnswer(text))
                    {
                        user.NumOfCorrect++;
                        notifier.ShowSuccess("Правильно!");
                    }
                    else
                    {
                        user.NumOfInCorrect++;
                        notifier.ShowError("Не верно!");
                    }

                    Answer_tb.Text = "";

                    if (question.data.Count() > 0)
                    {
                        NextQuestion();
                    }
                    else
                    {
                        timeLeft = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                notifier.ShowError($"Error: {ex.Message}");
            }
        }

        private async Task NextQuestion()
        {
            question.GenerateQuestion();
            LoadImageAsync(question.GetQuestionImage());
        }

        private async Task LoadImageAsync(string imageUrl)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    byte[] imageData = await client.GetByteArrayAsync(imageUrl);

                    BitmapImage bitmapImage = new BitmapImage();

                    using (MemoryStream stream = new MemoryStream(imageData))
                    {
                        bitmapImage.BeginInit();
                        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                        bitmapImage.StreamSource = stream;
                        bitmapImage.EndInit();
                    }

                    Image.Source = bitmapImage;
                }
            }
            catch (Exception ex)
            {
                notifier.ShowError($"Error: {ex.Message}");
            }
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Sumbit_btn_Click(sender, e);
            }
        }

    }
}
