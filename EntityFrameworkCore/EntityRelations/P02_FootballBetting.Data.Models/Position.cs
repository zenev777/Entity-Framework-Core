﻿using System.ComponentModel.DataAnnotations;

namespace P02_FootballBetting.Data.Models
{
    public class Position
    {
        public int PositionId{ get; set; }

        [Required]
        [MaxLength(Constraints.PositionNameLenght)]
        public string Name { get; set; }

        public virtual ICollection<Player> Players { get; set; }
    }
}