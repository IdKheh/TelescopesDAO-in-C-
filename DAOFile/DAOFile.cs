using Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DAOFile
{
    public class DAOFile : IDAO
    {
        public List<ITelescope> listOfTelescopes;
        public List<IProducer> listOfProducers;

        public DAOFile()
        {
            listOfProducers = new List<IProducer>();
            listOfTelescopes = new List<ITelescope>();
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

                        foreach(var p in listOfProducers)
                        {
                            if (p.Name.Equals(parts[2]))
                            {
                                producer = (Producer)p;
                                break;
                            }
                        }

                        listOfTelescopes.Add(new Telescope() { Id = id,Name = parts[1], Producer = producer,
                            OpticalSystem= opticalSystem, Aperture = aperture, FocalLength = focalLength});
                    }
                }
            }
        }
        ~DAOFile()
        {
            SaveInFile();
            listOfProducers.Clear();
            listOfTelescopes.Clear();
        }

        public IProducer CreateNewProducer()
        {
            return new Producer();
        }

        public void DeleteProducer(int id)
        {
            Producer p = (Producer)listOfProducers.Find(x => x.Id == id);
            if (p == null)
            {
                throw new KeyNotFoundException();
            }
            else
            {
                listOfProducers.Remove(p);
            }
        }

        public void DeleteTelescope(int id)
        {
            Telescope t = (Telescope)listOfTelescopes.Find(x => x.Id == id);
            if (t == null)
            {
                throw new KeyNotFoundException();
            }
            else
            {
                listOfTelescopes.Remove(t);
            }
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            return listOfProducers;
        }

        public IEnumerable<ITelescope> GetAllTelescopes()
        {
            return listOfTelescopes;
        }

        public void InsertNewProducer(int id, string name)
        {
            listOfProducers.Add(new Producer() { Id = id, Name = name });
        }

        public void InsertNewTelescope(int id, string name, IProducer producer, OpticalSystem opticalSystem, int aperture, int focalLength)
        {
            listOfTelescopes.Add(new Telescope()
            {
                Id = id,
                Producer = producer,
                OpticalSystem = opticalSystem,
                Aperture = aperture,
                FocalLength = focalLength
            });
        }

        public void ModifyProducer(int id, string name)
        {
            try
            {
                listOfProducers[id].Name = name;
            }
            catch { throw new KeyNotFoundException(); }

        }

        public void ModifyTelescope(int id, string name, IProducer producer, OpticalSystem opticalSystem, int aperture, int focalLength)
        {
            try
            {
                listOfTelescopes[id] = new Telescope()
                {
                    Id = id,
                    Name = name,
                    Producer = (Producer)producer,
                    OpticalSystem = opticalSystem,
                    Aperture = aperture,
                    FocalLength = focalLength
                };
            }
            catch { throw new KeyNotFoundException(); }
        }

        private void SaveInFile()
        {
            string file = "DbInFile.txt";
            string content = "";

            foreach (var p in listOfProducers) {
                content += $"{p.Id} {p.Name}" + "\n";
            }
            content += "---\n";
            foreach (var t in listOfTelescopes) {
                content += $"{t.Id} {t.Name} {t.Producer.Name} {t.OpticalSystem} {t.Aperture} {t.FocalLength}" + "\n";
            }

            File.WriteAllText(file,content);
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
            else { 
                throw new FileNotFoundException(file);
            }
        }
    }
}