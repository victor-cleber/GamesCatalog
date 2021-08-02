using GamesCatalogAPI.Exceptions;
using GamesCatalogAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GamesCatalogAPI.ViewModel;


namespace GamesCatalogAPI.Controllers.v1 {
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GamesController : Controller {
        //var service = new Service();

        private readonly IGameService _gameService;

        public GamesController(IGameService gameService) {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameViewModel>>> GetGames([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int quantity = 5) {
            var games = await _gameService.Get(page, quantity);

            if (games.Count() == 0)
                return NoContent();

            return Ok(games);
        }

        [HttpGet("{idGame:guid}")]
        public async Task<ActionResult<GameViewModel>> GetGame([FromRoute] Guid idGame) {
            var game = await _gameService.Get(idGame);

            if (game == null)
                return NoContent();

            return Ok(game);
        }

        [HttpPost]
        public async Task<ActionResult<GameViewModel>> InsertGame([FromBody] GameInputModel gameInputModel) {
            try {
                var game = await _gameService.Insert(gameInputModel);
                return Ok();
            } catch (GameAlreadyRegisteredException ex) {
                return UnprocessableEntity("There is already a game for this producer with this name.");

            }
        }

        [HttpPut("{idGame:guid}")]
        public async Task<ActionResult> UpdateGame([FromRoute] Guid idGame, [FromBody] GameInputModel gameInputModel) {
            try {
                await _gameService.Update(idGame, gameInputModel);

                return Ok();
            } catch (GameNotRegisteredException ex) {
                return NotFound("This game does not exist.");
            }
        }

        [HttpPatch("{idGame:guid}/price/{price:double}")]
        public async Task<ActionResult> UpdatePrice([FromRoute] Guid idGame, [FromRoute] double price) {
            try {
                await _gameService.Update(idGame, price);

                return Ok();
            } catch (GameNotRegisteredException ex) {
                return NotFound("This game does not exist.");
            }
        }

        [HttpDelete("{idGame:guid}")]
        public async Task<ActionResult> DeleteGame([FromRoute] Guid idGame) {
            try {
                await _gameService.Remove(idGame);

                return Ok();
            } catch (GameNotRegisteredException ex) {
                return NotFound("This game does not exist.");
            }
        }

    }
}
