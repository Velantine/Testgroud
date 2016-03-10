using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Chat : MonoBehaviour {
	public Text ctext;
	public Text iText;
	public GameObject player;
	// Use this for initialization
	void Start () {
		ctext = gameObject.GetComponent<Text> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	//An alle spieler ist noch nötig!
	public void Chatin(){
		ctext.text = (gameObject.GetComponent<Text> ().text + "\r\n" +player.name+": "+ iText.text);
	}
}
