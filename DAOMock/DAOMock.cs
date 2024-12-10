using Interfaces;
using System.Collections.ObjectModel;
using System.Xml.Linq;

namespace DAOMock
{
    public class DAOMock : Interfaces.IDAO
    {
        private List<ITelescope> listOfTelescopes;
        private List<IProducer> listOfProducers;

        private List<ITelescope> oldValuesTelescopes;
        private List<IProducer> oldValuesProducers;

        #region konstruktory
        public DAOMock()
        {
           
            listOfProducers = new List<IProducer>();
            listOfProducers.Add(new Producer() { Id = 1, Name = "Celestron" });
            listOfProducers.Add(new Producer() { Id = 2, Name = "Sky-Watcher" });
            listOfProducers.Add(new Producer() { Id = 3, Name = "Taurus" });

            listOfTelescopes = new List<ITelescope>();
            listOfTelescopes.Add(new Telescope() { Id = 1, Name = "AstroMaster", Producer = listOfProducers[0] as Producer, OpticalSystem = OpticalSystem.Newton, Aperture=130, FocalLength=650 });
            listOfTelescopes.Add(new Telescope() { Id = 2, Name = "Travel Scope", Producer = listOfProducers[0] as Producer, OpticalSystem = OpticalSystem.Refractor, Aperture = 70, FocalLength = 400 });
            listOfTelescopes.Add(new Telescope() { Id = 3, Name = "BK MAK 127 EQ3-2", Producer = listOfProducers[1] as Producer, OpticalSystem = OpticalSystem.MaksutovCassegrain, Aperture = 127, FocalLength = 1500 });
            listOfTelescopes.Add(new Telescope() { Id = 4, Name = "BKP 15075 EQ3-2", Producer = listOfProducers[1] as Producer, OpticalSystem = OpticalSystem.Newton, Aperture = 150, FocalLength = 750 });
            listOfTelescopes.Add(new Telescope() { Id = 5, Name = "T300", Producer = listOfProducers[2] as Producer, OpticalSystem = OpticalSystem.Newton, Aperture = 152, FocalLength = 1500 });
            listOfTelescopes.Add(new Telescope() { Id = 6, Name = "T500", Producer = listOfProducers[2] as Producer, OpticalSystem = OpticalSystem.Newton, Aperture = 504, FocalLength = 2150 });

            oldValuesProducers = listOfProducers;
            oldValuesTelescopes = listOfTelescopes;
        }
        #endregion

        public void AddProducer(IProducer producer)
        {
            Producer p = producer as Producer;
            listOfProducers.Add(p);
        }

        public void AddTelescope(ITelescope telescope)
        {
            Telescope t = telescope as Telescope;
            listOfTelescopes.Add(t);
        }

        public IProducer CreateNewProducer()
        {
            return new Producer();
        }

        public ITelescope CreateNewTelescope()
        {
            return new Telescope();
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            return listOfProducers;
        }

        public IEnumerable<ITelescope> GetAllTelescopes()
        {
            return listOfTelescopes;
        }

        public void RemoveProducer(IProducer producer)
        {
            Producer p = producer as Producer;
            listOfProducers.Remove(p);
        }

        public void RemoveTelescope(ITelescope telescope)
        {
            Telescope t = telescope as Telescope;
            listOfTelescopes.Remove(t);
        }

        public void SaveChanges()
        {

        }

        public void UndoChanges()
        {
            listOfProducers = oldValuesProducers;
            listOfTelescopes = oldValuesTelescopes;
        }

        public void UpdateProducer(IProducer producer)
        {
            
        }

        public void UpdateTelescope(ITelescope telescope)
        {

        }
    }
}
