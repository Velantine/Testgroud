using UnityEngine;
using System.Collections;
using System.Security.Policy;
using UnityEngine.Networking;

[NetworkSettings(channel = 1)]
public class NetworkGun : NetworkBehaviour
{
	public AudioClip[] shootGun2;


    public float MaxBulletDist = 100;
    public float WallParticleTime = 2;
    public float BloodParticleTime = 2;

	[SyncVar]
	public float Ammunition;

    public Transform Muzzle;
    public GameObject WallParticlePrefab;
    public GameObject PlayerParticlePrefab;
	public GameObject lightprefab;
	public AudioSource Shot;
	public float fireRate;
	private float nextFire;

	public LineRenderer line;

	void Start(){
		line = GameObject.Find("Laser").GetComponent<LineRenderer>();
		line.enabled = false;
	}
	
	
	
    [ClientCallback]
    void Update()
    {

		if (Input.GetButton("Fire1") && isLocalPlayer && Ammunition>0&&Time.time>nextFire)
        {
			nextFire = Time.time + fireRate;
			Ammunition=Ammunition-1;
			line.enabled = true;
			ShootSound();
			var ray = new Ray(Muzzle.position, Muzzle.right);
            var hit = new RaycastHit();
			if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), out hit, MaxBulletDist))
            {
				//print("Hit: "+ hit.transform.position.ToString());
				line.SetPosition(0, ray.origin);
				line.SetPosition(1, hit.point);
                var tag = hit.transform.tag;
                switch (tag)
                {
                    case "Player":
                        //ShowParticles(PlayerParticlePrefab, hit.point, hit.normal, BloodParticleTime);
                        CmdInvokeParticle(hit.transform.tag, hit.point, hit.normal);
                        NetworkInstanceId id = hit.transform.GetComponent<NetworkIdentity>().netId;
                        CmdShoot(id);
						break;
					case "Destructable":
						//STUFF
						break;
                    default:
                        CmdInvokeParticle(hit.transform.tag, hit.point, hit.normal);
                        break;
                }
            }
			else{
				//print("Hit Noting");
				line.SetPosition(0, ray.origin);
				line.SetPosition(1, Camera.main.transform.forward);
			}
        }
		if (line.enabled) {
			StartCoroutine (KillLine());
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
        healthScript.GetShot();
    }

	//TODO: [Server]
	public void GetAmmo (float ammocount){
		Ammunition+=ammocount;
		RpcUpdateAmmo (Ammunition);
	}


	[ClientRpc(channel = 1)]
	private void RpcUpdateAmmo(float ammo)
	{
		Ammunition=ammo;
	}




    [Command(channel = 1)]
    private void CmdInvokeParticle(string mat, Vector3 pos, Vector3 normal)
    {
        RpcInvokeParticle(mat, pos, normal);
    }

    [ClientRpc(channel = 1)]
    private void RpcInvokeParticle(string mat, Vector3 pos, Vector3 normal)
    {
        GameObject prefab;
        float time;
        switch (mat)
        {
            case "Player":
                prefab = PlayerParticlePrefab;
                time = BloodParticleTime;
                break;
            default:
                prefab = WallParticlePrefab;
                time = WallParticleTime;
                break;
        }
        ShowParticles(prefab, pos, normal, time);
    }

    public void ShowParticles(GameObject prefab, Vector3 pos, Vector3 normal, float time)
    {
        var particles = Instantiate(prefab, pos, Quaternion.LookRotation(normal));
		var lightEffect = Instantiate (lightprefab, pos, Quaternion.LookRotation (normal));
        Destroy(particles, time);
		Destroy (lightEffect, .05f);
    }


	void ShootSound()
	{
		Shot.clip = shootGun2[Random.Range(0,shootGun2.Length)];
		Shot.Play();
	}

	IEnumerator KillLine(){
		yield return new WaitForSeconds(.05f);
		line.enabled = false;
	}

}
