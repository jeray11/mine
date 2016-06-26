using mine.core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.data.Mapping.Localization
{
   public class LocaleStringResourceMap: EntityTypeConfiguration<LocaleStringResource>
    {
        public LocaleStringResourceMap()
        {
            this.ToTable("LocaleStringResource");
            this.HasKey(l=>l.Id);
            this.Property(l => l.ResourceName).IsRequired().HasMaxLength(200);
            this.Property(l => l.ResourceValue).IsRequired();

            this.HasRequired(l => l.Language).WithMany(l=>l.LocaleStringResources).HasForeignKey(l => l.LanguageId);
        }
    }
}
