using System.Threading.Tasks;
using Grafos.Services.Interfaces;
using Grafos.Models;
using Grafos.Repository.Interfaces;
using System.Collections.Generic;
using System;

namespace Grafos.Services.Implementations
{
    public class DistanceService : IDistanceService
    {
        IDistanceRepository _distanceRepository;
        private int menorDist;
        private string rotaAtual;
        public DistanceService(IDistanceRepository distanceRepository){
            _distanceRepository = distanceRepository;
        }

        public Distancia achaMenorDistancia(Graph grafo, string town1, string town2){
            Distancia dist = new Distancia();
            if (town1 == town2){
                dist.distancia = 0;
                return dist;
            }
            menorDist = Int32.MaxValue;
            rotaAtual = "";
            recursao(grafo, town1, town2);
            if(rotaAtual == ""){
                dist.distancia = -1;
                return dist;
            }
            dist.distancia = menorDist;
            dist.path = rotaAtual.ToCharArray();
            
            return dist;
        }

        private void recursao(Graph grafo, string origem, string destino, int distAtual = 0, Linha linha = null, string rota = ""){
            if(linha != null){
                if(menorDist < distAtual)
                    return;
            }            
            if(origem == destino){
                menorDist = distAtual;
                rotaAtual = rota + origem;
                return;
            }
            foreach(Linha dado in grafo.data){
                if (dado.source != origem)
                    continue;
                if(rota.Contains(dado.target)){
                    continue;
                }
                recursao(grafo, dado.target, destino, distAtual + dado.distance, dado, rota + origem);
            }
        }
    }
}
