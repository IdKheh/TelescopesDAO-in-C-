using Interfaces;
using System.Xml.Linq;

namespace DAOMock
{
    internal class Producer : Interfaces.IProducer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Telescope> Telescopes { get; set; }

        public override string ToString()
        {
            return $"{Id}: {Name} ";
        }
    }
}
