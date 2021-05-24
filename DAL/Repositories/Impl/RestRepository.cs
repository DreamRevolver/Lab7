using System;
using System.Collections.Generic;
using System.Text;
using Catalog.DAL.Entities;
using Catalog.DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Catalog.DAL.EF;
using System.Linq;

namespace Catalog.DAL.Repositories.Impl
{
    public class restRepository
        : BaseRepository<rest>, Interfaces.restRepository
    {
        internal restRepository(CatalogContext context) 
            : base(context)
        {
        }
    }
}
