using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[NetworkSettings(channel = 1)]
public class LookAt : NetworkBehaviour {
	public float Dist;
	public int AmmunitionC;
	public int HealthC;
	public AudioSource pickUp;
	public GameObject thisObj;
	public GameObject[] prefabs;
	public bool siting;

	
	void Start () {
		siting=false;
	}

	void Update () {
		var hit = new RaycastHit();
		if (Physics.Raycast (Camera.main.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0f)),out hit, Dist)) {
			var tag = hit.transform.tag;
			switch(tag){
				case "Collect":
				if(Input.GetButtonDown("Enter")&&isLocalPlayer){
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
				if (Input.GetButtonDown ("Enter")&&isLocalPlayer) {
					pickUp.Play ();
					if (hit.transform.gameObject.GetComponent<ObInfo> ().name == "SpawnAmmo_Box") {
						//GameObject ammoBox = (GameObject)Instantiate (prefabs [0], new Vector3 (hit.transform.position.x, hit.transform.position.y, hit.transform.position.z-2), Quaternion.identity);
						//NetworkServer.Spawn(ammoBox);
						CmdSpawn (0, hit.transform.gameObject);
						break;
					}
					if (hit.transform.gameObject.GetComponent<ObInfo> ().name == "SpawnMedkit") {
						//Network.Instantiate (prefabs[1], new Vector3(hit.transform.position.x,hit.transform.position.y,hit.transform.position.z-2), transform.rotation, 0);
						CmdSpawn (1, hit.transform.gameObject);
						break;
					}
						//if(){}
						
					}
					break;
			case "Sit":
				if (Input.GetButtonDown ("Enter")&&isLocalPlayer) {
					this.gameObject.transform.SetParent (hit.transform);
					siting = true;
					s (2);
					break;
				}
				break;
							}
		}
		if (siting&&Input.GetButtonDown("Enter")&&isLocalPlayer) {
			this.gameObject.transform.SetParent(GameObject.Find("World").transform);
			siting = false;
		}
	}

	[Command(channel=1)]
	void CmdSpawn(int pre, GameObject pos){
		if (isLocalPlayer) {
			GameObject objectToSpawn = (GameObject)Instantiate (prefabs [pre], new Vector3 (pos.transform.position.x, pos.transform.position.y, pos.transform.position.z - 2), Quaternion.identity);
			NetworkServer.Spawn(objectToSpawn);
		} else {
			GameObject objectToSpawn = (GameObject)Instantiate (prefabs [pre], gameObject.transform.position, Quaternion.identity);
			NetworkServer.Spawn(objectToSpawn);
		}
	}

	IEnumerator s(float time){
		yield return new WaitForSeconds (time);
	}
}