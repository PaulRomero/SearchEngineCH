using SearchFlight.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace SearchFlight.Application.Services.Interfaces
{
    public interface IEngineApplication
    {
        List<Engine> GetAll();

    }
}
