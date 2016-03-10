using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;
using System;

public class NetworkConnect : MonoBehaviour {
	
	NetworkClient myClient;
	public Text PlayerName;
	public Text IP;
	public Text TPort;
	public Transform playerPrefab;

	void Start()
	{

	}

	public void SetupServer()
	{
		NetworkServer.Listen(int.Parse(TPort.text));
	}
	
	// Create a client and connect to the server port
	public void SetupClient()
	{
		myClient = new NetworkClient();
		myClient.RegisterHandler(MsgType.Connect, OnConnected);     
		myClient.Connect(IP.text, int.Parse(TPort.text));
	}
	
	// Create a local client and connect to the local server
	public void SetupLocalClient()
	{
		myClient = ClientScene.ConnectLocalServer();
		myClient.RegisterHandler(MsgType.Connect, OnConnected);     
	}


	public void CreateAndConnect()
	{
		SetupServer();
		SetupLocalClient();
	}



	public void OnConnected(NetworkMessage netMsg)
	{
		Debug.Log("Connected to server");
		if(NetworkServer.active && myClient.isConnected){
			Debug.Log("NetworkServer is activ");
			Application.LoadLevel(2);
		}
	}

	public void OnDisconnect(NetworkMessage netMsg)
	{
		Debug.Log("Disconnected from server");
		Application.LoadLevel(0);
	}


	public void PlayerNameA(){
		GameObject.Find ("Options").GetComponent<Options> ().name = PlayerName.text;
	}
}

