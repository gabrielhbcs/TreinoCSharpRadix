using System;
using Newtonsoft.Json;

namespace Grafos.Models
{
    public class Linha
    {
        [JsonIgnore]
        public int ID { get; set; }
        public string source { get; set; }
        public string target { get; set; }
        public int distance { get; set; }


        public string ToString(string[] args){
            string toto;
            toto = "Source: '" + source +"' Tarrget: '" + target + "' Distance: '" + distance.ToString() + "'";

            return toto;
        }
    }
}
