using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace GrowingABackbone.Controllers
{
    public class GameController : Controller
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [ActionName("Data")]
        public ActionResult Get(Guid? gameId)
        {
            if (gameId == null)
                return Json(games, JsonRequestBehavior.AllowGet);

            return Json(games.FirstOrDefault(g => g.id == gameId.Value), JsonRequestBehavior.AllowGet);
        }
        
        [HttpPut]
        [ActionName("Data")]
        public ActionResult Put(Game game)
        {
            var dsGame = games.First(p => p.id == game.id);
            games[games.ToList().IndexOf(dsGame)] = game;
            return new EmptyResult();
        }

        [HttpDelete]
        [ActionName("Data")]
        public ActionResult Delete(Game game)
        {
            var dsGame = games.First(p => p.id == game.id);
            games.Remove(dsGame);

            return new EmptyResult();            
        }

        [HttpPost]
        [ActionName("Data")]
        public ActionResult Post([Bind(Exclude = "Id,id")]Game game)
        {
            games.Add(game);
            game.id = Guid.NewGuid();

            return Json(game);
        }

        static readonly IList<Game> games = new List<Game>
        {
            new Game{ id = Guid.NewGuid(), Name="Portal" },
            new Game{ id = Guid.NewGuid(), Name="Portal 2" },
            new Game{ id = Guid.NewGuid(), Name="Star Wars: Knights of the Old Republic" },
            new Game{ id = Guid.NewGuid(), Name="Battlefield: Bad Company 2" },
            new Game{ id = Guid.NewGuid(), Name="Team Fortress" },
            new Game{ id = Guid.NewGuid(), Name="Populous" },
            new Game{ id = Guid.NewGuid(), Name="Flashback" },
            new Game{ id = Guid.NewGuid(), Name="Another World" },
        };
    }
    
    public interface IBackboneModel
    {
        Guid? id { get; set; }
    }
    
    [Bind(Exclude="Id")]
    public class BackboneModel : IBackboneModel
    {
        public Guid? id { get; set; }
    }

    public class Game : BackboneModel
    {
        public string Name { get; set; }
        public bool IsWatched { get; set; }
    }
}
