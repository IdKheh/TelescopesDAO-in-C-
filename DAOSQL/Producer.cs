namespace DAOSQL
{
    internal class Producer : Interfaces.IProducer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Telescope> Telescopes { get; set; }
    }
}
