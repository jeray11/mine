using mine.core.Domain.Seo;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.data.Mapping.Seo
{
    public class UrlRecordMap:EntityTypeConfiguration<UrlRecord>
    {
        public UrlRecordMap() 
        {
            this.ToTable("UrlRecord");
            this.HasKey(u=>u.Id);
            this.Property(u=>u.EntityName).IsRequired().HasMaxLength(400);
            this.Property(u => u.Slug).IsRequired().HasMaxLength(400);
        }
    }
}
