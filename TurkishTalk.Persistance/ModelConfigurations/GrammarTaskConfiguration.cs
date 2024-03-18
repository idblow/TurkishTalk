using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TurkishTalk.Persistance.Models;

namespace TurkishTalk.Persistance.ModelConfigurations
{
    internal class GrammarTaskConfiguration : IEntityTypeConfiguration<GrammarTask>
    {
        public void Configure(EntityTypeBuilder<GrammarTask> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasMany(x => x.ProgresGrammars).WithOne(x => x.GrammarTask);
            builder.Property(x => x.Tests).HasJsonConversion();
            builder.Property(x => x.RadioTests).HasJsonConversion();
        }
    }
}
