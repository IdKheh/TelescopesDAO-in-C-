namespace Drozdzynski_Debowska.Telescopes.Interfaces
{
    public interface ITelescope
    {
        int Id { get; set; }
        string Name { get; set; }
        IProducer Producer { get; set; }
        OpticalSystem OpticalSystem { get; set; }
        int Aperture { get; set; }
        int FocalLength { get; set; }
    }
}
