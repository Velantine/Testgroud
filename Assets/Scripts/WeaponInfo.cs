using UnityEngine;
using System.Collections;

public class WeaponInfo : MonoBehaviour {

	public AudioClip[] shootAudio;
	public float MaxBulletDist;
	public float fireRate;
	public bool projectile;
	public Transform muzzle;
	public GameObject projectilePrefab;
	public float speed;
    public float ammo;
}
