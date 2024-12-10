using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Input;
using TelescopeGUI.ViewModels;

namespace TelescopeGUI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = TelescopeListViewModel.Instance;
        }

        private void SwitchToProducerView_Click(object sender, RoutedEventArgs e)
        {
            if (AddButton.IsEnabled)
            {
                SwitchToProducerView();
            }
        }

        public ObservableCollection<TelescopeViewModel> getTelescopesList()
        {
            return TelescopeListViewModel.Instance.Telescopes;
        }

        private void SwitchToProducerView()
        {
            var producerView = new ProducerView();
            producerView.Show();

            this.Close();
        }
    }
}