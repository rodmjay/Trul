using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using Trul.Domain.Entities;

namespace Trul.Data.EntityFramework.Mapping
{
    public class RoleEntityTypeConfiguration : EntityTypeConfiguration<Role>
    {
        public RoleEntityTypeConfiguration()
        {
            this.Property(p => p.ID).HasColumnName("RoleID");

            this.HasMany(r => r.Users).WithMany().Map(m =>
            {
               // m.ToTable("UserRole");
                m.MapLeftKey("RoleID");
               // m.MapRightKey("UserID");
            }
            );

            this.ToTable("Role");
        }
    }
}
