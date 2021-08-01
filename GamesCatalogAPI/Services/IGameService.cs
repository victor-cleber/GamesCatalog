using GamesCatalogAPI.InputModel;
using GamesCatalogAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GamesCatalogAPI.Services {
    public interface IGameService : IDisposable {
        Task<List<GameViewModel>> Get(int page, int quantity);
        Task<GameViewModel> Get(Guid idGame);
        Task<GameViewModel> Insert(GameInputModel game);
        Task Update(Guid idGame, GameInputModel game);
        Task Update(Guid idGame, double price);
        Task Remove(Guid idGame);
    }
}
