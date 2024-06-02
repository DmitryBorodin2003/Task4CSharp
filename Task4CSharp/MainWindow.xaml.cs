using System.Windows;
using System.Windows.Threading;

namespace Task4CSharp
{
    public partial class MainWindow : Window
    {
        private MainViewModel mainViewModel;

        public MainWindow()
        {
            InitializeComponent();
            Dispatcher dispatcher = Dispatcher.CurrentDispatcher;
            mainViewModel = new MainViewModel(dispatcher);
            DataContext = mainViewModel;
        }

        private async void CreateNewCrosswalkButton(object sender, RoutedEventArgs e)
        {
            await mainViewModel.InitializeCrosswalksAsync();
        }
    }
}
