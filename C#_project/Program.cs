using Interfaces;

namespace C__project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Interfaces.IDAO dao = new DAOSQL.DAOSQL();

            Console.WriteLine("Producers");
            foreach (Interfaces.IProducer p in dao.GetAllProducers())
            {
                Console.WriteLine($"{p.Id} {p.Name}");

            }

            Console.WriteLine("Telescopes");
            foreach (Interfaces.ITelescope c in dao.GetAllTelescopes())
            {
                Console.WriteLine($"{c.Id} {c.Name} {c.Producer.Name} {c.OpticalSystem} {c.Aperture} {c.FocalLength}");
            }

            foreach (Interfaces.IProducer p in dao.GetAllProducers())
            {
                Console.WriteLine($"{p.Id} {p.Name}");

            }
        }
    }
}
