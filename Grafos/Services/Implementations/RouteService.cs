using System.Threading.Tasks;
using Grafos.Services.Interfaces;
using Grafos.Models;
using Grafos.Repository.Interfaces;
using System.Collections.Generic;

namespace Grafos.Services.Implementations
{
    public class RouteService : IRouteService
    {
        private readonly IRouteRepository _routeRepository;
        public RouteService(IRouteRepository routeRepository){
            _routeRepository = routeRepository;
        }

        public List<Rota> achaRota(Graph grafo, string origem, string destino, int maxStops){
            return _routeRepository.achaRota(grafo, origem, destino, maxStops);
        }
    }
}
