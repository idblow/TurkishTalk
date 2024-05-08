using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TurkishTalk.Persistance.Models;

namespace TurkishTalk.Persistance.ModelConfigurations
{
    internal class WordDictionaryConfiguration: IEntityTypeConfiguration<WordDictionary>
    {
        public void Configure(EntityTypeBuilder<WordDictionary> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.WriteTask).WithMany(x => x.WordDictionary);
            builder.HasOne(x => x.AlfabetTask).WithMany(x => x.WordDictionary);

            builder.Property(p => p.Word).HasBase64Conversion();
        }
    }
}
