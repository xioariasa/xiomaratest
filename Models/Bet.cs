using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    [Serializable]
    public class Bet
    {
        public double Amount { get; set; }
        public int? Number { get; set; }
        public Colors? Color { get; set; }
        public string User { get; set; }
        public bool IsWinner { get; set; }
        public double Reward { get; set; }
    }
}
