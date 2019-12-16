using Xunit;
using Moq;
using Grafos.Controllers;
using Grafos.Services.Interfaces;
using Grafos.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;

namespace GrafosTests.Controllers
{
    public class RouteControllerTest
    {
        private RouteController _routeController;
        private ILogger<RouteController> _log;
        public RouteControllerTest()
        {            
            var serviceProvider = new ServiceCollection()
                .AddLogging()
                .BuildServiceProvider();
            var factory = serviceProvider.GetService<ILoggerFactory>();
            _log = factory.CreateLogger<RouteController>();
        }

        [Fact (DisplayName = "Bad Request no teste de rota")]
        public void TesteRotaBadRequest(){
            var mock = new Mock<IRouteService>();
        }
    }
}
