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
            listOfProducers.Add(new Producer() { Id = 3, Name = "Takahashi" });

            listOfTelescopes = new List<ITelescope>();
            listOfTelescopes.Add(new Telescope() { Id = 1, Name = "Punto", Producer = listOfProducers[0], OpticalSystem = OpticalSystem.Newton });
            listOfTelescopes.Add(new Telescope() { Id = 2, Name = "125p", Producer = listOfProducers[0], OpticalSystem = OpticalSystem.Refractor });
            listOfTelescopes.Add(new Telescope() { Id = 3, Name = "W176", Producer = listOfProducers[1], OpticalSystem = OpticalSystem.MaksutovCassegrain });
            listOfTelescopes.Add(new Telescope() { Id = 4, Name = "W245", Producer = listOfProducers[1], OpticalSystem = OpticalSystem.Newton });
            listOfTelescopes.Add(new Telescope() { Id = 5, Name = "1201", Producer = listOfProducers[2], OpticalSystem = OpticalSystem.Newton });
            listOfTelescopes.Add(new Telescope() { Id = 6, Name = "Octavia", Producer = listOfProducers[2], OpticalSystem = OpticalSystem.Newton });
        }
        #endregion

        public IProducer CreateNewProcucer()
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
    }
}
