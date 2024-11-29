using Interfaces;

namespace DAOFile
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
