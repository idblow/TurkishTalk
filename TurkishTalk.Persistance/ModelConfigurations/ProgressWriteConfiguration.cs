
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TurkishTalk.Persistance.Models;

namespace TurkishTalk.Persistance.ModelConfigurations
{
    internal class ProgressWriteConfiguration: IEntityTypeConfiguration<ProgresWrite>
    {
        public void Configure(EntityTypeBuilder<ProgresWrite> builder)
        {
            builder.HasKey(x => x.Id);
          //  builder.HasOne(x => x.WriteTask).WithMany(x => x.ProgresWrite);
           // builder.HasOne(x => x.User).WithOne(x => x.ProgresWrite);
        }

    }
}
