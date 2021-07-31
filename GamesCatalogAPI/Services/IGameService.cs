using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GamesCatalogAPI.Model;

namespace GamesCatalogAPI.Services
{
    public interface IGameService
    {
        Task<List<GameViewModel>> Get(int page, int quantity);
        Task<GameViewModel> Get(Guid idGame);
        Task<GameViewModel> Insert(GameInputModel game);
        Task Update(Guid idGame, GameInputModel game);
        Task Update(Guid idGame, double price);
        Task Remove(Guid idGame);


    }
}
