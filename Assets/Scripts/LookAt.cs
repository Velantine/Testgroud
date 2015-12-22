using UnityEngine;
using System.Collections;

public class LookAt : MonoBehaviour {
	public float Dist;
	public int AmmunitionC;
	public int HealthC;
	public AudioSource pickUp;
	public GameObject thisObj;
	public GameObject[] prefabs;

	
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
					if (Input.GetButtonDown ("Enter")) {
						pickUp.Play ();
						if (hit.transform.gameObject.GetComponent<ObInfo> ().name == "SpawnAmmo_Box") {
							//Network.Instantiate (prefabs[0], new Vector3(hit.transform.position.x,hit.transform.position.y,hit.transform.position.z-2), transform.rotation, 0);
							Instantiate (prefabs [0], new Vector3 (hit.transform.position.x, hit.transform.position.y, hit.transform.position.z-2), Quaternion.identity);
							break;
						}
						if (hit.transform.gameObject.GetComponent<ObInfo> ().name == "SpawnMedkit") {
							//Network.Instantiate (prefabs[1], new Vector3(hit.transform.position.x,hit.transform.position.y,hit.transform.position.z-2), transform.rotation, 0);
							Instantiate (prefabs [1], new Vector3 (hit.transform.position.x, hit.transform.position.y, hit.transform.position.z-2), Quaternion.identity);
							break;
						}
						/*if (hit.transform.gameObject.GetComponent<ObInfo> ().name == "Ship") {
							Instantiate (prefabs [2], new Vector3 (hit.transform.position.x, hit.transform.position.y+20, hit.transform.position.z), Quaternion.identity);
							break;
						}*/
					}
					break;
			}
		}
	}
}
