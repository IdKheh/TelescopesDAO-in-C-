using Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace DAOSQL
{
    public class DAOSQL : DbContext, Interfaces.IDAO
    {
        private DbSet<Producer> Producers { get; set; }
        private DbSet<Telescope> Telescopes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=app.db;");
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
            if (telescope is Telescope t)
            {
                Telescopes.Add(t);
            }
        }

        public void AddProducer(IProducer producer)
        {
            if (producer is Producer p)
            {
                Producers.Add(p);
            }
        }

        public ITelescope CreateNewTelescope() => new Telescope();

        public IProducer CreateNewProducer() => new Producer();

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
