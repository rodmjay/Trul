using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using Trul.Domain.Entities;

namespace Trul.Data.Mapping
{
    public class CountryEntityTypeConfiguration : EntityTypeConfiguration<Country>
    {
        public CountryEntityTypeConfiguration()
        {
            this.Property(p => p.ID).HasColumnName("CountryID");
            this.ToTable("Country");
        }
    }
}
