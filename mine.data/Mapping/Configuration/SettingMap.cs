using mine.core.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.data.Mapping.Configuration
{
    public class SettingMap : EntityTypeConfiguration<Setting>
    {
        public SettingMap()
        {
            this.ToTable("Setting");
            this.HasKey(item => item.Id);
            this.Property(item => item.Name).IsRequired().HasMaxLength(200);
            this.Property(item => item.Value).IsRequired().HasMaxLength(2000);
        }
    }
}
