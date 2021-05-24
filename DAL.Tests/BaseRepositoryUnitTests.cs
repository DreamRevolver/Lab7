using System;
using Xunit;
using Catalog.DAL.Repositories.Impl;
using Catalog.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Catalog.DAL.Entities;
using Catalog.DAL.Repositories.Interfaces;
using System.Linq;
using Moq;

namespace DAL.Tests
{
    class TestrestRepository
        : BaseRepository<rest>
    {
        public TestrestRepository(DbContext context) 
            : base(context)
        {
        }
    }

    public class BaseRepositoryUnitTests
    {

        [Fact]
        public void Create_InputrestInstance_CalledAddMethodOfDBSetWithrestInstance()
        {
           
            DbContextOptions opt = new DbContextOptionsBuilder<CatalogContext>()
                .Options;
            var mockContext = new Mock<CatalogContext>(opt);
            var mockDbSet = new Mock<DbSet<rest>>();
            mockContext
                .Setup(context => 
                    context.Set<rest>(
                        ))
                .Returns(mockDbSet.Object);
            
            var repository = new TestrestRepository(mockContext.Object);

            rest expectedrest = new Mock<rest>().Object;

            
            repository.Create(expectedrest);

            
            mockDbSet.Verify(
                dbSet => dbSet.Add(
                    expectedrest
                    ), Times.Once());
        }

        [Fact]
        public void Delete_InputId_CalledFindAndRemoveMethodsOfDBSetWithCorrectArg()
        {
       
            DbContextOptions opt = new DbContextOptionsBuilder<CatalogContext>()
                .Options;
            var mockContext = new Mock<CatalogContext>(opt);
            var mockDbSet = new Mock<DbSet<rest>>();
            mockContext
                .Setup(context =>
                    context.Set<rest>(
                        ))
                .Returns(mockDbSet.Object);
         
            var repository = new TestrestRepository(mockContext.Object);

            rest expectedrest = new rest() { restID = 1};
            mockDbSet.Setup(mock => mock.Find(expectedrest.restID)).Returns(expectedrest);

         
            repository.Delete(expectedrest.restID);

        
            mockDbSet.Verify(
                dbSet => dbSet.Find(
                    expectedrest.restID
                    ), Times.Once());
            mockDbSet.Verify(
                dbSet => dbSet.Remove(
                    expectedrest
                    ), Times.Once());
        }

        [Fact]
        public void Get_InputId_CalledFindMethodOfDBSetWithCorrectId()
        {
  
            DbContextOptions opt = new DbContextOptionsBuilder<CatalogContext>()
                .Options;
            var mockContext = new Mock<CatalogContext>(opt);
            var mockDbSet = new Mock<DbSet<rest>>();
            mockContext
                .Setup(context =>
                    context.Set<rest>(
                        ))
                .Returns(mockDbSet.Object);

            rest expectedrest = new rest() { restID = 1 };
            mockDbSet.Setup(mock => mock.Find(expectedrest.restID))
                    .Returns(expectedrest);
            var repository = new TestrestRepository(mockContext.Object);

         
            var actualrest = repository.Get(expectedrest.restID);

            
            mockDbSet.Verify(
                dbSet => dbSet.Find(
                    expectedrest.restID
                    ), Times.Once());
            Assert.Equal(expectedrest, actualrest);
        }

      
    }
}
