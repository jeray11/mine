using mine.core.Domain.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.data.Mapping.Common
{
    public class GenericAttributeMap : EntityTypeConfiguration<GenericAttribute>
    {
        public GenericAttributeMap() 
        {
            this.ToTable("GenericAttribute");
            this.HasKey(g=>g.Id);
            this.Property(g => g.EntityId).IsRequired();
            this.Property(g => g.KeyGroup).IsRequired().HasMaxLength(400);
            this.Property(g => g.Key).IsRequired().HasMaxLength(400);
            this.Property(g => g.Value).IsRequired();
            this.Property(g => g.StoreId).IsRequired();
        }
    }
}
