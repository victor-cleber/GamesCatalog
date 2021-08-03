using System;
using GamesCatalogAPI.Model;

namespace GamesCatalogAPI.Entities {
    public class Game : Base{        
        public string Name {
            get; set;
        }
        public string Producer {
            get; set;
        }
        public double Price {
            get; set;
        }
    }


}
