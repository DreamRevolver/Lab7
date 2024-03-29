﻿using Catalog.DAL.Entities;
using Catalog.DAL.Repositories.Impl;
using Catalog.DAL.Repositories.Interfaces;
using Catalog.DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;

namespace Catalog.DAL.EF
{
    public class EFUnitOfWork
        : IUnitOfWork
    {
        private CatalogContext db;
        private Repositories.Impl.CatalogRepository catalogRepository;
        private Repositories.Impl.restRepository restRepository;

        public EFUnitOfWork(CatalogContext context)
        {
            db = context;
        }
        public Repositories.Interfaces.CatalogRepository Catalogs
        {
            get
            {
                if (catalogRepository == null)
                    catalogRepository = new Repositories.Impl.CatalogRepository(db);
                return catalogRepository;
            }
        }

        public Repositories.Interfaces.restRepository rests
        {
            get
            {
                if (restRepository == null)
                    restRepository = new Repositories.Impl.restRepository(db);
                return restRepository;
            }
        }

        public void Save()
        {
            db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
