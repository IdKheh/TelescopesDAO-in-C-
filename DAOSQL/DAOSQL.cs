using Interfaces;
using System.Collections;
using System.Data.SQLite;
using System.Xml.Linq;

namespace DAOSQL
{
    public class DAOSQL : IDAO
    {
        private readonly string connectionString;
        public DAOSQL()
        {
            connectionString = "Data Source=telescope.db;Version=3;";
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string createTableProducer = @"
                    CREATE TABLE IF NOT EXISTS Producer (
                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                        Name TEXT NOT NULL
                    );";

                string createTableTelescope = @"
                    CREATE TABLE IF NOT EXISTS Telescopes (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Name TEXT NOT NULL,
                    ProducerId INTEGER,
                    OpticalSystem INTEGER,
                    Aperture INTEGER,
                    FocalLength INTEGER,
                    FOREIGN KEY (ProducerId) REFERENCES Producers(Id)
                );";

                using (var command = new SQLiteCommand(createTableProducer, connection))
                {
                    command.ExecuteNonQuery();
                }

                using (var command = new SQLiteCommand(createTableTelescope, connection))
                {
                    command.ExecuteNonQuery();
                }
                connection.Close();

                if (!GetAllProducers().Any())
                {
                    InsertNewProducer(1, "Celeston");
                    InsertNewProducer(2, "Sky-Watcher");
                    Producer p1 = new Producer() { Id=1, Name = "Celeston" };
                    Producer p2 = new Producer() { Id=2, Name = "Sky-Watcher" };

                    InsertNewTelescope(1, "AstroMaster", p1, OpticalSystem.Newton, 130, 650);
                    InsertNewTelescope(2, "Dobson", p2, OpticalSystem.Newton, 130, 650);
                }
            }
        }

        public IProducer CreateNewProducer()
        {
            return new Producer();
        }

        public void DeleteProducer(int id)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "Delete from Producer where Id = @id";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
        }

        public void DeleteTelescope(int id)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "Delete from Telescopes where Id = @id";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            List<IProducer> listOfProducers = new List<IProducer>() { };
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Producer";
                using (var command = new SQLiteCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        listOfProducers.Add(new Producer() { Id = Convert.ToInt32(reader["Id"]), Name = reader["Name"].ToString() });
                    }
                }
                connection.Close();
            }
            return listOfProducers;
        }

        public IEnumerable<ITelescope> GetAllTelescopes()
        {
            List<ITelescope> listOfTelescopes = new List<ITelescope>() { };
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = @"SELECT t.Id, t.Name, t.Aperture, t.FocalLength, t.OpticalSystem, 
                   p.Id as ProducerId, p.Name as ProducerName FROM Telescopes t JOIN Producer p ON t.ProducerId = p.Id";

                using (var command = new SQLiteCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = Convert.ToInt32(reader["Id"]);
                        int aperture = Convert.ToInt32(reader["Aperture"]);
                        int focalLength = Convert.ToInt32(reader["FocalLength"]);
                        OpticalSystem opticalSystem = (OpticalSystem)Convert.ToInt32(reader["OpticalSystem"]);

                        int producerId = Convert.ToInt32(reader["ProducerId"]);
                        string producerName = reader["ProducerName"].ToString();

                        var producer = new Producer { Id = producerId, Name = producerName };
                        var telescope = new Telescope { Id = id, Name = reader["Name"].ToString(), Producer = producer,
                            OpticalSystem = opticalSystem, Aperture = aperture, FocalLength = focalLength };

                        listOfTelescopes.Add(telescope);
                    }
                }
                connection.Close();
            }
            return listOfTelescopes;
        }

        public void InsertNewProducer(int id, string name)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Producer (Name) VALUES (@name)";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void InsertNewTelescope(int id, string name, IProducer producer, OpticalSystem opticalSystem, int aperture, int focalLength)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO Telescopes (Name, ProducerId, OpticalSystem, Aperture, FocalLength) " +
                    "VALUES (@name, @producer, @opt, @ape, @fl)";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@producer", producer.Id);
                    command.Parameters.AddWithValue("@opt", (int)opticalSystem);
                    command.Parameters.AddWithValue("@ape", aperture);
                    command.Parameters.AddWithValue("@fl", focalLength);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void ModifyProducer(int id, string name)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Producer SET Name = @name where Id = @id";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@name", name);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void ModifyTelescope(int id, string name, IProducer producer, OpticalSystem opticalSystem, int aperture, int focalLength)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string query = "UPDATE Telescopes SET Name = @name, ProducerId = @producer, OpticalSystem = @opt, Aperture = @ape, FocalLenght=@fl where Id = @id";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    command.Parameters.AddWithValue("@name", name);
                    command.Parameters.AddWithValue("@producer", producer.Id);
                    command.Parameters.AddWithValue("@opt", (int)opticalSystem);
                    command.Parameters.AddWithValue("@ape", aperture);
                    command.Parameters.AddWithValue("@fl", focalLength);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
