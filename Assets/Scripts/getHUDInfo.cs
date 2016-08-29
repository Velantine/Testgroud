using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class getHUDInfo : MonoBehaviour {
	public GameObject ThisO;
    public WeaponInfo WI;
    public Slider healthSlider;
	public Text deathCounter;
	public Text Ammonition;

    void Start() {
        WI = ThisO.GetComponent<NetworkGun>().WI;
    }

	void Update () {
        WI = ThisO.GetComponent<NetworkGun>().WI;
        NetworkHealth HScript = ThisO.GetComponent<NetworkHealth>();
		healthSlider.value = HScript.Health;
		NetworkGun GunScript = ThisO.GetComponentInChildren<NetworkGun> ();
		Ammonition.text = WI.ammo.ToString();
		deathCounter.text = "Deaths: " + HScript.deaths;
	}
}
