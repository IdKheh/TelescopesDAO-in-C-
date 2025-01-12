using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Drozdzynski_Debowska.Telescopes.BLC
{
    
    public class BLC
    {
        private Interfaces.IDAO dao;

        public Interfaces.IDAO DAO => dao;

        public BLC(IConfiguration config)
        {
            string libraryName = config.GetValue<string>("libraryName");
            CreateDAO(libraryName);
        }

        public BLC(string libraryName)
        {
            CreateDAO(libraryName);
        }

        private void CreateDAO(string libraryName)
        {
            Assembly assembly = Assembly.UnsafeLoadFrom(libraryName);
            Type typeToCreate = null;

            foreach (Type t in assembly.GetTypes())
            {
                if (t.IsAssignableTo(typeof(Interfaces.IDAO)))
                {
                    typeToCreate = t;
                    break;
                }
            }
            dao = Activator.CreateInstance(typeToCreate) as Interfaces.IDAO;
        }
    }
}
