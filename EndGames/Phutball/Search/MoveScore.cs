﻿namespace EndGames.Phutball.Search
{
    public class MoveScore<T,TScore>
    {
        private T _move;

        public T Move
        {
            get { return _move; }
            set {                 
                _move = value; 
                
            }
        }

        public TScore Score { get; set; }
        public int Depth { get; set; }

        public override string ToString()
        {
            return "Score: {0}, Move: {1}".ToFormat(Score, Move);
        }
    }
}