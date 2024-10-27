using Interfaces;

namespace DAOMock
{
    internal class Producer : Interfaces.IProducer
    {
        private int _id;
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
    }
}
