namespace Interfaces
{
    public interface IDAO
    {
        IEnumerable<IProducer> GetAllProducers();
        IEnumerable<ITelescope> GetAllTelescopes();
        IProducer CreateNewProducer();
        ITelescope CreateNewTelescope();
        void AddTelescope(ITelescope telescope);
        void RemoveTelescope(ITelescope telescope);
        void AddProducer(IProducer producer);
        void RemoveProducer(IProducer producer);
        void UpdateTelescope(ITelescope telescope);
        void SaveChanges();
        void UndoChanges();
        void UpdateProducer(IProducer producer);
    }
}
