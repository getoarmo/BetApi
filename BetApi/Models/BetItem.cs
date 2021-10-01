using System;

namespace BetApi.Models
{
    public class BetItem
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public string BetTeam { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }

        public bool IsUpdated { get; set; }
    }
}
