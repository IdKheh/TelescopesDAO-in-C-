using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOSQL
{
    internal class DAOSQL : IDAO
    {
        public IProducer CreateNewProcucer()
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
    }
}
