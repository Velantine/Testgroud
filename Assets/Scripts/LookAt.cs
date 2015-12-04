using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour {
	public float Dist;
	public int AmmunitionC;
	public int HealthC;
	public AudioSource pickUp;

	
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
						DestroyImmediate(hit.transform.gameObject);
						if(hit.transform.gameObject.name=="Ammo_Box"){
							this.gameObject.GetComponent<NetworkGun>().Ammunition = gameObject.GetComponent<NetworkGun>().Ammunition+AmmunitionC;
						}
						if(hit.transform.gameObject.name=="Medkit"){
							this.gameObject.GetComponent<NetworkGun>().Ammunition = gameObject.GetComponent<NetworkGun>().Ammunition+AmmunitionC;
						}
						break;
					}
					break;
				case "Vehicle":
					break;
			}
		}
	}
}
