using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TicTacToe.ApiModels;
using TicTacToe.Data;
using TicTacToe.Models;

namespace TicTacToe.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GameController : ControllerBase
    {
        private TicTacToeDb _db;

        public GameController(TicTacToeDb db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("{gameId}")]
        public string Get(int gameId)
        {
            var map = _db.Games
                .FirstOrDefault(x => x.Id == gameId)?
                .Map;

            return JsonConvert.SerializeObject(map);
        }

        [HttpPost]
        [Route("{gameId}")]
        public void Post(int gameId, Position pos)
        {
            var game = _db.Games.FirstOrDefault(x => x.Id == gameId);

            var map = JsonConvert.DeserializeObject<int[,]>(game.Map);
            var turn = game.Turn;

            var engine = new GameEngine(map, turn);
            engine.MakeTurn(pos.Row, pos.Col);

            game.Map = JsonConvert.SerializeObject(engine.Map);
            game.Turn = engine.CurrentTurn;
            game.GameOver = engine.GameOver();

            _db.SaveChanges();
        }

        [HttpPut]
        public void Put()
        {
            var engine = new GameEngine();
            var game = new Game
            {
                Map = JsonConvert.SerializeObject(engine.Map),
                Turn = engine.CurrentTurn,
                GameOver = engine.GameOver()
            };

            _db.Games.Add(game);
            _db.SaveChanges();
        }
    }
}
