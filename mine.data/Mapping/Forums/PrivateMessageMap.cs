using mine.core.Domain.Forums;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.data.Mapping.Forums
{
    public class PrivateMessageMap : EntityTypeConfiguration<PrivateMessage>
    {
        public PrivateMessageMap() 
        {
            this.ToTable("Forums_PrivateMessage");
            this.HasKey(p=>p.Id);

            this.Property(p => p.Subject).IsRequired().HasMaxLength(450);
            this.Property(p => p.Text).IsRequired();

            this.HasRequired(p => p.FromCustomer).WithMany().HasForeignKey(p=>p.FromCustomerId).WillCascadeOnDelete(false);
            this.HasRequired(p => p.ToCustomer).WithMany().HasForeignKey(p => p.ToCustomerId).WillCascadeOnDelete(false);
        }
    }
}
