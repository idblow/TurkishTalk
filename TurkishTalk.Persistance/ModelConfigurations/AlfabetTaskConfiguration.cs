using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TurkishTalk.Persistance.Models;

namespace TurkishTalk.Persistance.ModelConfigurations
{
    internal class AlfabetTaskConfiguration : IEntityTypeConfiguration<AlfabetTask>
    {
        public void Configure(EntityTypeBuilder<AlfabetTask> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.ProgressAlfabet).WithOne(x => x.AlfabetTask);
            builder.HasMany(x => x.WordDictionary).WithOne(x => x.AlfabetTask);
            builder.Property(x => x.Tests).HasJsonBase64Conversion();
        }
    }
}
