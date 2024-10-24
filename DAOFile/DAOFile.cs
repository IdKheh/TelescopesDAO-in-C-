using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAOFile
{
    public class DAOFile : IDAO
    {
        public List<ITelescope> listOfTelescopes;
        public List<IProducer> listOfProducers;

        public IProducer CreateNewProducer()
        {
            return new Producer();
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            return listOfProducers;
        }

        public IEnumerable<ITelescope> GetAllTelescopes()
        {
            return listOfTelescopes;
        }

        public void InsertNewProducer(IProducer p)
        {
            throw new NotImplementedException();
        }

        public void InsertNewTelescope(ITelescope t)
        {
            throw new NotImplementedException();
        }

        public void SaveInFile()
        {
            string file = "newFile.txt";
        }
        public void LoadFromFile()
        {

        }
    }
}
