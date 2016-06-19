using Entities.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Repositories.Mapping
{
    public class tbUserMap : EntityTypeConfiguration<tbUser>
    {
        public tbUserMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.User)
                .IsRequired()
                .HasMaxLength(50);

            // Table & Column Mappings
            this.ToTable("tbUser");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.User).HasColumnName("User");
            this.Property(t => t.Password).HasColumnName("Password");
        }
    }
}
