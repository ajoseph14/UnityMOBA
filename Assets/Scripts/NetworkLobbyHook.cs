using UnityEngine;
using Prototype.NetworkLobby;
using System.Collections;
using UnityEngine.Networking;

public class NetworkLobbyHook : LobbyHook 
{

	public override void OnLobbyServerSceneLoadedForPlayer(NetworkManager manager, GameObject lobbyPlayer, GameObject gamePlayer)
	{
		LobbyPlayer lobby = lobbyPlayer.GetComponent<LobbyPlayer>();

		//Player localPlayer = gamePlayer.GetComponent<Player>(); // changed from gameplayercontroller - For Medea
		Rip_PlayerScript localPlayer = gamePlayer.GetComponent<Rip_PlayerScript>(); //For Rip,

		localPlayer.pname = lobby.playerName;
	}
}
