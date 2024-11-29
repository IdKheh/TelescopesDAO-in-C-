using Interfaces;
using System.Collections.ObjectModel;

namespace DAOFile
{
    public class DAOFile : IDAO
    {
        private ObservableCollection<ITelescope> listOfTelescopes;
        private ObservableCollection<IProducer> listOfProducers;

        public DAOFile()
        {
            listOfProducers = new ObservableCollection<IProducer>();
            listOfTelescopes = new ObservableCollection<ITelescope>();
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
            this.SaveChanges();
        }

        public void UpdateTelescope(ITelescope telescope)
        {
            throw new NotImplementedException();
        }

        private void SaveInFile()
        {
            string file = "DbInFile.txt";
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
            string file = "DbInFile.txt";
            if (File.Exists(file))
            {
                string[] content = File.ReadAllLines(file);
                return content;
            }
            else
            {
                throw new FileNotFoundException(file);
            }
        }
    }
}