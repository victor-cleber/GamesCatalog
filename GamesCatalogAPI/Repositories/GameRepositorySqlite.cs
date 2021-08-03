using GamesCatalogAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GamesCatalogAPI.Repositories {
    public class GameRepositorySqlite : IGameRepository {
        public void Dispose() {
            throw new NotImplementedException();
        }

        public Task<List<Game>> Get(int page, int quantity) {
            throw new NotImplementedException();
        }

        public Task<Game> Get(Guid id) {
            throw new NotImplementedException();
        }

        public Task<List<Game>> Get(string name, string producer) {
            throw new NotImplementedException();
        }

        public Task Insert(Game game) {
            throw new NotImplementedException();
        }

        public Task Remove(Guid idGame) {
            throw new NotImplementedException();
        }

        public Task Update(Game game) {
            throw new NotImplementedException();
        }
    }
}
