
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TurkishTalk.Persistance.Models;

namespace TurkishTalk.Persistance.ModelConfigurations
{
    internal class ProgressReadConfiguration: IEntityTypeConfiguration<ProgresRead>
    {
        public void Configure(EntityTypeBuilder<ProgresRead> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.ReadTask).WithMany(x=>x.ProgresRead);
            builder.HasOne(x => x.User).WithMany(x=>x.ProgresRead);
        }
    }
}
