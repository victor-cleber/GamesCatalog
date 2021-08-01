using GamesCatalogAPI.Model;

namespace GamesCatalogAPI.ViewModel {
    public class GameViewModel : Base {
        /*view model can be part of a model or 
        for a report a view model can be compound of fields from diferent tables*/

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
