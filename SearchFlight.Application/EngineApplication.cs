using SearchFlight.Application.Services.Interfaces;
using SearchFlight.Model;
using SearchFlight.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SearchFlight.Application.Services
{
    public class EngineApplication: IEngineApplication
    {
        private readonly IEngineRepository _engineRepository;

        public EngineApplication(IEngineRepository engineRepository)
        {
            this._engineRepository = engineRepository;
        }

        public List<Engine> GetAll()
        {
            return _engineRepository.GetAll();
        }
    }
}
