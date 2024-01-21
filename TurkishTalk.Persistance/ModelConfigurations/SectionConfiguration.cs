using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TurkishTalk.Persistance.Models;


namespace TurkishTalk.Persistance.ModelConfigurations
{
    internal class SectionConfiguration : IEntityTypeConfiguration<Section>
    {
        public void Configure(EntityTypeBuilder<Section> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder.HasMany(x=>x.AlfabetTasks).WithOne(x=>x.Section);

            builder.HasMany(x=>x.ReadTasks).WithOne(x=>x.Section);

            builder.HasMany(x=>x.GrammarTasks).WithOne(x=>x.Section);

            builder.HasMany(x=>x.WriteTasks).WithOne(x=>x.Section);
        }
    }
}
