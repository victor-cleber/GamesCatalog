using GamesCatalogAPI.Exceptions;
using GamesCatalogAPI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using GamesCatalogAPI.ViewModel;
using GamesCatalogAPI.InputModel;


namespace GamesCatalogAPI.Controllers.v1 {
    [Route("api/v1/[controller]")]
    [ApiController]
    public class GamesController : Controller {
        //var service = new Service();

        private readonly IGameService _gameService;

        public GamesController(IGameService gameService) {
            _gameService = gameService;
        }
        /// <summary>
        /// Get all games using pagination
        /// </summary>
        /// <remarks>
        /// It is not possible get games without pagination
        /// </remarks>
        /// <param name="page">Indicates wich page is being consulted [minimum 1]</param>
        /// <param name="quantity">Indicates the quantity of records per page. [minimum 1 - maximum 50</param>
        /// <response code="200">Return a list of games</response>
        /// <response code="204">In case that there are no games</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GameViewModel>>> GetGames([FromQuery, Range(1, int.MaxValue)] int page = 1, [FromQuery, Range(1, 50)] int quantity = 5) {
            var games = await _gameService.Get(page, quantity);

            if (games.Count() == 0)
                return NoContent();

            return Ok(games);
        }

        /// <summary>
        /// Get a game by id
        /// </summary>
        /// <param name="idGame">Game id searched</param>
        ///<response code="200">Return the game searched</response>
        ///<response code="204">Case there is no game with this id</response>
        [HttpGet("{idGame:guid}")]
        public async Task<ActionResult<GameViewModel>> GetGame([FromRoute] Guid idGame) {
            var game = await _gameService.Get(idGame);

            if (game == null)
                return NoContent();

            return Ok(game);
        }

        /// <summary>
        /// Insert a new game
        /// </summary>
        /// <param name="gameInputModel">A data representation for a new game</param>
        /// <response code="200">The game was inserted</response>
        /// <response code="204">There is already a game for this producer with this name</response>
        [HttpPost]
        public async Task<ActionResult<GameViewModel>> InsertGame([FromBody] GameInputModel gameInputModel) {
            try {
                var game = await _gameService.Insert(gameInputModel);
                return Ok();
            } catch (GameAlreadyRegisteredException ex) {
                return UnprocessableEntity("There is already a game for this producer with this name.");

            }
        }

        /// <summary>
        /// Update a game
        /// </summary>
        /// <param name="idGame">The identification of a game</param>
        /// <param name="gameInputModel">A data representation for a game</param>
        /// <response code="200">The game was updated</response>
        /// <response code="204">This game does not exist</response>
        [HttpPut("{idGame:guid}")]
        public async Task<ActionResult> UpdateGame([FromRoute] Guid idGame, [FromBody] GameInputModel gameInputModel) {
            try {
                await _gameService.Update(idGame, gameInputModel);

                return Ok();
            } catch (GameNotRegisteredException ex) {
                return NotFound("This game does not exist.");
            }
        }

        /// <summary>
        /// Update a game price
        /// </summary>
        /// <param name="idGame">The identification of a game</param>
        /// <param name="price">The price of a game</param>
        /// <response code="200">The price of a game was update</response>
        /// <response code="204">This game does not exist</response>
        [HttpPatch("{idGame:guid}/price/{price:double}")]
        public async Task<ActionResult> UpdatePrice([FromRoute] Guid idGame, [FromRoute] double price) {
            try {
                await _gameService.Update(idGame, price);

                return Ok();
            } catch (GameNotRegisteredException ex) {
                return NotFound("This game does not exist.");
            }
        }

        /// <summary>
        /// Remove a game
        /// </summary>
        /// <param name="idGame">The identification of a game</param>
        /// <response code="200">The game was deleted</response>
        /// <response code="204">This game does not exist</response>
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
