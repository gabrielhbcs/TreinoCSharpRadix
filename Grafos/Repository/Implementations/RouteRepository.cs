using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Grafos.Repository.Interfaces;
using Grafos.Repository.Configuration;
using Grafos.Models;
using System;
using System.Collections.Generic;

namespace Grafos.Repository.Implementations
{
    public class RouteRepository : IRouteRepository
    {
        private readonly GraphDataContext _context;
        private List<Rota> rotasFinais = new List<Rota>();
        public RouteRepository(GraphDataContext context){
            _context = context;
        }

        public List<Rota> achaRota(Graph grafo, string origem, string destino, int maxStops, int stops = 0, string ultimo = ""){
            recursao(grafo, origem, destino, maxStops);

            return rotasFinais;
        }

        private void recursao(Graph grafo, string origem, string destino, int maxStops, int stops = 0, string ultimo = "", string rotaFinal = ""){
            if(origem == destino){
                Rota aux = new Rota();
                aux.rota = rotaFinal + origem;
                aux.stops = stops;
                rotasFinais.Add(aux);
                return;
            } else if(stops == maxStops){
                return;
            }
            foreach(Linha linha in grafo.data){
                if(linha.source != origem || linha.target == ultimo){
                    continue;
                }
                recursao(grafo, linha.target, destino, maxStops, stops + 1, origem, rotaFinal + origem);
            }

        }

    }
}
