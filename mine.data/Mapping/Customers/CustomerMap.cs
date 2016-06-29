using mine.core.Domain.Customers;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.data.Mapping.Customers
{
   public class CustomerMap:EntityTypeConfiguration<Customer>
    {
       public CustomerMap() 
       {
           this.ToTable("Customer");
           this.HasKey(c=>c.Id);
           this.Property(u => u.Username).HasMaxLength(1000);
           this.Property(u => u.Email).HasMaxLength(1000);
           this.Property(u => u.SystemName).HasMaxLength(400);

           this.Ignore(u => u.PasswordFormat);

           this.HasMany(c => c.CustomerRoles).WithMany().Map(m => m.ToTable("Customer_CustomerRole_Mapping"));
       }
    }
}
