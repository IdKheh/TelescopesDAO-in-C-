using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOMock
{
    internal class Telescope : ITelescope
    {
        public int Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public IProducer Producer { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public OpticalSystem OpticalSystem { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Aperture { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int FocalLength { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
