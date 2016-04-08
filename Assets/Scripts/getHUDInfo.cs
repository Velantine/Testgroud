using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class getHUDInfo : MonoBehaviour {
	public GameObject ThisO;
	public Slider healthSlider;
	public Text deathCounter;
	public Text Ammonition;

	void Update () {
		NetworkHealth HScript = ThisO.GetComponent<NetworkHealth>();
		healthSlider.value = HScript.Health;
		NetworkGun GunScript = ThisO.GetComponentInChildren<NetworkGun> ();
		Ammonition.text = GunScript.Ammunition.ToString();
		deathCounter.text = "Deaths: " + HScript.deaths;
	}
}
