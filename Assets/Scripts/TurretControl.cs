using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Audio;

public class TurretControl : NetworkBehaviour {

    public GameObject lifter;
    public GameObject turret;
    public float mouseSensitivity = 100.0f;
    public float rotY = 0.0f;
    public float rotX = 0.0f;

    public float ammunation;
    public float firerate;
    public AudioClip shootsound;
    public AudioMixerGroup amg;
    private float nextFire;
    public float fireRate;

    //Temp
    public GameObject cam;



    // Update is called once per frame
    public void Update () {
       //Movement -Start
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = -Input.GetAxis("Mouse Y");
        rotY += mouseX * mouseSensitivity * Time.deltaTime;
        rotX += mouseY * mouseSensitivity * Time.deltaTime;
        rotX = Mathf.Clamp(rotX, -15,12);
        turret.transform.rotation = Quaternion.Euler(270, rotY,0);
        lifter.transform.localRotation = Quaternion.Euler(90, -rotX, 0);
        //Movemend - End
        //Shoot -Start
        if (Input.GetButton("Fire1") && ammunation > 0 && Time.time > nextFire && isLocalPlayer) {
            nextFire = Time.time + fireRate;
            ammunation = ammunation - 1;
        }
        //Temporär
        if (Input.GetButtonDown("Jump")) {
            if (!cam.activeInHierarchy)
            {
                cam.SetActive(true);
            }
            else {
                cam.SetActive(false);
            }

            
        }
    }

    void ShootSound()
    {
        AudioSource shot = gameObject.AddComponent<AudioSource>();
        shot.outputAudioMixerGroup = amg;
        shot.clip = shootsound;
        shot.Play();
        Destroy(shot, 2);
    }

    //TODO: [Server]
    public void GetAmmo(float ammocount)
    {
        ammunation += ammocount;
        RpcUpdateAmmo(ammunation);
    }

    [ClientRpc(channel = 1)]
    private void RpcUpdateAmmo(float ammo)
    {
        ammunation = ammo;
    }


}
