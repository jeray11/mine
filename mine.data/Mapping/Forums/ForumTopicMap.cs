using mine.core.Domain.Forums;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.data.Mapping.Forums
{
    public class ForumTopicMap:EntityTypeConfiguration<ForumTopic>
    {
        public ForumTopicMap() {
            this.ToTable("ForumTopic");
            this.HasKey(f=>f.Id);

            this.Property(f=>f.Subject).IsRequired().HasMaxLength(450);
            this.Ignore(ft => ft.ForumTopicType);
            this.HasRequired(f => f.Forum).WithMany().HasForeignKey(f=>f.ForumId);
            this.HasRequired(f => f.Customer).WithMany().HasForeignKey(f=>f.CustomerId).WillCascadeOnDelete(false);
        }
    }
}
