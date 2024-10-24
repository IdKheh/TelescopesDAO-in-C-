namespace Interfaces
{
    public interface IDAO
    {
        IProducer CreateNewProducer();
        IEnumerable<IProducer> GetAllProducers();
        IEnumerable<ITelescope> GetAllTelescopes();

        void InsertNewProducer(IProducer p);
        void InsertNewTelescope(ITelescope t);

    }
}
