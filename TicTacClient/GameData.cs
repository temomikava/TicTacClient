using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacClient
{
    public class GameData
    {
        public int GameId { get; set; }
        public int StateId { get; set; }
        public Player PlayerOne { get; set; }
        public Player PlayerTwo { get; set; }
        public int PlayerOneScore { get; set; }
        public int PlayerTwoScore { get; set; }
        public int Winner_Player_id { get; set; }
        public int TargetScore { get; set; }
        public int BoardSize { get; set; }
        private string? State { get; set; }
        public string DisplayMember
        {           
            get
            {
                return $"player1: {PlayerOne.UserName}, player2 {PlayerTwo.UserName}, boardsize={BoardSize}, targetscore={TargetScore}, state= {GetState()}";
            }            
        }
        private string GetState()
        {
            State = StateId == 1 ? StateType.created.ToString() : StateType.started.ToString();
            return State;
        }
    }
}
