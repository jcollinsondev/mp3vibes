using BatMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Services
{
    public interface IEntityMapperService
    {
        public TOut Map<TOut>(object entity);
        public IEnumerable<TOut> MapList<TOut>(IEnumerable<object> entityList);
    }

    public class EntityMapperService : IEntityMapperService
    {
        private MapConfiguration _mapper;

        public EntityMapperService(MapConfiguration mapper)
        {
            this._mapper = mapper;
        }

        public TOut Map<TOut>(object entity)
        {
            return this._mapper.Map<TOut>(entity);
        }

        public IEnumerable<TOut> MapList<TOut>(IEnumerable<object> entityList)
        {
            return entityList.Select(entity => this._mapper.Map<TOut>(entity));
        }
    }
}
