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
	public bool cSit;
	public Transform weaponSpot;
	Options opt;

	
	void Start () {
		siting=false;
		cSit = false;
		opt = GameObject.Find ("Options").GetComponent<Options> ();
	}

	void Update () {
		var hit = new RaycastHit();
		if (Physics.Raycast (Camera.main.ViewportPointToRay (new Vector3 (0.5f, 0.5f, 0f)),out hit, Dist)) {
			var tag = hit.transform.tag;
			switch(tag){
				case "Collect":
				if(Input.GetButtonDown("Interaktion") &&isLocalPlayer){
						pickUp.Play();
						if(hit.transform.gameObject.GetComponent<ObInfo>().obname=="Ammo_Box"){
							NetworkInstanceId id = gameObject.GetComponent<NetworkIdentity>().netId;
							NetworkGun GunScript = thisObj.GetComponent<NetworkGun> ();
							GunScript.WI.ammo = GunScript.WI.ammo + hit.transform.gameObject.GetComponent<ObInfo>().iNumber;
                            CmdDestroy(hit.transform.GetComponent<NetworkIdentity>().netId);
                            break;
						}
						if(hit.transform.gameObject.GetComponent<ObInfo>().obname=="Medkit"){
							NetworkHealth HScript = thisObj.GetComponent<NetworkHealth> ();
							HScript.Health = HScript.Health+hit.transform.gameObject.GetComponent<ObInfo>().iNumber;
                            CmdDestroy(hit.transform.GetComponent<NetworkIdentity>().netId);
							break;
						}
						break;
					}
					break;
				case "Vehicle":
					//Something
					break;
			case "Activate":
				if (Input.GetButtonDown ("Interaktion") &&isLocalPlayer) {
					pickUp.Play ();
					if (hit.transform.gameObject.GetComponent<ObInfo> ().obname == "SpawnAmmo_Box") {
						//GameObject ammoBox = (GameObject)Instantiate (prefabs [0], new Vector3 (hit.transform.position.x, hit.transform.position.y, hit.transform.position.z-2), Quaternion.identity);
						//NetworkServer.Spawn(ammoBox);
						CmdSpawn (0, hit.point);
						break;
					}
					if (hit.transform.gameObject.GetComponent<ObInfo> ().obname == "SpawnMedkit") {
						//Network.Instantiate (prefabs[1], new Vector3(hit.transform.position.x,hit.transform.position.y,hit.transform.position.z-2), transform.rotation, 0);
						CmdSpawn (1, hit.point);
						break;
					}
					if(hit.transform.gameObject.GetComponent<ObInfo>().obname=="SwitchWeapon"){
						if (opt.weapon == (opt.weapons.Length-1)) {
							opt.weapon=0;
						} 
						else {
							opt.weapon++;
						}
						Destroy (GetComponentInChildren<WeaponInfo> ().gameObject);
						GameObject weaponO = Instantiate (opt.weapons [opt.weapon], weaponSpot.position, Quaternion.identity)as GameObject;
						weaponO.transform.SetParent (GetComponentInChildren<Camera>().gameObject.transform);
						weaponO.transform.localEulerAngles=new Vector3(270,0,90);//Quaternion.Euler(270,0,90);
						StartCoroutine (WaitW(1));
						break;
					}
					break;
						
					}
					break;
			case "Sit":
				if (Input.GetButtonDown ("Interaktion") &&isLocalPlayer) {
					this.gameObject.transform.SetParent (hit.transform);
					siting = true;
					StartCoroutine( Wait (2));
					break;
				}
				break;
							}
		}
		if (siting&&Input.GetButtonDown("Interaktion") &&isLocalPlayer&&cSit) {
			this.gameObject.transform.SetParent(GameObject.Find("World").transform);
			siting = false;
			cSit = false;
		}
	}



	IEnumerator Wait(float sec){
		yield return new WaitForSeconds (sec);
		cSit = true;
	}
	IEnumerator WaitW(float sec){
		yield return new WaitForSeconds (sec);
		gameObject.GetComponent<NetworkGun> ().WeaponUpdateInfo ();
	}


	[Command(channel=1)]
	void CmdSpawn(int pre, Vector3 pos){
		RpcSpawn (pre, pos);
	}

	[ClientRpc(channel = 1)]
	private void RpcSpawn(int objectToSpawn, Vector3 pos)
	{
		SpawnOb (objectToSpawn, pos);
	}

	void SpawnOb(int pre, Vector3 pos){
		var sobject = (GameObject)Instantiate (prefabs [pre], pos, Quaternion.identity);
	}

	void CmdHeal(NetworkInstanceId id, Transform hit){
		GameObject player = NetworkServer.FindLocalObject(id);
		var healthScript = player.GetComponent<NetworkHealth>();
		healthScript.GetHeald(hit.gameObject.GetComponent<ObInfo>().iNumber);
	}

	void CmdAmmo(NetworkInstanceId id, Transform hit){
		GameObject player = NetworkServer.FindLocalObject(id);
		var gunScript = player.GetComponent<NetworkGun>();
		gunScript.GetAmmo(hit.gameObject.GetComponent<ObInfo>().iNumber);
	}


    [Command(channel = 1)]
    void CmdDestroy(NetworkInstanceId id)
    {
        RpcDestroy(id);
    }

    [ClientRpc(channel = 1)]
    private void RpcDestroy(NetworkInstanceId id)
    {
        DestroyOb(id);
    }

    void DestroyOb(NetworkInstanceId id)
    {
        DestroyImmediate(NetworkServer.FindLocalObject(id));
    }
}