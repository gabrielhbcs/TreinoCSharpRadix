using Grafos.Models;
using System.Threading.Tasks;

namespace Grafos.Services.Interfaces
{
    public interface IGraphService
    {
        Task<Graph> LoadGraph (int ID);
        Task<int> SaveGraph (Graph graph);
    }


}