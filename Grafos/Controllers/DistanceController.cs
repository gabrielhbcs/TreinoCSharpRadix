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
    public class DistanceController : ControllerBase
    {
        private readonly IDistanceService _distanceService;
        private readonly IGraphService _graphService;
        private readonly ILogger _logger;
        public DistanceController(IDistanceService distanceService, IGraphService graphService, ILogger<DistanceController> logger){
            _distanceService = distanceService;
            _graphService = graphService;
            _logger = logger;
        }

        [HttpGet("{id}/from/{town1}/to/{town2}")]
        public async Task<ActionResult> GetActionAsync(int ID, string town1, string town2){
            _logger.LogInformation("Get {id} {town1} {town2}");
            RotaDistancia rota = new RotaDistancia();
            Graph grafo = new Graph();
            try{
                grafo = await _graphService.LoadGraph(ID);
            }catch{
                return BadRequest("ID inválido");
            }
            if (grafo == null)
                return NotFound("Grafo não encontrado");
            Distancia dist = new Distancia();
            dist = _distanceService.achaMenorDistancia(grafo, town1, town2);
            if(dist.distancia == -1)
                return Ok("Não existe rota");
            if(dist.distancia == 0)
                return Ok("Origem e destino iguais");

            return Ok(dist);
        }
    }
}
