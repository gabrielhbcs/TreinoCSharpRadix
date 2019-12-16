using Grafos.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Grafos.Services.Interfaces
{
    public interface IRouteService
    {
        List<Rota> achaRota(Graph grafo, string origem, string destino, int maxStops);
    }
}
