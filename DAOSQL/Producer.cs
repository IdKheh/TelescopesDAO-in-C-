using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Drozdzynski_Debowska.Telescopes.DAOSQL
{
    public class Producer : Interfaces.IProducer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Telescope> Telescopes { get; set; }
    }
}
