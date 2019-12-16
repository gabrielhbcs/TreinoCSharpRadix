using Grafos.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Grafos.Services.Interfaces
{
    public interface IDistanceService
    {
        Distancia achaMenorDistancia(Graph grafo, string town1, string town2);
    }
}
