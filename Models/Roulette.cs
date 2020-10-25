using System;
using System.Collections.Generic;

namespace Models
{
    [Serializable]
    public class Roulette
    {
        public Roulette()
        {
            this.Bets = new List<Bet>();
        }

        public int Id { get; set; }
        public bool IsOpen { get; set; }
        public List<Bet> Bets { get; set; }
    }
}
