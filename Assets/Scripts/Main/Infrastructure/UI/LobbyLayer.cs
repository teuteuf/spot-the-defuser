using Main.Domain.DefuseAttempts;
using Main.Domain.Players;
using Main.Domain.UI;
using Main.Domain.UI.Layers;
using Main.Infrastructure.Controllers.Network;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Main.Infrastructure.UI
{
    public class LobbyLayer : BaseLayer, ILobbyLayer
    {
        public Text nbDefusersText;
        public Text nbExplosivesText;
        public Button playButton;

        private const string DEFAULT_NB_DEFUSERS_DISPLAYED = "0";
        private const string DEFAULT_NB_EXPLOSIVES_DISPLAYED = "1";

        private DefuserCounter _defuserCounter;
        private AllPlayerControllers _allPlayerControllers;

        [Inject]
        public void Init(DefuserCounter defuserCounter, AllPlayerControllers allPlayerControllers)
        {
            _defuserCounter = defuserCounter;
            _allPlayerControllers = allPlayerControllers;
        }

        public void Start()
        {
            nbDefusersText.text = DEFAULT_NB_DEFUSERS_DISPLAYED;
            nbExplosivesText.text = DEFAULT_NB_EXPLOSIVES_DISPLAYED;
            playButton.interactable = false;
        }

        public void UpdatePlayerList(Player[] allPlayers)
        {
            nbExplosivesText.text = _defuserCounter.GetNumberOfExplosivePlayers(allPlayers.Length).ToString();
            
            var numberOfDefuserPlayers = _defuserCounter.GetNumberOfDefuserPlayers(allPlayers.Length);
            nbDefusersText.text = numberOfDefuserPlayers.ToString();
            playButton.interactable = numberOfDefuserPlayers > 0;
        }

        public void OnClickOnPlay()
        {
            _allPlayerControllers.StartNewGameOnServer();
        }

        public override View GetView()
        {
            return View.Lobby;
        }
    }
}