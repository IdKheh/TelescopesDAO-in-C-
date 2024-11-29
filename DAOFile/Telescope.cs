using Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAOFile
{
    internal class Telescope : ITelescope
    {
        public int Id { get; set; }
        public Producer Producer { get; set; }
        public int ProducerId { get; set; }
        public string Name { get; set; }
        public OpticalSystem OpticalSystem { get; set; }
        public int Aperture { get; set; }
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
