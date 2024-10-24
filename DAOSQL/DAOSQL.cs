using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOSQL
{
    public class DAOSQL : IDAO
    {
        public IProducer CreateNewProducer()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<ITelescope> GetAllTelescopes()
        {
            throw new NotImplementedException();
        }

        public void InsertNewProducer(IProducer p)
        {
            throw new NotImplementedException();
        }
        public void InsertNewTelescope(ITelescope t)
        {
            throw new NotImplementedException();
        }
    }
}
