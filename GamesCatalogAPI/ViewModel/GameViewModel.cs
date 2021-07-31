using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesCatalogAPI.Model

{
    public class GameViewModel : Base
    {        
        public string Name { get; set; }
        public string Producer{ get; set; }
        public double  Price { get; set; }
    }
}
