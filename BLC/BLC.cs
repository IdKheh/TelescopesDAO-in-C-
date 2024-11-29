using System.Reflection;

namespace BLC
{
    //to powinien być singleton
    public class BLC
    {
        private Interfaces.IDAO dao;

        public Interfaces.IDAO DAO => dao;

        public BLC(string libraryName)
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
