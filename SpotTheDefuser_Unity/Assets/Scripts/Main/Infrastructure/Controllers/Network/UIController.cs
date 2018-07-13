﻿using UnityEngine;
using Zenject;

namespace Main.Infrastructure.Controllers.Network
{
	public class UIController : MonoBehaviour
	{
		[Inject] public AllPlayerControllers AllPlayerControllers;

		public string PlayerName { get; private set; }

		public void OnEndEditOnPlayerName(string playerName)
		{
			PlayerName = playerName;
		}

		public void OnClickOnAddPlayer()
		{
			Debug.Log("OnClickOnAddPlayer with player name: " + PlayerName);
		}
		
		public void OnClickOnNewDefuseAttempt()
		{
			AllPlayerControllers.SetNewDefuseAttemptOnServer();
		}
		
		public void OnClickOnTryToDefuse()
		{
			Debug.Log("OnClickOnTryToDefuse");
		}
	}
}
