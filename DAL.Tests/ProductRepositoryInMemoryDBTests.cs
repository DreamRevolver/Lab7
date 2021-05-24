using System;
using Xunit;
using Catalog.DAL.Repositories.Impl;
using Catalog.DAL.EF;
using Microsoft.EntityFrameworkCore;
using Catalog.DAL.Entities;
using Catalog.DAL.Repositories.Interfaces;
using System.Linq;

namespace DAL.Tests
{
    public class restRepositoryInMemoryDBTests
    {
        public CatalogContext Context => SqlLiteInMemoryContext();

        private CatalogContext SqlLiteInMemoryContext()
        {

            var options = new DbContextOptionsBuilder<CatalogContext>()
                .UseSqlite("DataSource=:memory:")
                .Options;

            var context = new CatalogContext(options);
            context.Database.OpenConnection();
            context.Database.EnsureCreated();
            return context;
        }

        [Fact]
        public void Create_InputrestWithId0_SetrestId1()
        {
            // Arrange
            int expectedListCount = 6;
            var context = SqlLiteInMemoryContext();
            EFUnitOfWork uow = new EFUnitOfWork(context);
            Catalog.DAL.Repositories.Interfaces.restRepository repository = uow.rests;

            rest rest = new rest()
            {
                CatalogID = 6,
                Name = "testN",
                Description = "testD",
                Catalog = new Catalog.DAL.Entities.Catalog() { CatalogID = 6}
            };

   
            repository.Create(rest);
            uow.Save();
            var factListCount = context.rests.Count();

          
            Assert.Equal(expectedListCount, factListCount);
        }

        [Fact]
        public void Delete_InputExistrestId_Removed()
        {
            
            int expectedListCount = 0;
            var context = SqlLiteInMemoryContext();
            EFUnitOfWork uow = new EFUnitOfWork(context);
            Catalog.DAL.Repositories.Interfaces.restRepository repository = uow.rests;
            rest rest = new rest()
            {
              
                CatalogID = 6,
                Name = "testN",
                Description = "testD",
                Catalog = new Catalog.DAL.Entities.Catalog() { CatalogID = 6 }
            };
            context.rests.Add(rest);
            context.SaveChanges();

       
            repository.Delete(rest.restID);
            uow.Save();
            var factrestCount = context.rests.Count();

            
            Assert.Equal(expectedListCount, factrestCount);
        }

        [Fact]
        public void Get_InputExistrestId_Returnrest()
        {
        
            var context = SqlLiteInMemoryContext();
            EFUnitOfWork uow = new EFUnitOfWork(context);
            Catalog.DAL.Repositories.Interfaces.restRepository repository = uow.rests;
            rest expectedrest = new rest()
            {
                
                CatalogID = 6,
                Name = "testN",
                Description = "testD",
                Catalog = new Catalog.DAL.Entities.Catalog() { CatalogID = 6 }
            };
            context.rests.Add(expectedrest);
            context.SaveChanges();

            
            var factrest = repository.Get(expectedrest.restID);

            
            Assert.Equal(expectedrest, factrest);
        }
    }
}
