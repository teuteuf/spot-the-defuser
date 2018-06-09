﻿using Main.Domain.Players;

namespace Main.UseCases.Players
{
    public class AddNewPlayer
    {
        private readonly PlayerRepository _playerRepository;

        public AddNewPlayer(PlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public virtual void Execute(Player player)
        {
            _playerRepository.Add(player);
        }
    }
}
