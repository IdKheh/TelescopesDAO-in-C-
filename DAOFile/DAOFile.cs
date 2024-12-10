using Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Configuration;

namespace DAOFile
{
    public class DAOFile : IDAO
    {
        private List<ITelescope> listOfTelescopes;
        private List<IProducer> listOfProducers;

        private List<ITelescope> oldTelescopes;
        private List<IProducer> oldProducers;

        public DAOFile()
        {
            listOfProducers = new List<IProducer>();
            listOfTelescopes = new List<ITelescope>();
            #region loadConfigFile
            string[] content = LoadFromFile();
            bool isEndOfProducent = false;
            if (content != null)
            {
                foreach (string line in content)
                {
                    if (line.Contains("---")) { isEndOfProducent = true; }
                    else if (!isEndOfProducent)
                    {
                        string[] parts = line.Split(' ');
                        int.TryParse(parts[0], out int number);
                        listOfProducers.Add(new Producer() { Id = number, Name = parts[1] });
                        Console.WriteLine(listOfProducers[0].Name);
                    }
                    else
                    {
                        string[] parts = line.Split(' ');
                        int.TryParse(parts[0], out int id);
                        int.TryParse(parts[4], out int aperture);
                        int.TryParse(parts[5], out int focalLength);
                        OpticalSystem opticalSystem = (OpticalSystem)Enum.Parse(typeof(OpticalSystem), parts[3]);
                        Producer producer = null;

                        foreach (var p in listOfProducers)
                        {
                            if (p.Name.Equals(parts[2]))
                            {
                                producer = (Producer)p;
                                break;
                            }
                        }

                        listOfTelescopes.Add(new Telescope()
                        {
                            Id = id,
                            Name = parts[1],
                            Producer = producer,
                            OpticalSystem = opticalSystem,
                            Aperture = aperture,
                            FocalLength = focalLength
                        });
                    }
                }
            }
            #endregion
            oldProducers = listOfProducers;
            oldTelescopes = listOfTelescopes;
        }
        ~DAOFile()
        {
            SaveInFile();
            listOfProducers.Clear();
            listOfTelescopes.Clear();
        }

        public void AddProducer(IProducer producer)
        {
            Producer p = producer as Producer;
            listOfProducers.Add(p);
            SaveInFile();
        }

        public void AddTelescope(ITelescope telescope)
        {
            Telescope t = telescope as Telescope;
            listOfTelescopes.Add(t);
        }

        public IProducer CreateNewProducer()
        {
            return new Producer();
        }

        public ITelescope CreateNewTelescope()
        {
            return new Telescope();
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            return listOfProducers;
        }

        public IEnumerable<ITelescope> GetAllTelescopes()
        {
            return listOfTelescopes;
        }

        public void RemoveProducer(IProducer producer)
        {
            Producer p = producer as Producer;
            listOfProducers.Remove(p);
        }

        public void RemoveTelescope(ITelescope telescope)
        {
            Telescope t = telescope as Telescope;
            listOfTelescopes.Remove(t);
        }

        public void SaveChanges()
        {
            SaveInFile();
        }

        public void UpdateTelescope(ITelescope telescope)
        {
            
        }
        public void UpdateProducer(IProducer producer)
        {

        }
        public void UndoChanges()
        {
            listOfProducers = oldProducers;
            listOfTelescopes = oldTelescopes;
        }

        private void SaveInFile()
        {
            string file = ConfigurationManager.AppSettings["dbFile"];
            string content = "";

            foreach (var p in listOfProducers)
            {
                content += $"{p.Id} {p.Name}" + "\n";
            }
            content += "---\n";
            foreach (var t in listOfTelescopes)
            {
                content += $"{t.Id} {t.Name} {t.Producer.Name} {t.OpticalSystem} {t.Aperture} {t.FocalLength}" + "\n";
            }

            File.WriteAllText(file, content);
            Console.WriteLine("Writing to file...");
        }

        private string[] LoadFromFile()
        {
            string file = ConfigurationManager.AppSettings["dbFile"];
            if (File.Exists(file))
            {
                string[] content = File.ReadAllLines(file);
                return content;
            }
            else
            {
                throw new WarningException("Database File doesn't exist");
            }
        }
    }
}