using System.Collections;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TelescopeGUI.ViewModels
{
    public class TelescopeViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private Interfaces.ITelescope telescope;
        public Interfaces.ITelescope Telescope => telescope;

        public TelescopeViewModel(Interfaces.ITelescope telescope)
        {
            this.telescope = telescope;
            isChanged = false;
        }
        private void RaisePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

            if (propertyName != nameof(HasErrors))
            {
                Validate();
            }
        }

        public Interfaces.OpticalSystem OpticalSystem
        {
            get => telescope.OpticalSystem;
            set
            {
                IsChanged = true;
                telescope.OpticalSystem = value;
                RaisePropertyChanged(nameof(OpticalSystem));
            }
        }
        [Required]
        [Range(50, 500000, ErrorMessage = "Apertura musi być w przedziale [50, 500 000] mm")]
        public int Aperture
        {
            get => telescope.Aperture;
            set
            {
                IsChanged = true;
                telescope.Aperture = value;
                RaisePropertyChanged(nameof(Aperture));
            }
        }

        [Required]
        [Range(50, 500000, ErrorMessage = "Ogniskowa musi być w przedziale [50, 500 000] mm")]
        public int FocalLength
        {
            get => telescope.FocalLength;
            set
            {
                IsChanged = true;
                telescope.FocalLength = value;
                RaisePropertyChanged(nameof(FocalLength));
            }
        }

        [Required]
        public Interfaces.IProducer Producer
        {
            get => telescope.Producer;
            set
            {
                IsChanged = true;
                telescope.Producer = value;
                RaisePropertyChanged(nameof(Producer));
            }
        }

        [Required(ErrorMessage = "Nazwa musi zostać nadana")]
        public string Name
        {
            get { return telescope.Name; }
            set
            {
                IsChanged = true;
                telescope.Name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        public int Id
        {
            get => telescope.Id;
            set
            {
                telescope.Id = value;
                RaisePropertyChanged(nameof(Id));
            }
        }


        private Dictionary<string, ICollection<string>> errorsCollection = new Dictionary<string, ICollection<string>>();

        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public bool HasErrors => errorsCollection.Count > 0;
        public IEnumerable GetErrors(string? propertyName)
        {
            if (string.IsNullOrEmpty(propertyName) || !errorsCollection.ContainsKey(propertyName)) return null;
            return errorsCollection[propertyName];
        }
        protected void RaiseErrorChanged(string propertyName)
        {
            if (ErrorsChanged != null)
            {
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
                RaisePropertyChanged(nameof(HasErrors));
            }
        }

        public void Validate()
        {
            var validationContext = new ValidationContext(this, null, null);
            var validationResults = new List<ValidationResult>();
            //wywołanie walidacji obiektu
            Validator.TryValidateObject(this, validationContext, validationResults, true);
            // usunięcie tych wpisów z kolekcji błędów, dla których już ich nie ma
            foreach (var kv in errorsCollection.ToList())
            {
                if (validationResults.All(r => r.MemberNames.All(m => m != kv.Key)))
                {
                    errorsCollection.Remove(kv.Key);
                    RaiseErrorChanged(kv.Key);
                }
            }
            var q = from result in validationResults
                    from member in result.MemberNames
                    group result by member into gr
                    select gr;
            foreach (var prop in q)
            {
                var messages = prop.Select(r => r.ErrorMessage).ToList();

                if (errorsCollection.ContainsKey(prop.Key))
                {
                    errorsCollection.Remove(prop.Key);
                }
                errorsCollection.Add(prop.Key, messages);
                RaiseErrorChanged(prop.Key);
            }
        }


        private bool isChanged;
        public bool IsChanged
        {
            get { return isChanged; }
            set
            {
                isChanged = value;
                RaisePropertyChanged(nameof(IsChanged));
            }
        }
    }
}
