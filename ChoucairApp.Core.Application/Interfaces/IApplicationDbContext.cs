using ChoucairApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChoucairApp.Core.Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<TaskEntity> Tasks { get; set; }
        DbSet<StatusEntity> Statuses { get; set; }

        Task<int> SaveChanges();
    }
}
