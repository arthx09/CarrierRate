using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Repositories.Mapping;
using Entities.Models;

namespace Repositories
{
    public partial class CarrierRateContext : DbContext
    {
        static CarrierRateContext()
        {
            Database.SetInitializer<CarrierRateContext>(null);
        }

        public CarrierRateContext()
            : base("Name=DefaultConnection")
        {
        }

        public DbSet<tbCarrier> tbCarriers { get; set; }
        public DbSet<tbRate> tbRates { get; set; }
        public DbSet<tbUser> tbUsers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new tbCarrierMap());
            modelBuilder.Configurations.Add(new tbRateMap());
            modelBuilder.Configurations.Add(new tbUserMap());
        }
    }
}
