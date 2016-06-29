using mine.core.Domain.Security;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.data.Mapping.Security
{
    public class PermissionRecordMap : EntityTypeConfiguration<PermissionRecord>
    {
        public PermissionRecordMap() 
        {
            this.ToTable("PermissionRecord");
            this.HasKey(p=>p.Id);
            this.Property(p => p.Name).IsRequired();
            this.Property(p=>p.SystemName).IsRequired().HasMaxLength(255);
            this.Property(p => p.Category).IsRequired().HasMaxLength(255);

            this.HasMany(p => p.CustomerRoles).WithMany(c => c.PermissionRecords).Map(m => m.ToTable("PermissionRecord_Role_Mapping"));
        }
    }
}
