using ChoucairApp.Core.Application.Interfaces;
using ChoucairApp.Core.Application.Models;
using ChoucairApp.Core.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace ChoucairApp.Infrastructure.Data
{
    public class ChoucairDbContext : IdentityDbContext<ApplicationUser>, IApplicationDbContext
    {
        public ChoucairDbContext(DbContextOptions<ChoucairDbContext> options) : base(options)
        {
        }
        public DbSet<TaskEntity> Tasks { get; set; }
        public DbSet<StatusEntity> Statuses { get; set; }

        public async Task<int> SaveChanges()
        {
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
