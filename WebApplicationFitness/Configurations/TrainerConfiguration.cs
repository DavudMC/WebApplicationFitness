using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApplicationFitness.Models;

namespace WebApplicationFitness.Configurations
{
    public class TrainerConfiguration : IEntityTypeConfiguration<Trainer>
    {
        public void Configure(EntityTypeBuilder<Trainer> builder)
        {
            builder.Property(x=>x.Name).IsRequired().HasMaxLength(256);
            builder.Property(x => x.Profession).IsRequired().HasMaxLength(256);
            builder.Property(x => x.Description).IsRequired(false).HasMaxLength(1024);
            builder.Property(x => x.ImagePath).IsRequired().HasMaxLength(1024);
        }
    }
}
