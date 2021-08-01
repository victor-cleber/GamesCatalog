using System;

namespace GamesCatalogAPI.Entities {
    public class Game {
        public Guid Id {
            get; set;
        }
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
