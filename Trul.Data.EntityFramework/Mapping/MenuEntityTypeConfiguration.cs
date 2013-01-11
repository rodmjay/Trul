using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using Trul.Domain.Entities;

namespace Trul.Data.EntityFramework.Mapping
{
    public class MenuEntityTypeConfiguration : EntityTypeConfiguration<Menu>
    {
        public MenuEntityTypeConfiguration() {

            this.Property(p => p.ID).HasColumnName("MenuID");
            //this.Property(p => p.ApplicationCode).HasColumnName("APPLICATION_CODE");
            //this.Property(p => p.LinkName).HasColumnName("LINK_ADI4");
            this.ToTable("Menu");
        }
    }
}
