using Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace TelescopeGUI
{
    public partial class ProducerView : UserControl
    {
        public ProducerView()
        {
            InitializeComponent();  // This should work, as long as the XAML and the code-behind match.
            DataContext = new ViewModels.ProducerListViewModel();
        }
    }
}
