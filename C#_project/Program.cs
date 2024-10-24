using Interfaces;

namespace C__project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Interfaces.IDAO dao = new DAOMock.DAOMock();

            Console.WriteLine("Producers");
            foreach (Interfaces.IProducer p in dao.GetAllProducers())
            {
                Console.WriteLine($"{p.Id} {p.Name}");

            }

            Console.WriteLine("Telescopes");
            foreach (Interfaces.ITelescope c in dao.GetAllTelescopes())
            {
                Console.WriteLine($"{c.Id} {c.Name} {c.OpticalSystem} {c.Aperture} {c.FocalLength}");
            }

            string nameProducer = Console.ReadLine();
            IProducer producer = dao.CreateNewProducer();
            producer.Name = nameProducer;
        }
    }
}
