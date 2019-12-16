using System.Collections.Generic;

namespace Grafos.Models
{
    public class Graph
    {
        //TODO colocar os demais atributos
        public int ID { get; set; }
        public List<Linha> data { get; set; }

        
        public string ToString(string[] args){
            string toto;
            toto = "ID: " + ID.ToString() + "\nData: " + data.ToString();

            return toto;
        }
    }
}