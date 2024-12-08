using Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Windows.Data;

namespace TelescopeGUI.ViewModels
{
    public class TelescopeListViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private ListCollectionView view;

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<TelescopeViewModel> telescopes;

        public ObservableCollection<TelescopeViewModel> Telescopes
        {
            get { return telescopes; }
            set { telescopes = value; RaisePropertyChanged(nameof(Telescopes)); }
        }

        private ObservableCollection<IProducer> producers;
        public ObservableCollection<IProducer> Producers
        {
            get { return producers; }
            set
            {
                producers = value;
                RaisePropertyChanged(nameof(Producers));
            }
        }
        private IDAO dao;
        public TelescopeListViewModel()
        {
            string libraryName = ConfigurationManager.AppSettings["libraryFile"];
            dao = new BLC.BLC(libraryName).DAO;
            producers = new ObservableCollection<IProducer>(dao.GetAllProducers());
            telescopes = new ObservableCollection<TelescopeViewModel>();

            foreach (var telescope in dao.GetAllTelescopes())
            {
                Telescopes.Add(new TelescopeViewModel(telescope));
            }
            view = (ListCollectionView)CollectionViewSource.GetDefaultView(telescopes);
            addNewTelescopeCommand = new RelayCommand(_ => AddNewTelescope(), _ => CanAddNewTelescope());
            saveTelescopeCommand = new RelayCommand(_ => SaveTelescope(), _ => CanSaveTelescope());
            filterDataCommand = new RelayCommand(_ => FilterData());
            undoChangesCommand = new RelayCommand(_ => UndoChanges());
        }

        private TelescopeViewModel selectedTelescope;

        public TelescopeViewModel SelectedTelescope
        {
            get { return selectedTelescope; }
            set
            {
                selectedTelescope = value;

                if (CanAddNewTelescope())
                {
                    EditedTelescope = SelectedTelescope;
                }

                RaisePropertyChanged(nameof(SelectedTelescope));


            }

        }

        private TelescopeViewModel editedTelescope;
        public TelescopeViewModel EditedTelescope
        {
            get { return editedTelescope; }
            set
            {
                editedTelescope = value;
                RaisePropertyChanged(nameof(EditedTelescope));
            }
        }
        private void AddNewTelescope()
        {
            TelescopeViewModel cvm = new TelescopeViewModel(dao.CreateNewTelescope());
            EditedTelescope = cvm;
            cvm.IsChanged = true;
            SelectedTelescope = null;
        }

        private bool CanAddNewTelescope()
        {
            if ((EditedTelescope == null) || (!EditedTelescope.IsChanged))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CanSaveTelescope()
        {
            if ((EditedTelescope == null) || (!EditedTelescope.IsChanged))
            {
                return false;
            }
            else
            {
                return !EditedTelescope.HasErrors;
            }
        }

        private void SaveTelescope()
        {
            if (!EditedTelescope.HasErrors)
            {
                if (EditedTelescope.Id == 0)
                {
                    int Id = 0;
                    foreach (var t in telescopes)
                    {
                        if(t.Id > Id) Id = t.Id;
                    }
                    EditedTelescope.Id = Id+1;
                    telescopes.Add(EditedTelescope);
                    dao.AddTelescope(editedTelescope.Telescope);
                }
                else
                {
                    foreach (var t in telescopes)
                    {
                        if (t.Id == EditedTelescope.Id)
                        {
                            telescopes[telescopes.IndexOf(t)] = EditedTelescope;
                            dao.UpdateTelescope(editedTelescope.Telescope);
                            break;
                        }
                    }
                }
                EditedTelescope.IsChanged = false;
                dao.SaveChanges();
                EditedTelescope = null;
            }
        }

        private string filter;
        public string Filter
        {
            get => filter;
            set
            {
                filter = value;
                RaisePropertyChanged(nameof(filter));
            }
        }

        private void FilterData()
        {
            if (string.IsNullOrEmpty(filter))
            {
                view.Filter = null;

            }
            else
            {
                view.Filter = c => ((TelescopeViewModel)c).Name.Contains(filter);
            }
        }

        private void UndoChanges()
        {
            if (EditedTelescope != null)
            {
                if (EditedTelescope.Id != 0)
                {
                    dao.UndoChanges();
                    ITelescope telescope = dao.GetAllTelescopes().First(c => c.Id == EditedTelescope.Id);
                    int index = Telescopes.IndexOf(EditedTelescope);
                    Telescopes[index] = new TelescopeViewModel(telescope);
                }
            }
            EditedTelescope = null;
        }

        private RelayCommand addNewTelescopeCommand;

        public RelayCommand AddNewTelescopeCommand
        {
            get => addNewTelescopeCommand;
        }

        private RelayCommand saveTelescopeCommand;

        public RelayCommand SaveTelescopeCommand
        {
            get => saveTelescopeCommand;
        }

        private RelayCommand filterDataCommand;
        public RelayCommand FilterDataCommand
        { get => filterDataCommand; }

        private RelayCommand undoChangesCommand;
        public RelayCommand UndoChangesCommand
        {
            get => undoChangesCommand;
        }
    }
}
