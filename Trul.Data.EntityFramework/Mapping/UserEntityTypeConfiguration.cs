using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using Trul.Domain.Entities;

namespace Trul.Data.EntityFramework.Mapping
{
    public class UserEntityTypeConfiguration : EntityTypeConfiguration<User>
    {
        public UserEntityTypeConfiguration()
        {
            this.Property(p => p.ID).HasColumnName("UserID");

            this.HasMany(r => r.Roles).WithMany().Map(m =>
                {
                    m.ToTable("UserRole");
                    m.MapLeftKey("UserID");
                    m.MapRightKey("RoleID");
                }
            );
            this.ToTable("User");
        }
    }
}
