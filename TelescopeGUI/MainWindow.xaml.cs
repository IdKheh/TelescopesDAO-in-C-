using System.Text;
using System.Windows;
using System.Windows.Input;

namespace TelescopeGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ICommand ShowTelescopeViewCommand { get; }
        public ICommand ShowProducerViewCommand { get; }

        public MainWindow()
        {
            ShowTelescopeViewCommand = new RelayCommand(_ => ShowTelescopeView());
            ShowProducerViewCommand = new RelayCommand(_ => ShowProducerView());
            InitializeComponent();
            DataContext = new ViewModels.TelescopeListViewModel();
        }
        public void ShowTelescopeView()
        {
            new TelescopeView();  // Show the Telescope view
        }

        public void ShowProducerView()
        {
            new ProducerView();   // Show the Producer view
        }
    }
}