using Interfaces;
using System.Xml.Linq;

namespace DAOMock
{
    public class DAOMock : Interfaces.IDAO
    {
        public List<ITelescope> listOfTelescopes;
        public List<IProducer> listOfProducers;

        #region konstruktory
        public DAOMock()
        {
            listOfProducers = new List<IProducer>();
            listOfProducers.Add(new Producer() { Id = 1, Name = "Celestron" });
            listOfProducers.Add(new Producer() { Id = 2, Name = "Sky-Watcher" });
            listOfProducers.Add(new Producer() { Id = 3, Name = "Taurus" });

            listOfTelescopes = new List<ITelescope>();
            listOfTelescopes.Add(new Telescope() { Id = 1, Name = "AstroMaster", Producer = listOfProducers[0], OpticalSystem = OpticalSystem.Newton, Aperture=130, FocalLength=650 });
            listOfTelescopes.Add(new Telescope() { Id = 2, Name = "Travel Scope", Producer = listOfProducers[0], OpticalSystem = OpticalSystem.Refractor, Aperture = 70, FocalLength = 400 });
            listOfTelescopes.Add(new Telescope() { Id = 3, Name = "BK MAK 127 EQ3-2", Producer = listOfProducers[1], OpticalSystem = OpticalSystem.MaksutovCassegrain, Aperture = 127, FocalLength = 1500 });
            listOfTelescopes.Add(new Telescope() { Id = 4, Name = "BKP 15075 EQ3-2", Producer = listOfProducers[1], OpticalSystem = OpticalSystem.Newton, Aperture = 150, FocalLength = 750 });
            listOfTelescopes.Add(new Telescope() { Id = 5, Name = "T300", Producer = listOfProducers[2], OpticalSystem = OpticalSystem.Newton, Aperture = 152, FocalLength = 1500 });
            listOfTelescopes.Add(new Telescope() { Id = 6, Name = "T500", Producer = listOfProducers[2], OpticalSystem = OpticalSystem.Newton, Aperture = 504, FocalLength = 2150 });
        }
        #endregion

        public IProducer CreateNewProducer()
        {
            return new Producer();
        }
        public void DeleteProducer(int id)
        {
            Producer p = (Producer)listOfProducers.Find(x=> x.Id == id);
            if (p == null)
            {
                throw new KeyNotFoundException();
            }
            else
            {
                listOfProducers.Remove(p);
            }
        }

        public void DeleteTelescope(int id)
        {
            Telescope t = (Telescope)listOfTelescopes.Find(x => x.Id == id);
            if (t == null)
            {
                throw new KeyNotFoundException();
            }
            else
            {
                listOfTelescopes.Remove(t);
            }
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            return listOfProducers;
        }

        public IEnumerable<ITelescope> GetAllTelescopes()
        {
            return listOfTelescopes;
        }

        public void InsertNewProducer(int id, string name)
        {
            listOfProducers.Add(new Producer() { Id = id, Name = name });
        }

        public void InsertNewTelescope(int id, string name, IProducer producer, OpticalSystem opticalSystem, int aperture, int focalLength)
        {
            listOfTelescopes.Add(new Telescope() { Id = id, Producer = producer, OpticalSystem = opticalSystem,
                Aperture = aperture, FocalLength = focalLength });  
        }

        public void ModifyProducer(int id, string name)
        {
            try
            {
                listOfProducers[id].Name = name;
            }
            catch { throw new KeyNotFoundException(); }

        }

        public void ModifyTelescope(int id, string name, IProducer producer, OpticalSystem opticalSystem, int aperture, int focalLength)
        {
            try
            {
                listOfTelescopes[id] = new Telescope() { Id = id, Name = name, Producer = (Producer)producer, 
                    OpticalSystem = opticalSystem, Aperture = aperture, FocalLength = focalLength };
            }
            catch { throw new KeyNotFoundException(); }
        }
    }
}
