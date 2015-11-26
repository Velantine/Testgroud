using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

[NetworkSettings(channel = 1)]
public class NetworkHealth : NetworkBehaviour
{
    [Range(0, 100)]
    [SyncVar]
	public float Health = 100f;
	[SyncVar]
	public float deaths = 0f;
	[SyncVar]
	bool dead = false;
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
			gameObject.GetComponent<FirstPersonController>().enabled = false;
			gameObject.GetComponent<NetworkGun>().enabled = false;
			RespawnB.SetActive(true);
			deaths++;
		}
	}

	public void Respawn(){
		RespawnB.SetActive(false);
		gameObject.transform.position =  GameObject.Find ("Spawn").gameObject.transform.position;
		gameObject.GetComponent<Collider>().enabled = true;
		gameObject.GetComponent<FirstPersonController>().enabled = true;
		gameObject.GetComponent<NetworkGun>().enabled = true;
		Health=100f;
		dead = false;
	}
}
