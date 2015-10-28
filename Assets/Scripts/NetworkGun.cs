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

    public Transform Muzzle;
    public GameObject WallParticlePrefab;
    public GameObject PlayerParticlePrefab;
	public AudioSource Shot;

    [ClientCallback]
    void Update()
    {
		if (Input.GetButtonDown("Fire1") && isLocalPlayer)
        {
			ShootSound();
			var ray = new Ray(Muzzle.position, Muzzle.right);
            var hit = new RaycastHit();
			if (Physics.Raycast(Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f)), out hit, MaxBulletDist))
            {
                var tag = hit.transform.tag;
                switch (tag)
                {
                    case "Player":
                        //ShowParticles(PlayerParticlePrefab, hit.point, hit.normal, BloodParticleTime);
                        CmdInvokeParticle(hit.transform.tag, hit.point, hit.normal);
                        NetworkInstanceId id = hit.transform.GetComponent<NetworkIdentity>().netId;
                        CmdShoot(id);
                        break;
                    default:
                        CmdInvokeParticle(hit.transform.tag, hit.point, hit.normal);
                        break;
                }
            } 
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
        Destroy(particles, time);
    }


	void ShootSound()
	{
		if (Shot.isPlaying) return;
		Shot.clip = shootGun2[Random.Range(0,shootGun2.Length)];
		Shot.Play();
	}

}
