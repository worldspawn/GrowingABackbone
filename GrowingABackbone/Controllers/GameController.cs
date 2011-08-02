using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
        public ActionResult Data(Guid? gameId)
        {
            if (gameId == null)
                return Json(games, JsonRequestBehavior.AllowGet);

            return Json(games.FirstOrDefault(g => g.Id == gameId.Value), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Data(Game game)
        {
            var dsGame = games.First(p => p.Id == game.Id);
            games[games.ToList().IndexOf(dsGame)] = game;
            return new EmptyResult();
        }

        static readonly Game[] games = new[]
        {
            new Game{ Id = Guid.NewGuid(), Name="Portal" },
            new Game{ Id = Guid.NewGuid(), Name="Portal 2" },
            new Game{ Id = Guid.NewGuid(), Name="Star Wars: Knights of the Old Republic" },
            new Game{ Id = Guid.NewGuid(), Name="Battlefield: Bad Company 2" },
            new Game{ Id = Guid.NewGuid(), Name="Team Fortress" },
            new Game{ Id = Guid.NewGuid(), Name="Populous" },
            new Game{ Id = Guid.NewGuid(), Name="Flashback" },
            new Game{ Id = Guid.NewGuid(), Name="Another World" },
        };
    }

    public class Game
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsWatched { get; set; }
    }

    
}
