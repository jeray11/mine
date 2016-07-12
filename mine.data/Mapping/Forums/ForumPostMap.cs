using mine.core.Domain.Forums;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.data.Mapping.Forums
{
    public class ForumPostMap:EntityTypeConfiguration<ForumPost>
    {
        public ForumPostMap() {
            this.ToTable("Forums_Post");
            this.HasKey(f=>f.Id);

            this.Property(f => f.Text).IsRequired();
            this.Property(f => f.IPAddress).IsRequired().HasMaxLength(100);
            this.HasRequired(f => f.Customer).WithMany().HasForeignKey(f=>f.CustomerId);
            this.HasRequired(f => f.ForumTopic).WithMany().HasForeignKey(f=>f.TopicId);
        }
    }
}
