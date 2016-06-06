using mine.core.Domain.Forums;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.data.Mapping.Forums
{
    public class ForumMap : EntityTypeConfiguration<Forum>
    {
        public ForumMap() 
        {
            this.ToTable("Forums_Forum");
            this.HasKey(item=>item.Id);
            this.Property(item=>item.Name).IsRequired().HasMaxLength(200);

            this.HasRequired(item => item.ForumGroup)
                .WithMany(fg => fg.Forums)
                .HasForeignKey(f=>f.ForumGroupId);
        }
    }
}
