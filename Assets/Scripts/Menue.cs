﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using System.Collections.Generic;

public class Menue : MonoBehaviour {

	public Text newsText;
	public string urlNews = "http://roisawesome.bplaced.net/Testbuild/news.txt";
	public GameObject options;


	IEnumerator Start() {
		WWW www = new WWW(urlNews);
		yield return www;
		newsText.text = www.text;
		DestroyImmediate (GameObject.Find ("Networkmanager"));


		if (!GameObject.Find("Options")) {
			Instantiate (options);
			GameObject.Find("Options(Clone)").name="Options";
		}
			
	}


	// Update is called once per frame
	void Update () {
	
	}


	public void UpdateOptions(){
		GameObject.Find ("Options").GetComponent<Options> ().NumberUpdate ();
	}

	/*public NetworkManager NetworkmanagerO;
	public GameObject NetworkmanagerHUD;
	public Text RoomName;
	public Text PlayerName;
	public uint MatchSize;

	public void Play(){
		NetworkmanagerO.StartMatchMaker ();
	}
	public void Create(){
		//NetworkmanagerO.StartMatchMaker ();
		NetworkmanagerO.matchMaker.CreateMatch (RoomName.text, MatchSize, true, "", NetworkmanagerO.OnMatchCreate);

	}
	*/
}