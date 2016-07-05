using mine.core.Domain.Topics;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.data.Mapping.Topics
{
    public class TopicMap:EntityTypeConfiguration<Topic>
    {
        public TopicMap() 
        {
            this.ToTable("Topic");
            this.HasKey(t=>t.Id);

        }
    }
}
