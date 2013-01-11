using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity.ModelConfiguration;
using Trul.Domain.Entities;

namespace Trul.Data.EntityFramework.Mapping
{
    public class PersonEntityTypeConfiguration : EntityTypeConfiguration<Person>
    {
        public PersonEntityTypeConfiguration()
        {
            this.Property(p => p.ID).HasColumnName("PersonID");

            this.HasRequired(r => r.Country).WithMany().HasForeignKey(f => f.CountryID);

            this.ToTable("Person");
        }
    }
}
