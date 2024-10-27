namespace Interfaces
{
    public interface IDAO
    {
        IProducer CreateNewProducer();
        IEnumerable<IProducer> GetAllProducers();
        IEnumerable<ITelescope> GetAllTelescopes();

        void InsertNewProducer(int id, string name);
        void InsertNewTelescope(int id, string name, IProducer producer, OpticalSystem opticalSystem, int aperture, int focalLength);

        void ModifyProducer(int id, string name);
        void ModifyTelescope(int id, string name, IProducer producer, OpticalSystem opticalSystem, int aperture, int focalLength);

        void DeleteProducer(int id);
        void DeleteTelescope(int id);
        

    }
}
