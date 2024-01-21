using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TurkishTalk.Persistance.Models;

namespace TurkishTalk.Persistance.ModelConfigurations
{
    internal class ProgressAlphabetConfiguration : IEntityTypeConfiguration<ProgressAlfabet>
    {
        public void Configure(EntityTypeBuilder<ProgressAlfabet> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.AlfabetTask).WithMany(x => x.ProgressAlfabet);
            builder.HasOne(x => x.User).WithMany(x => x.ProgressAlfabet);
        }
    }
}
