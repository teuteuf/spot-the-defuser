﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using Main.Domain.Players;

namespace Main.Domain.DefuseAttempts
{
    public class DefuseAttempt
    {
        private const int DEFAULT_TIME_TO_DEFUSE = 10;
        
        public string BombId { get; }
        public virtual int TimeToDefuse { get; } = DEFAULT_TIME_TO_DEFUSE;

        private readonly DefuserCounter _defuserCounter;
        private readonly IList<Player> _defuserPlayers;

        public DefuseAttempt(IRandom random, DefuserCounter defuserCounter, AllBombs allBombs,
            ReadOnlyCollection<Player> allPlayers, int nbBombsDefused)
        {
            _defuserCounter = defuserCounter;
            _defuserPlayers = GetDefuserPlayers(random, allPlayers);
            BombId = allBombs.PickRandomBombId(nbBombsDefused);
        }

        public virtual bool IsDefuser(Player player)
        {
            return _defuserPlayers.Contains(player);
        }

        private List<Player> GetDefuserPlayers(IRandom random, ReadOnlyCollection<Player> allPlayers)
        {
            var numberOfDefuserPlayers = _defuserCounter.GetNumberOfDefuserPlayers(allPlayers.Count);
            var players = new List<Player>(allPlayers);
            var defuserPlayers = new List<Player>();
            for (var i = 0; i < numberOfDefuserPlayers; i++)
            {
                var defuserIndex = random.Range(0, players.Count);
                defuserPlayers.Add(players[defuserIndex]);
                players.RemoveAt(defuserIndex);
            }

            return defuserPlayers;
        }
    }
}
