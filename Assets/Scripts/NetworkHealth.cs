using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

[NetworkSettings(channel = 1)]
public class NetworkHealth : NetworkBehaviour
{
	[SyncVar]
    [Range(0, 100)]
	public float Health = 100f;
	[SyncVar]
	public float deaths = 0f;
	[SyncVar]
	public bool dead = false;
	public GameObject RespawnB;



    //TODO: [Server]
    public void GetShot()
    {
        Health -= 20;
        RpcUpdateHealth(Health);
    }

    [ClientRpc(channel = 1)]
    private void RpcUpdateHealth(float health)
    {
        Health = health;
    }

	void Update(){
		if(Health<=0f&&dead==false){
			dead=true;
			print("Somebody died!");
			gameObject.GetComponent<Collider>().enabled = false;
			//gameObject.GetComponent<FirstPersonController>().enabled = false;
			gameObject.GetComponent<NetworkGun>().enabled = false;
			RespawnB.SetActive(true);
			deaths++;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			gameObject.GetComponent<Screen>().screenLock=false;
		}
		if(Health>100){
			Health = 100;
		}
	}

	public void Respawn(){
		RespawnB.SetActive(false);
		//gameObject.transform.position =  GameObject.Find ("Spawn").gameObject.transform.position;
		gameObject.GetComponent<Collider>().enabled = true;
		//gameObject.GetComponent<FirstPersonController>().enabled = true;
		gameObject.GetComponent<NetworkGun>().enabled = true;
		Health=100f;
		dead = false;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		gameObject.GetComponent<Screen>().screenLock=true;
	}
}
