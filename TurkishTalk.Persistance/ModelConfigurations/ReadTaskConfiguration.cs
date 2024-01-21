using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TurkishTalk.Persistance.Models;

namespace TurkishTalk.Persistance.ModelConfigurations
{
    internal class ReadTaskConfiguration: IEntityTypeConfiguration<ReadTask>
    {
        public void Configure(EntityTypeBuilder<ReadTask> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.ProgresRead).WithOne(x=>x.ReadTask);
        }
    }
}
