using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour {
	public float Dist;
	public int AmmunitionC;
	public int HealthC;
	public AudioSource pickUp;
	public GameObject thisObj;

	
	void Start () {

	}

	void Update () {
		var hit = new RaycastHit();
		if (Physics.Raycast (Camera.main.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0f)),out hit, Dist)) {
			var tag = hit.transform.tag;
			switch(tag){
				case "Collect":
					if(Input.GetButtonDown("Enter")){
						pickUp.Play();
						if(hit.transform.gameObject.GetComponent<ObInfo>().name=="Ammo_Box"){
							NetworkGun GunScript = thisObj.GetComponent<NetworkGun> ();
							GunScript.Ammunition = GunScript.Ammunition+hit.transform.gameObject.GetComponent<ObInfo>().iNumber;
							DestroyImmediate(hit.transform.gameObject);
							break;
						}
						if(hit.transform.gameObject.GetComponent<ObInfo>().name=="Medkit"){
							NetworkHealth HScript = thisObj.GetComponent<NetworkHealth> ();
							HScript.Health = HScript.Health+hit.transform.gameObject.GetComponent<ObInfo>().iNumber;
							DestroyImmediate(hit.transform.gameObject);
							break;
						}
						break;
					}
					break;
				case "Vehicle":
					//Something
					break;
				case "Activate":
					//Something
					break;
			}
		}
	}
}
