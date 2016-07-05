using mine.core.Domain.Localization;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mine.data.Mapping.Localization
{
    public class LocalizedPropertyMap : EntityTypeConfiguration<LocalizedProperty>
    {
        public LocalizedPropertyMap()
        {
            this.ToTable("LocalizedProperty");
            this.HasKey(l=>l.Id);
            this.Property(l => l.LocaleKeyGroup).IsRequired().HasMaxLength(400);
            this.Property(l => l.LocaleKey).IsRequired().HasMaxLength(400);
            this.Property(l => l.LocaleValue).IsRequired();
            this.HasRequired(l => l.Language).WithMany().HasForeignKey(l=>l.LanguageId);
        }
    }
}
