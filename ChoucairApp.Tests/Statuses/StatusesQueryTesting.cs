using AutoMapper;
using ChoucairApp.Core.Application.CQRS.Queries;
using ChoucairApp.Core.Application.DTOs;
using ChoucairApp.Core.Application.Interfaces;
using ChoucairApp.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace ChoucairApp.Tests.Statuses
{
    public class StatusesQueryTesting
    {

        [Fact]
        public async Task Handle_ShouldReturnAllStatuses()
        {
            // Arrange
            var mockContext = new Mock<IApplicationDbContext>();
            var mockMapper = new Mock<IMapper>();

            // Datos de ejemplo
            var statuses = new List<StatusEntity>
        {
            new StatusEntity { ID = 1, Status_Description = "Abierta" },
            new StatusEntity { ID = 2, Status_Description = "Pendiente" },
            new StatusEntity { ID = 3, Status_Description = "Completada" }
        }.AsQueryable();

            var statusesDTO = new List<StatusesDTO>
        {
            new StatusesDTO { ID = 1, Description = "Abierta" },
            new StatusesDTO { ID = 2, Description = "Pendiente" },
            new StatusesDTO { ID = 3, Description = "Completada" }
            };

            var mockDbSet = new Mock<DbSet<StatusEntity>>();
            mockDbSet.As<IQueryable<StatusEntity>>().Setup(m => m.Provider).Returns(statuses.Provider);
            mockDbSet.As<IQueryable<StatusEntity>>().Setup(m => m.Expression).Returns(statuses.Expression);
            mockDbSet.As<IQueryable<StatusEntity>>().Setup(m => m.ElementType).Returns(statuses.ElementType);
            mockDbSet.As<IQueryable<StatusEntity>>().Setup(m => m.GetEnumerator()).Returns(statuses.GetEnumerator());

            mockContext.Setup(c => c.Statuses).Returns(mockDbSet.Object);
            mockMapper.Setup(m => m.Map<List<StatusesDTO>>(It.IsAny<List<StatusEntity>>())).Returns(statusesDTO);

            var handler = new GetAllStatusesQueryHandler(mockContext.Object, mockMapper.Object);
            var query = new GetAllStatusesQuery();

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.True(result.Succeeded);
            var statusList = result.Data.ToList();
            Assert.Equal(3, statusList.Count);
            Assert.Equal("Abierta", statusList[0].Description);
            Assert.Equal("Pendiente", statusList[1].Description);
            Assert.Equal("Completada", statusList[2].Description);
        }

    }
}
