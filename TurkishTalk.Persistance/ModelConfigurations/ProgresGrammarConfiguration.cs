

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TurkishTalk.Persistance.Models;

namespace TurkishTalk.Persistance.ModelConfigurations
{
    internal class ProgresGrammarConfiguration: IEntityTypeConfiguration<ProgresGrammar>
    {
        public void Configure(EntityTypeBuilder<ProgresGrammar> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.GrammarTask).WithMany(x => x.ProgresGrammars);
            builder.HasOne(x => x.User).WithMany(x => x.ProgresGrammar);
        }
    }
}
