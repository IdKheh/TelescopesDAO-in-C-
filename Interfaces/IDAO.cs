namespace Interfaces
{
    public interface IDAO
    {
        IEnumerable<IProducer> GetAllProducers();
        IEnumerable<ITelescope> GetAllTelescopes();

        IProducer CreateNewProcucer();
    }
}
