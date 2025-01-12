using Drozdzynski_Debowska.Telescopes.Interfaces;

namespace Drozdzynski_Debowska.Telescopes.DAOFile
{
    internal class Producer : IProducer
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
