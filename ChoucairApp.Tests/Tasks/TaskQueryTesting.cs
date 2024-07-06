using ChoucairApp.Core.Application.CQRS.Commands;
using ChoucairApp.Core.Application.Interfaces;
using ChoucairApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ChoucairApp.Tests.Tasks
{
    public class TaskQueryTesting
    {
        [Fact]
        public async Task CompleteTaskHandler()
        {
            // Arrange
            var mockContext = new Mock<IApplicationDbContext>();
            var tasks = new List<TaskEntity>
            {
                new TaskEntity { ID = 1, StatusID = 1 }
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<TaskEntity>>();
            mockDbSet.As<IQueryable<TaskEntity>>().Setup(m => m.Provider).Returns(tasks.Provider);
            mockDbSet.As<IQueryable<TaskEntity>>().Setup(m => m.Expression).Returns(tasks.Expression);
            mockDbSet.As<IQueryable<TaskEntity>>().Setup(m => m.ElementType).Returns(tasks.ElementType);
            mockDbSet.As<IQueryable<TaskEntity>>().Setup(m => m.GetEnumerator()).Returns(tasks.GetEnumerator());

            mockContext.Setup(c => c.Tasks).Returns(mockDbSet.Object);

            var handler = new CompletedTaskCommandHandler(mockContext.Object);
            var command = new CompletedTaskCommand { Id = 1 };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.True(result.Succeeded);
            Assert.Equal(3, tasks.First().StatusID);
            mockContext.Verify(m => m.SaveChanges(), Times.Once());
        }

        [Fact]
        public async Task NoCompleteTaskHandler()
        {
            // Arrange
            var mockContext = new Mock<IApplicationDbContext>();
            var tasks = new List<TaskEntity>().AsQueryable();

            var mockDbSet = new Mock<DbSet<TaskEntity>>();
            mockDbSet.As<IQueryable<TaskEntity>>().Setup(m => m.Provider).Returns(tasks.Provider);
            mockDbSet.As<IQueryable<TaskEntity>>().Setup(m => m.Expression).Returns(tasks.Expression);
            mockDbSet.As<IQueryable<TaskEntity>>().Setup(m => m.ElementType).Returns(tasks.ElementType);
            mockDbSet.As<IQueryable<TaskEntity>>().Setup(m => m.GetEnumerator()).Returns(tasks.GetEnumerator());

            mockContext.Setup(c => c.Tasks).Returns(mockDbSet.Object);

            var handler = new CompletedTaskCommandHandler(mockContext.Object);
            var command = new CompletedTaskCommand { Id = 1 };

            // Act
            var result = await handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.False(result.Succeeded);
            Assert.Equal("La tarea no se pudo completar", result.Messages.First());
            mockContext.Verify(m => m.SaveChanges(), Times.Never());
        }
    }
}
