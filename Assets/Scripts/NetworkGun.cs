using UnityEngine;
using System.Collections;
using System.Security.Policy;
using UnityEngine.Networking;
using UnityEngine.Audio;

[NetworkSettings(channel = 1)]
public class NetworkGun : NetworkBehaviour
{
	public AudioClip[] shootAudio;
	public AudioMixerGroup amg;
    public GameObject explObj;


	private float MaxBulletDist = 100;
    public float WallParticleTime = 2;
    public float BloodParticleTime = 2;


	private Transform Muzzle;
    public GameObject WallParticlePrefab;
    public GameObject PlayerParticlePrefab;
	public GameObject lightprefab;
	public float projectileDelTime;
	private float fireRate;
	private float nextFire;
	private bool projectile;
	private GameObject projectilePrefab;
	private float speed;

	[SerializeField]public WeaponInfo WI;
	public LineRenderer line;

	void Start(){
		line = GameObject.Find("Laser").GetComponent<LineRenderer>();
		line.enabled = false;

		WeaponUpdateInfo ();
        
	}
	
	
	
    [ClientCallback]
    void Update()
    {
		//Laser
		if (Input.GetButton("Fire1")&&isLocalPlayer&&WI.ammo>0&&Time.time>nextFire&&!projectile)
        {
			nextFire = Time.time + fireRate;
            WI.ammo = WI.ammo - 1;
            line.enabled = true;
            ShootSound();
            //var ray = new Ray(Muzzle.position, Muzzle.right);
            var hit = new RaycastHit();
			if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), out hit, MaxBulletDist))
            {
				//print("Hit: "+ hit.transform.position.ToString());
				line.SetPosition(0, Muzzle.transform.position);
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
                        GameObject expo = Instantiate(explObj,hit.transform.position,hit.transform.rotation);
                        Destroy(hit.transform.gameObject);
                        Destroy(expo, 2f);
						break;
                    default:
                        CmdInvokeParticle(hit.transform.tag, hit.point, hit.normal);
                        break;
                }
            }
			else{
				//print("Hit Noting");
				line.SetPosition(0, Muzzle.transform.position);
                GameObject player = NetworkServer.FindLocalObject(this.transform.GetComponent<NetworkIdentity>().netId);
                line.SetPosition(1, player.GetComponentInChildren<Camera>().transform.forward*WI.MaxBulletDist);
			}
        }
		//Projectile
		if (Input.GetButton ("Fire1") && isLocalPlayer && WI.ammo > 0 && Time.time > nextFire && projectile) {
			nextFire = Time.time + fireRate;
            WI.ammo = WI.ammo - 1;
            CmdShootProjectile (this.transform.GetComponent<NetworkIdentity>().netId);

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
        healthScript.GetShot(20);
    }

	//TODO: [Server]
	public void GetAmmo (float ammocount){
		WI.ammo+=ammocount;
		RpcUpdateAmmo (WI.ammo);
	}


	[ClientRpc(channel = 1)]
	private void RpcUpdateAmmo(float ammo)
	{
        WI.ammo = ammo;

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
		AudioSource shot = gameObject.AddComponent<AudioSource> ();
		shot.outputAudioMixerGroup=amg;
		shot.clip = shootAudio[Random.Range(0,shootAudio.Length)];
		shot.Play();
		Destroy (shot, 2);
	}

	IEnumerator KillLine(){
		yield return new WaitForSeconds(.05f);
		line.enabled = false;
	}
		

	public void WeaponUpdateInfo(){
		WI = GetComponentInChildren<WeaponInfo> ();
		Muzzle = WI.muzzle;
		shootAudio = WI.shootAudio;
		fireRate = WI.fireRate;
		MaxBulletDist = WI.MaxBulletDist;
		projectile = WI.projectile;
		projectilePrefab = WI.projectilePrefab;
		speed = WI.speed;


    }

    //Projectile
	[Command(channel=1)]
	void CmdShootProjectile(NetworkInstanceId id){
		RpcShootProjectile (id);
	}

	[ClientRpc(channel = 1)]
	private void RpcShootProjectile(NetworkInstanceId id)
	{
		ShootProjectile (id);
	}

	void ShootProjectile(NetworkInstanceId id){
        ShootSound();
        GameObject player = NetworkServer.FindLocalObject(id);
        GameObject projectileS = Instantiate (Resources.Load("Prefabs/"+ player.GetComponentInChildren<WeaponInfo>().projectilePrefab.name), player.GetComponentInChildren<WeaponInfo>().muzzle.transform.position, player.GetComponentInChildren<WeaponInfo>().muzzle.transform.localRotation) as GameObject;
		//projectileS.transform.position = player.GetComponentInChildren<WeaponInfo>().muzzle.transform.position;
        //projectileS.transform.forward = player.GetComponentInChildren<WeaponInfo>().muzzle.transform.forward;
        Rigidbody rb = projectileS.GetComponent<Rigidbody> ();
		rb.velocity = player.GetComponentInChildren<Camera>().transform.forward * player.GetComponentInChildren<WeaponInfo>().speed;
	}

}
