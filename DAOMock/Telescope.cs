using Interfaces;

namespace DAOMock
{
    internal class Telescope : ITelescope
    {
        private int _id;
        private int _aperture;
        private int _focalLength;
        private string _name;

        public int Id
        {
            get => _id;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(Id), "Id cannot be less than 0!");
                }
                _id = value;
            }
        }
        public string Name
        {
            get => _name;
            set
            {
                if (value.Length < 3)
                {
                    throw new ArgumentOutOfRangeException(nameof(Name), "Name is too short!");
                }
                _name = value;
            }
        }
        public IProducer Producer { get; set; }
        public OpticalSystem OpticalSystem { get; set; }
        public int Aperture
        {
            get => _aperture;
            set
            {
                if (value < 40)
                {
                    throw new ArgumentOutOfRangeException(nameof(Aperture), "Aperture is too small.");
                }
                _aperture = value;
            }
        }
        public int FocalLength
        {
            get => _focalLength;
            set
            {
                if (value < 100)
                {
                    throw new ArgumentOutOfRangeException(nameof(FocalLength), "Focal Lenght is too small.");
                }
                _focalLength = value;
            }
        }
    }
}
