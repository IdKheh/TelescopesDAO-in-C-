using Interfaces;

namespace DAOSQL
{
    public class DAOSQL : IDAO
    {
        public DAOSQL() { }

        public IProducer CreateNewProducer()
        {
            throw new NotImplementedException();
        }

        public void DeleteProducer(int id)
        {
            throw new NotImplementedException();
        }

        public void DeleteTelescope(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITelescope> GetAllTelescopes()
        {
            throw new NotImplementedException();
        }

        public void InsertNewProducer(int id, string name)
        {
            throw new NotImplementedException();
        }

        public void InsertNewTelescope(int id, string name, IProducer producer, OpticalSystem opticalSystem, int aperture, int focalLength)
        {
            throw new NotImplementedException();
        }

        public void ModifyProducer(int id, string name)
        {
            throw new NotImplementedException();
        }

        public void ModifyTelescope(int id, string name, IProducer producer, OpticalSystem opticalSystem, int aperture, int focalLength)
        {
            throw new NotImplementedException();
        }
    }
}
