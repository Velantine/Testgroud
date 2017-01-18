using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;

public class NetworkConnect : MonoBehaviour {
	
	NetworkClient myClient;
	public Text PlayerName;
	public Text roomName;
	public Transform playerPrefab;
	public NetworkManager manager;
	public Dropdown weapon;


	void Awake()
	{
		manager.StartMatchMaker();

	}



	public void Connect()
	{
		//manager.matchMaker.ListMatches(0,20, roomName.text, manager.OnMatchList);

	}




	public void CreateConnect(){
		manager.StartMatchMaker();
		//manager.matchMaker.CreateMatch(roomName.text, 20, true, "", manager.OnMatchCreate);
	}




	public void PlayerNameA(){
		GameObject.Find ("Options").GetComponent<Options> ().nameOfPlayer = PlayerName.text;
	}

	public void WeaponSelect(){
		GameObject.Find ("Options").GetComponent<Options> ().weapon = weapon.value;
	}
}

