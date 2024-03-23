using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace lab10
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
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


        public MainWindow()
        {
            List<string> categories = new List<string>();
            categories.Add("Еда");
            categories.Add("Растения");
            categories.Add("Животные");
            categories.Add("Все");
            InitializeComponent();

            Category_cb.ItemsSource = categories;
        }

        private void Play_btn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string UserName = UserName_tb.Text;
                string CategoryName = Category_cb.Text;
                if (CategoryName != "" && UserName != "")
                {
                    GameWindow window = new GameWindow(UserName, CategoryName);
                    window.Show();
                    this.Close();
                }
                else
                {
                    notifier.ShowWarning("Введите все данные!");
                }
            }
            catch
            {
                notifier.ShowError("Что-то пошло не так!");
            }
        }
    }
}