using SearchFlight.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SearchFlight.Repositories.Interfaces
{
    public interface IEngineRepository
    {
        List<Engine> GetAll();
    }
}
