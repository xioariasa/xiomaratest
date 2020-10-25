using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Models;
using Services;

namespace MasivianTestApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RouleteController : ControllerBase
    {
        private IRouletteService RouletteService;
        private readonly ILogger<RouleteController> _logger;
        public RouleteController(ILogger<RouleteController> logger, IRouletteService rouletteService)
        {
            _logger = logger;
            this.RouletteService = rouletteService;
        }

        [HttpPost]
        [Route("Create")]
        public int Create()
        {
            return this.RouletteService.Create();
        }

        [HttpPost]
        [Route("Open/{id}")]
        public bool Open(int id)
        {
            return this.RouletteService.Open(id);
        }

        [HttpPost]
        [Route("Bet")]
        public void Bet([FromBody]Bet bet)
        {
            Validation(bet);
            IEnumerable<string> headerValues = Request.Headers["userId"];
            bet.User = headerValues.FirstOrDefault();
            this.RouletteService.Bet(bet);
        }

        [HttpPost]
        [Route("Close/{id}")]
        public List<Bet> Close(int id)
        {
           return this.RouletteService.Close(id);           
        }

        [HttpGet]
        [Route("GetAll")]
        public List<Roulette> GetAll()
        {
            return this.RouletteService.GetAll();
        }

        private void Validation(Bet bet)
        {
            if (bet.Number != null && bet.Color != null)
            {
                throw new Exception("You have to select just one type of bet (Color or number)");
            }
            if (bet.Number > 36 && bet.Number < 0)
            {
                throw new Exception("The number is not valid for the roulette bet");
            }
            if (bet.Amount > 10000)
            {
                throw new Exception("The bet exceeds the allowed maximum amount");
            }
            if (!Request.Headers.ContainsKey("userId"))
            {
                throw new Exception("Invalid authentication");

            }
        }

    }
}
