using BLL.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using BLL.DTO;

namespace BLL.Services.Impl
{
    public class TableService
        : ITableService
    {
        private readonly IUnitOfWork _database;
        private int pageSize = 10;
​
        public TableService(
            IUnitOfWork unitOfWork)
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException(nameof(unitOfWork));
            }
            _database = unitOfWork;
        }
​
        /// <exception cref="MethodAccessException"></exception>
        public IEnumerable<TablesDTO> GetStreets(int pageNumber)
        {
            var user = SecurityContext.GetUser();
            var userType = user.GetType();
            if (userType != typeof(Admin)
                && userType != typeof(Client))
            {
                throw new MethodAccessException();
            }
            var osbbId = user.OSBBID;
            var streetsEntities =
                _database
                    .Streets
                    .Find(z => z.OSBBID == osbbId, pageNumber, pageSize);
            var mapper =
                new MapperConfiguration(
                    cfg => cfg.CreateMap<Table, TablesDTO>()
                    ).CreateMapper();
            var streetsDto =
                mapper
                    .Map<IEnumerable<Table>, List<TablesDTO>>(
                        streetsEntities);
            return streetsDto;
        }
    }
}
