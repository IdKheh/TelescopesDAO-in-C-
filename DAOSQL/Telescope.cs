using Drozdzynski_Debowska.Telescopes.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Drozdzynski_Debowska.Telescopes.DAOSQL
{
    public class Telescope : ITelescope
    {
        public int Id { get; set; }
        public Producer Producer { get; set; }
        public int ProducerId { get; set; }
        [Required(ErrorMessage = "Nazwa musi zostać nadana")]
        public string Name { get; set; }
        [Required]
        public OpticalSystem OpticalSystem { get; set; }
        [Required]
        [Range(50, 500000, ErrorMessage = "Apertura musi być w przedziale [50, 500 000] mm")]
        public int Aperture { get; set; }
        [Required]
        [Range(50, 500000, ErrorMessage = "Ogniskowa musi być w przedziale [50, 500 000] mm")]
        public int FocalLength { get; set; }

        [NotMapped]
        IProducer ITelescope.Producer
        {
            get => this.Producer;
            set
            {
                this.Producer = value as Producer;
                this.ProducerId = value.Id;
            }
        }
    }
}
