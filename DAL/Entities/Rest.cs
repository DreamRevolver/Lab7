using System;
using System.Collections.Generic;
using System.Text;

namespace Catalog.DAL.Entities
{
    public class rest
    {
        public int restID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int CatalogID { get; set; }
        public Catalog Catalog { get; set; }
    }
}
