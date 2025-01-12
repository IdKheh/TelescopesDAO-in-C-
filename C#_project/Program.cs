using Drozdzynski_Debowska.Telescopes.Interfaces;
using System.Configuration;
namespace Drozdzynski_Debowska.Telescopes.C__project
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string libraryName = ConfigurationManager.AppSettings["libraryFile"];
            Interfaces.IDAO dao = new BLC.BLC(libraryName).DAO;

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
            dao.CreateNewProducer();

            foreach (Interfaces.IProducer p in dao.GetAllProducers())
            {
                Console.WriteLine(p.ToString());

            }
        }
    }
}
