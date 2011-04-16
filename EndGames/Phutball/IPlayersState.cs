﻿namespace EndGames.Phutball
{
    public interface IPlayersState : IPlayersSwapper
    {
        Player CurrentPlayer { get; }
        PlayerOnBoardInfo First { get; }
        PlayerOnBoardInfo Second { get; }
        void Stop();
        IPlayersState CopyRestarted();
        void StartVsComputer();
        void StartVsHuman();
    }
}