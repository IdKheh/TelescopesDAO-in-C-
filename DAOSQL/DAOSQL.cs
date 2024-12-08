using Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAOSQL
{
    public class DAOSQL : DbContext, Interfaces.IDAO
    {
        private DbSet<Producer> Producers { get; set; }
        private DbSet<Telescope> Telescopes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Filename=D:\Dokumenty_Wiktorii\studia\rok4\sem7\C#\C#_project\DAOSQL\telescopesDB.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Telescope> ()
                .HasOne(c => c.Producer)
                .WithMany(p => p.Telescopes)
                .HasForeignKey(c => c.ProducerId)
                .IsRequired();
        }
        public void AddTelescope(ITelescope telescope)
        {
            Telescope t = telescope as Telescope;
            Telescopes.Add(t);
        }

        public void AddProducer(IProducer producer)
        {
            Producer p = producer as Producer;
            Producers.Add(p);
        }

        public ITelescope CreateNewTelescope()
        {
            return new Telescope();
        }

        public IProducer CreateNewProducer()
        {
            return new Producer();
        }

        public IEnumerable<ITelescope> GetAllTelescopes()
        {
            var cars = Telescopes.Include("Producer").ToList();
            return Telescopes;
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            return Producers;
        }

        public void RemoveTelescope(ITelescope telescope)
        {
            
        }

        public void RemoveProducer(IProducer producer)
        {
            throw new NotImplementedException();
        }

        public void UpdateTelescope(ITelescope telescope)
        {
            throw new NotImplementedException();
        }

        void IDAO.SaveChanges()
        {
            this.SaveChanges();
        }

        public void UndoChanges()
        {
            throw new NotImplementedException();
        }
    }
}
