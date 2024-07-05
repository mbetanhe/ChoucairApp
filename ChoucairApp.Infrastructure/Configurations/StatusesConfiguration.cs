using ChoucairApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChoucairApp.Infrastructure.Configurations
{
    public class StatusesConfiguration : IEntityTypeConfiguration<StatusEntity>
    {
        public void Configure(EntityTypeBuilder<StatusEntity> entity)
        {
            entity.ToTable("Status", "system");

            entity.HasKey(e => e.ID);

            entity.Property(e => e.ID).HasColumnName("Status_ID");

            entity.Property(e => e.Status_Description).HasColumnName("Status_Description");
        }
    }
}
