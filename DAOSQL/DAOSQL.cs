using Drozdzynski_Debowska.Telescopes.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace Drozdzynski_Debowska.Telescopes.DAOSQL
{
    public class DAOSQL : DbContext, Interfaces.IDAO
    {
        public DbSet<Producer> Producers { get; set; }
        public DbSet<Telescope> Telescopes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Filename=D:\PUT\VII semestr\Programowanie Wizualne\TelescopesDAO-in-C-\TelescopeGUI\app.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Telescope>()
                .HasOne(t => t.Producer)
                .WithMany(p => p.Telescopes)
                .HasForeignKey(t => t.ProducerId)
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

        public ITelescope CreateNewTelescope() => new Telescope();

        public IProducer CreateNewProducer()
        {
            Producer producer = new Producer();
            producer.Telescopes = new List<Telescope>();
            return producer;
        }

        public IEnumerable<ITelescope> GetAllTelescopes()
        {
            return Telescopes.Include("Producer").ToList();
        }

        public IEnumerable<IProducer> GetAllProducers()
        {
            using (var context = new DAOSQL())
            {
                context.Database.EnsureCreated();
            }

            return Producers.ToList();
        }

        public void RemoveTelescope(ITelescope telescope)
        {
            if (telescope is Telescope t)
            {
                Telescopes.Remove(t);
            }
        }

        public void RemoveProducer(IProducer producer)
        {
            if (producer is Producer p)
            {
                Producers.Remove(p);
            }
        }

        public void UpdateTelescope(ITelescope telescope)
        {
            if (telescope is Telescope t)
            {
                var entity = Telescopes.Find(t.Id);
                if (entity != null)
                {
                    Entry(entity).CurrentValues.SetValues(t);
                }
            }
        }

        public void UpdateProducer(IProducer producer)
        {
            if (producer is Producer p)
            {
                var entity = Producers.Find(p.Id);
                if (entity != null)
                {
                    Entry(entity).CurrentValues.SetValues(p);
                }
            }
        }

        void IDAO.SaveChanges()
        {
            this.SaveChanges();
        }

        public void UndoChanges()
        {
            foreach (var entry in this.ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Modified)
                {
                    entry.State = EntityState.Unchanged;
                }
            }
            this.SaveChanges();
        }
    }
}
