using mine.core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.data.Mapping.Customers
{
   public class CustomerRoleMap:EntityTypeConfiguration<CustomerRole>
    {
       public CustomerRoleMap() 
       {
           this.ToTable("CustomerRole");
           this.HasKey(c=>c.Id);
           this.Property(u => u.Name).IsRequired().HasMaxLength(255);
           this.Property(u => u.FreeShipping).IsRequired();
           this.Property(u => u.TaxExempt).IsRequired();
           this.Property(u => u.Active).IsRequired();
           this.Property(u => u.IsSystemRole).IsRequired();
           this.Property(u => u.SystemName).HasMaxLength(255);
           this.Property(u => u.PurchasedWithProductId).IsRequired();
       }
    }
}
