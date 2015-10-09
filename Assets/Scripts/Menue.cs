using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Menue : MonoBehaviour {
	public Text NewsText;
	public string urlNews = "http://roisawesome.bplaced.net/Testbuild/news.txt";
	// Use this for initialization

	IEnumerator Start() {
		WWW www = new WWW(urlNews);
		yield return www;
		NewsText.text = www.text;
	}
	

	// Update is called once per frame
	void Update () {
	
	}
}
