﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacClient
{
    public class GameData
    {
        public int Id { get; set; }
        public int StateId { get; set; }
        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }
        public int PlayerOneScore { get; set; }
        public int PlayerTwoScore { get; set; }
        public int Winner_Player_id { get; set; }
        public int TargetScore { get; set; }
        public int BoardSize { get; set; }
        public string DisplayMember
        {
            get
            {
                return $"player: {PlayerOne.UserName}, boardsize={BoardSize}, targetscore={TargetScore}";
            }
            set
            {

            }
        }
    }
}