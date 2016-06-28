using mine.core.Domain.Stores;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.data.Mapping.Stores
{
    public class StoreMappingMap : EntityTypeConfiguration<StoreMapping>
    {
        public StoreMappingMap()
        {
            this.ToTable("StoreMapping");
            this.HasKey(s => s.Id);

            this.Property(s => s.EntityId).IsRequired();
            this.Property(s => s.EntityName).IsRequired().HasMaxLength(400);
            this.HasRequired(s => s.Store).WithMany().HasForeignKey(s=>s.StoreId);
        }
    }
}
