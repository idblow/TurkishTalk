using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TurkishTalk.Persistance.Models;

namespace TurkishTalk.Persistance.ModelConfigurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Role).IsRequired();
            
            builder.HasMany(x => x.ProgresWrite).WithOne(x => x.User);

           
            builder.HasMany(x=>x.ProgresGrammar).WithOne(x => x.User);

            
            builder.HasMany(x=>x.ProgressAlfabet).WithOne(x => x.User);

           
            builder.HasMany(x=>x.ProgresRead).WithOne(x => x.User);
        }
    }
}
