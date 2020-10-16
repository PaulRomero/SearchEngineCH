using SearchFlight.Model;
using SearchFlight.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SearchFlight.Repositories
{
    public class EngineRepository: IEngineRepository
    {
        private readonly IEngineRepository _engineRepository;
        public EngineRepository(IEngineRepository engineRepository)
        {
            this._engineRepository = engineRepository;
        }
        public List<Engine> GetAll()
        {
            return DbContext.GetEngineList();
        }
    }
}
