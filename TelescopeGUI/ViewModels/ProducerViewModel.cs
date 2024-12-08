using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelescopeGUI.ViewModels
{
    public class ProducerViewModel : INotifyPropertyChanged, INotifyDataErrorInfo
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private Interfaces.IProducer producer;
        public Interfaces.IProducer Producer => producer;

        public ProducerViewModel(Interfaces.IProducer producer)
        {
            this.producer = producer;
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

        [Required(ErrorMessage = "Nazwa musi zostać nadana")]
        public string Name
        {
            get { return producer.Name; }
            set
            {
                IsChanged = true;
                producer.Name = value;
                RaisePropertyChanged(nameof(Name));
            }
        }

        public int Id
        {
            get => producer.Id;
            set
            {
                producer.Id = value;
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
