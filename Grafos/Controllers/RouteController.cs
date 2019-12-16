using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Grafos.Services.Interfaces;
using Grafos.Models;
using System.Collections.Generic;

namespace Grafos.Controllers
{

    [Route("[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IRouteService _routeService;
        private readonly IGraphService _graphService;
        private readonly ILogger _logger;
        
        public RouteController (IRouteService routeService, IGraphService graphService, ILogger<RouteController> logger)
        {
            _routeService = routeService;
            _graphService = graphService;
            _logger = logger;
        }

        [HttpGet("{id}/from/{town1}/to/{town2}/maxStops/{maxStops}")]
        public async Task<ActionResult> GetActionAsync(int id, string town1, string town2, int maxStops){
            _logger.LogInformation("Get {ID} {town1} {town2} {maxStops}");
            Route route = new Route();
            List<Rota> rotas = new List<Rota>();
            Graph grafo = await _graphService.LoadGraph(id);
            rotas = _routeService.achaRota(grafo, town1, town2, maxStops);

            if(rotas == null){
                return NotFound();
            }
            RoutePrinted rotasPrinted = new RoutePrinted();
            rotasPrinted.routes = rotas;
            return Ok(rotasPrinted);
        }
    }
}
