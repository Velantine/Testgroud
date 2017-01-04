using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class Bullet : NetworkBehaviour {
	public float destroyTime;
	public int damage;
    public GameObject explObj;

    // Use this for initialization
    void Start () {
		Destroy (gameObject, destroyTime);
	}

	void OnCollisionEnter(Collision col){
		switch (col.gameObject.tag){
		case "Player":
			CmdShoot (col.gameObject.GetComponent<NetworkIdentity> ().netId);
			Destroy (gameObject, 0);
			break;
        case "Destructable":
            GameObject expo = Instantiate(explObj, col.transform.position, col.transform.rotation);
            Destroy(col.transform.gameObject);
            Destroy(expo, 2f);
            break;
        default:
			break;
		}
	}


	[Command(channel = 1)]
	private void CmdShoot(NetworkInstanceId id)
	{
		GameObject player = NetworkServer.FindLocalObject(id);
		var healthScript = player.GetComponent<NetworkHealth>();
		if (healthScript == null)
		{
			Debug.LogError("no hleathscript attached to player");
			return;
		}
		healthScript.GetShot(damage);
	}
}
