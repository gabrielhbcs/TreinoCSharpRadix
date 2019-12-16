using System.Threading.Tasks;
using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Grafos.Services.Interfaces;
using Grafos.Models;

namespace Grafos.Controllers
{
    /**
    * Classe que controla as chamadas de operação em gráfos.
    */
    
    [Route("[controller]")]
    [ApiController]
    public class GraphController : ControllerBase
    {

        private readonly IGraphService _graphService;
        private readonly ILogger _logger;

        public GraphController (IGraphService graphService, ILogger<GraphController> logger)
        {
            _graphService = graphService;
            _logger = logger;
        }
        // recupera um grafo pelo ID
        [HttpGet("{id}")]
         public async Task<ActionResult> GetAsync(int id)
        {
            //TODO: Implementar comportamento e retorno apropriado usando _graphService.
             _logger.LogInformation("Get {ID}");
            Graph graph = new Graph();
            graph = null;

            try {
                graph = await _graphService.LoadGraph(id);
            } catch {
                return BadRequest();
            }
            if (graph == null) { return NotFound(); }
            
            return Ok(graph);
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody] Graph graph)
        {
            //TODO: Implementar comportamento e retorno apropriado usando _graphService
            int id = await _graphService.SaveGraph(graph);
            if (id == 0)
                return BadRequest();            
            return CreatedAtAction("PostAsync",graph);
        }
       
    }

}
