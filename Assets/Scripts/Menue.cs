using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Menue : MonoBehaviour {
	public Text newsText;
	public string urlNews = "http://roisawesome.bplaced.net/Testbuild/news.txt";
	GameObject Networkmanager;
	// Use this for initialization

	IEnumerator Start() {
		WWW www = new WWW(urlNews);
		yield return www;
		newsText.text = www.text;
		DestroyImmediate (GameObject.Find ("Networkmanager"));
		

	}
	

	// Update is called once per frame
	void Update () {
	
	}

	void OpenNews(){

	}
}
