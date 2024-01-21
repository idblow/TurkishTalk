using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TurkishTalk.Persistance.Models;

namespace TurkishTalk.Persistance.ModelConfigurations
{
    internal class WriteTaskConfiguration : IEntityTypeConfiguration<WriteTask>
    {
        public void Configure(EntityTypeBuilder<WriteTask> builder) 
        {
            builder.HasKey(x => x.Id);

            builder.HasMany(x => x.ProgresWrite).WithOne(x=>x.WriteTask);

            builder.HasMany(x => x.WordDictionary).WithOne(x=>x.WriteTask);
        }
    }
}
