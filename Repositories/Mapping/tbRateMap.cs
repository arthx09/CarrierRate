using Entities.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Repositories.Mapping
{
    public class tbRateMap : EntityTypeConfiguration<tbRate>
    {
        public tbRateMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            // Table & Column Mappings
            this.ToTable("tbRates");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IdCarrier).HasColumnName("IdCarrier");
            this.Property(t => t.Rate).HasColumnName("price");
            this.Property(t => t.IdUser).HasColumnName("IdUser");

            // Relationships
            this.HasRequired(t => t.tbCarrier)
                .WithMany(t => t.tbRates)
                .HasForeignKey(d => d.IdCarrier);
            this.HasRequired(t => t.tbUser)
                .WithMany(t => t.tbRates)
                .HasForeignKey(d => d.IdUser);

        }
    }
}
