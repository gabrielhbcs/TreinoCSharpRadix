using Xunit;
using Moq;
using Grafos.Controllers;
using Grafos.Services.Interfaces;
using Grafos.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using System;

namespace GrafosTests
{
    public class GraphControllerTest
    {

        private GraphController _graphController;
        private ILogger<GraphController> _log;

        public GraphControllerTest()
        {
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();
            var factory = serviceProvider.GetService<ILoggerFactory>();
            _log = factory.CreateLogger<GraphController>();
        }

        [Fact (DisplayName = "Gráfico não encontrado: deve retornar código 404")]
        public void GraphNotFound()
        {
            var mock = new Mock<IGraphService>();
            mock.Setup(p => p.LoadGraph(0)).Returns(Task.FromResult<Graph>(new Graph()));
            GraphController graphController = new GraphController(mock.Object,_log);
            var result =  graphController.GetAsync(9999).Result;
            Assert.IsAssignableFrom<NotFoundResult>(result);
        }


        [Fact (DisplayName = "Grafo criado com sucesso")]
        public void GraphCriado(){
            Graph grafo = new Graph();
            var mock = new Mock<IGraphService>();
            
            mock.Setup(p => p.SaveGraph(It.IsAny<Graph>()))
                .Callback<Graph>(c => c.ID = 1)
                .Returns(Task.FromResult<int>(1));
            GraphController gc = new GraphController(mock.Object, _log);
            var result = gc.PostAsync(grafo);
            Assert.IsAssignableFrom<CreatedAtActionResult>(result.Result);
            
            var g = (Graph)((CreatedAtActionResult)result.Result).Value;
            Assert.Equal(1, g.ID);            
        }
    }
}
