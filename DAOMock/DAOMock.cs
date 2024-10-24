using Interfaces;

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

        public IEnumerable<IProducer> GetAllProducers()
        {
            return listOfProducers;
        }

        public IEnumerable<ITelescope> GetAllTelescopes()
        {
            return listOfTelescopes;
        }

        public void InsertNewProducer(IProducer p)
        {
           listOfProducers.Add(p);
        }

        public void InsertNewTelescope(ITelescope t)
        {
            listOfTelescopes.Add(t);
        }
    }
}
