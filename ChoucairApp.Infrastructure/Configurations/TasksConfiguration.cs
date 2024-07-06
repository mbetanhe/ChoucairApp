using ChoucairApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ChoucairApp.Infrastructure.Configurations
{
    public class TasksConfiguration : IEntityTypeConfiguration<TaskEntity>
    {
        public void Configure(EntityTypeBuilder<TaskEntity> entity)
        {
            entity.ToTable("Tasks", "system");

            entity.HasKey(e => e.ID);

            entity.Property(e => e.ID).HasColumnName("Task_ID");

            entity.Property(e => e.Task_Title).HasColumnName("Task_Title");
            entity.Property(e => e.Task_Description).HasColumnName("Task_Description");
            entity.Property(e => e.Task_EndDate).HasColumnName("Task_EndDate");
            entity.Property(e => e.Task_StartDate).HasColumnName("Task_StartDate");
            entity.Property(e => e.StatusID).HasColumnName("StatusID");
            entity.Property(e => e.UserID).HasColumnName("UserID");
        }
    }
}
