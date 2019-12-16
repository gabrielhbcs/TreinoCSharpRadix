using Grafos.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Grafos.Repository.Interfaces
{
    public interface IRouteRepository
    {
        List<Rota> achaRota(Graph grafo, string origem, string destino, int maxStops, int stops = 0, string ultimo = "");
    }
}
