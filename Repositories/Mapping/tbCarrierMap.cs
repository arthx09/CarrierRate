using Entities.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Repositories.Mapping
{
    public class tbCarrierMap : EntityTypeConfiguration<tbCarrier>
    {
        public tbCarrierMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(100);

            this.Property(t => t.NickName)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("tbCarriers");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.Name).HasColumnName("Name");
            this.Property(t => t.NickName).HasColumnName("NickName");
            this.Property(t => t.Status).HasColumnName("Status");
            this.Property(t => t.Deleted).HasColumnName("Deleted");   
        }
    }
}
