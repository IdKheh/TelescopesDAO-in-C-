using Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;
using System.Windows.Data;

namespace TelescopeGUI.ViewModels
{
    public class ProducerListViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        private ListCollectionView view;

        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        private ObservableCollection<ProducerViewModel> producers;

        public ObservableCollection<ProducerViewModel> Producers
        {
            get { return producers; }
            set { producers = value; RaisePropertyChanged(nameof(Producers)); }
        }

        private IDAO dao;
        public ProducerListViewModel()
        {
            string libraryName = ConfigurationManager.AppSettings["libraryFile"];
            dao = new BLC.BLC(libraryName).DAO;
            producers = new ObservableCollection<ProducerViewModel>();

            foreach (var producer in dao.GetAllProducers())
            {
                Producers.Add(new ProducerViewModel(producer));
            }
            view = (ListCollectionView)CollectionViewSource.GetDefaultView(producers);
            addNewProducerCommand = new RelayCommand(_ => AddNewProducer(), _ => CanAddNewProducer());
            saveProducerCommand = new RelayCommand(_ => SaveProducer(), _ => CanSaveProducer());
            filterDataCommand = new RelayCommand(_ => FilterData());
            undoChangesCommand = new RelayCommand(_ => UndoChanges());
        }

        private ProducerViewModel selectedProducer;

        public ProducerViewModel SelectedProducer
        {
            get { return selectedProducer; }
            set
            {
                selectedProducer = value;

                if (CanAddNewProducer())
                {
                    EditedProducer = SelectedProducer;
                }

                RaisePropertyChanged(nameof(SelectedProducer));


            }

        }

        private ProducerViewModel editedProducer;
        public ProducerViewModel EditedProducer
        {
            get { return editedProducer; }
            set
            {
                editedProducer = value;
                RaisePropertyChanged(nameof(EditedProducer));
            }
        }
        private void AddNewProducer()
        {
            ProducerViewModel cvm = new ProducerViewModel(dao.CreateNewProducer());
            EditedProducer = cvm;
            cvm.IsChanged = true;
            SelectedProducer = null;
        }

        private bool CanAddNewProducer()
        {
            if ((EditedProducer == null) || (!EditedProducer.IsChanged))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CanSaveProducer()
        {
            if ((EditedProducer == null) || (!EditedProducer.IsChanged))
            {
                return false;
            }
            else
            {
                return !EditedProducer.HasErrors;
            }
        }

        private void SaveProducer()
        {
            if (!EditedProducer.HasErrors)
            {
                if (EditedProducer.Id == 0)
                {
                    int Id = 0;
                    foreach (var p in producers)
                    {
                        if (p.Id > Id) Id = p.Id;
                    }
                    EditedProducer.Id = Id + 1;
                    producers.Add(EditedProducer);
                    dao.AddProducer(editedProducer.Producer);
                }
                else
                {
                    foreach (var t in producers)
                    {
                        if (t.Id == EditedProducer.Id)
                        {
                            producers[producers.IndexOf(t)] = EditedProducer;
                            dao.UpdateProducer(editedProducer.Producer);
                            break;
                        }
                    }
                }
                EditedProducer.IsChanged = false;
                dao.SaveChanges();
                EditedProducer = null;
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
                view.Filter = c => ((ProducerViewModel)c).Name.Contains(filter);
            }
        }

        private void UndoChanges()
        {
            if (EditedProducer != null)
            {
                if (EditedProducer.Id != 0)
                {
                    dao.UndoChanges();
                    IProducer producer = dao.GetAllProducers().First(c => c.Id == EditedProducer.Id);
                    int index = Producers.IndexOf(EditedProducer);
                    Producers[index] = new ProducerViewModel(producer);
                }
            }
            EditedProducer = null;
        }

        private RelayCommand addNewProducerCommand;

        public RelayCommand AddNewProducerCommand
        {
            get => addNewProducerCommand;
        }

        private RelayCommand saveProducerCommand;

        public RelayCommand SaveProducerCommand
        {
            get => saveProducerCommand;
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
