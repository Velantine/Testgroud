using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;
using UnityEngine.UI;

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
	public GameObject UI;
	public Text infoMessageText;



    //TODO: [Server]
	public void GetShot(int damage)
    {
        Health -= damage;
        RpcUpdateHealth(Health);
    }
	//TODO: [Server]
	public void GetHeald (float healing){
		Health += healing;
		RpcUpdateHealth (Health);
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
			CmdDead ();
			GameObject player = NetworkServer.FindLocalObject(gameObject.GetComponent<NetworkIdentity>().netId);
			player.GetComponent<Collider>().enabled = false;
			//gameObject.GetComponent<FirstPersonController>().enabled = false;
			player.GetComponent<NetworkGun>().enabled = false;
			RespawnB.SetActive(true);
			UI.SetActive(false);
			deaths++;
			player.GetComponent<Screen>().screenLock=false;

		}
		if(Health>100){
			Health = 100;
		}
	}

	public void Respawn(){
		GameObject player = NetworkServer.FindLocalObject(gameObject.GetComponent<NetworkIdentity>().netId);
		RespawnB.SetActive(false);
		UI.SetActive(true);
		player.transform.position =  GameObject.Find ("Spawn").gameObject.transform.position;
		player.GetComponent<Collider>().enabled = true;
		//gameObject.GetComponent<FirstPersonController>().enabled = true;
		player.GetComponent<NetworkGun>().enabled = true;
		Health=100f;
		RpcUpdateHealth (Health);
		dead = false;
		player.GetComponent<Screen>().screenLock=true;
	}

	[Command(channel = 1)]
	public void CmdDead(){
		RpcDead ();
	}

	[ClientRpc(channel = 1)]
	public void RpcDead(){
		infoMessageText.text = (infoMessageText.text+"\r\n"+gameObject.name+" has Died!");
	}



}
