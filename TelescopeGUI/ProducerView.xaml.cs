using Interfaces;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using TelescopeGUI.ViewModels;

namespace TelescopeGUI
{
    public partial class ProducerView : Window
    {
        public ProducerView()
        {
            InitializeComponent(); 
            DataContext = ProducerListViewModel.Instance;
        }
        private void SwitchToTelescoperView_Click(object sender, RoutedEventArgs e)
        {
            SwitchToTelescopeView();
        }
        private void SwitchToTelescopeView()
        {
            var telescopeView = new MainWindow();
            telescopeView.Show();
            this.Close();
        }
        public ObservableCollection<ProducerViewModel> getProducerList()
        {
            return ProducerListViewModel.Instance.Producers;
        }
    }
}
