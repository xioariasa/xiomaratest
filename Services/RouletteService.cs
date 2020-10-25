using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Models;
using Repository;

namespace Services
{
    public class RouletteService : IRouletteService
    {
        private IBettingRepository BettingRepository;

        public RouletteService(IBettingRepository bettingRepository)
        {
            this.BettingRepository = bettingRepository;
        }
        public void Bet(Bet bet)
        {
            this.BettingRepository.Bet(bet);
        }

        public List<Bet> Close(int id)
        {
            var roulette = this.BettingRepository.GetById(id);
            var winnerNumber = GetWinner();
            var bets = SetWinnerBet(roulette.Bets, winnerNumber);
            roulette.Bets = bets;
            this.BettingRepository.Close(roulette);
            return bets;
        }

        public int Create()
        {
            return this.BettingRepository.Create();
        }

        public List<Roulette> GetAll()
        {
            return this.BettingRepository.GetAll();
        }

        public bool Open(int id)
        {
            return this.BettingRepository.Open(id);
        }

        private int GetWinner()
        {
            Random random = new Random();
            return random.Next(1, 36);
        }

        private List<Bet> SetWinnerBet(List<Bet> bets, int winnerNumber)
        {
            bets.Where(x => x.Number == winnerNumber).ToList().ForEach(x => x.Reward = x.Amount * 5);
            var isBlack = this.IsBlack(winnerNumber);
            if (isBlack)
            {
                bets.Where(x => x.Color == Colors.Black).ToList().ForEach(x => x.Reward = x.Amount * 1.8);
            }
            else
            {
                bets.Where(x => x.Color == Colors.Red).ToList().ForEach(x => x.Reward = x.Amount * 1.8);
            }
            return bets;
        }

        private bool IsBlack(int number)
        {
            return number % 2 != 0;
        }
    }
}
