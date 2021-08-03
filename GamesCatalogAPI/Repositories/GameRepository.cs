using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using GamesCatalogAPI.Entities;

namespace GamesCatalogAPI.Repositories {
    public class GameRepository : IGameRepository {

        private static Dictionary<Guid, Game> games = new Dictionary<Guid, Game>() {

            {Guid.Parse("86eced56-82fe-4fe6-9b49-5a13b40d25bb"), new Game {Id = Guid.Parse("86eced56-82fe-4fe6-9b49-5a13b40d25bb"), Name="Fifa 20", Producer="EA",Price=190 } },
            {Guid.Parse("5ce86408-3bdd-463d-acc9-1f37f973919d"), new Game {Id = Guid.Parse("5ce86408-3bdd-463d-acc9-1f37f973919d"), Name="Fifa 21", Producer="EA",Price=200 } },
            {Guid.Parse("9b405113-df7b-4752-a7e4-de6e284c5ff5"), new Game {Id = Guid.Parse("9b405113-df7b-4752-a7e4-de6e284c5ff5"), Name="Fifa 22", Producer="EA",Price=240 } },
            {Guid.Parse("d951cb62-0152-4f71-95bf-e0b30748ee6e"), new Game {Id = Guid.Parse("d951cb62-0152-4f71-95bf-e0b30748ee6e"), Name="Fifa 18", Producer="EA",Price=170 } },
            {Guid.Parse("35742528-c36e-4e85-823a-9ec131ca0e55"), new Game {Id = Guid.Parse("35742528-c36e-4e85-823a-9ec131ca0e55"), Name="Street Fighter V", Producer="Capcom",Price=80 } },
            {Guid.Parse("703051da-8c0d-4ac5-b8c4-e37560256d70"), new Game {Id = Guid.Parse("703051da-8c0d-4ac5-b8c4-e37560256d70"), Name="Grand Theft Auto V", Producer="Rockstar",Price=190} }
        };

        public Task<List<Game>> Get(int page, int quantity) {
            
            return Task.FromResult(games.Values.Skip((page - 1) * quantity).Take(quantity).ToList());
        }

        public Task<Game> Get(Guid id){

            if (!games.ContainsKey(id))
                    return null;
            //Task.Fromresult creates a task without connected with a database
            return Task.FromResult(games[id]);
        }

        public Task<List<Game>> Get(string name, string producer) {
            
            return Task.FromResult(games.Values.Where(game => game.Name.Equals(name) && game.Producer.Equals(producer)).ToList<Game>());
        }

        public Task<List<Game>> GetWithoutLambda(string name, string producer) {

            var result = new List<Game>();

            foreach (var game in games.Values) {

                if (game.Name.Equals(name) && game.Producer.Equals(producer))
                    result.Add(game);
            }
            return Task.FromResult(result);
        }

        public Task Insert(Game game) {

            games.Add(game.Id, game);
            return Task.CompletedTask;
        }

        public Task Update(Game game) {
            games[game.Id] = game;
            return Task.CompletedTask;
        }

        public Task Remove(Guid id) {
            games.Remove(id);
            return Task.CompletedTask;
        }

        public void Dispose() {
            //Close the database connections 
        }
    }
}
