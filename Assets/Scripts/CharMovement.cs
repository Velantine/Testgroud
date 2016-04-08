/*using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

public class CharMovement : MonoBehaviour {

	public float speed;
	public float jumpSpeed;
	[SerializeField] public MouseLook m_MouseLook;
	public Camera cam;
	// Use this for initialization
	void Start () {
		m_MouseLook.Init(transform , cam.transform);
	}

	// Update is called once per frame
	void Update () {
		//Vector3 movement = new Vector3 (Input.GetAxis("Horizontal")*speed*Time.deltaTime,Input.GetAxis("Jump")*jumpspeed*Time.deltaTime,Input.GetAxis("Vertical")*speed*Time.deltaTime);
		//this.transform.Translate (new Vector3 (Input.GetAxis("Horizontal")*speed*Time.deltaTime,Input.GetAxis("Jump")*jumpSpeed*Time.deltaTime,Input.GetAxis("Vertical")*speed*Time.deltaTime));
		Vector3 vector3Move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
		transform.Translate(vector3Move * speed * Time.deltaTime);
		RotateView ();
	}


	private void RotateView()
	{
		m_MouseLook.LookRotation (transform, cam.transform);
	}
}*/

using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityStandardAssets.Characters.FirstPerson;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]

public class CharMovement : NetworkBehaviour {

	public float speed = 10.0f;
	public float gravity = 10.0f;
	public float maxVelocityChange = 10.0f;
	public bool canJump = true;
	public float jumpHeight = 2.0f;
	private bool grounded = false;
	private Rigidbody rb;
	[SerializeField] public MouseLook m_MouseLook;



	void Awake () {
		rb = gameObject.GetComponent<Rigidbody> ();
		rb.freezeRotation = true;
		rb.useGravity = false;
		m_MouseLook.Init(transform , Camera.main.transform);
	}

	void FixedUpdate () {
		if (grounded&&isLocalPlayer) {
			// Calculate how fast we should be moving
			Vector3 targetVelocity = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			targetVelocity = transform.TransformDirection(targetVelocity);
			targetVelocity *= speed;

			// Apply a force that attempts to reach our target velocity
			Vector3 velocity = rb.velocity;
			Vector3 velocityChange = (targetVelocity - velocity);
			velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
			velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
			velocityChange.y = 0;
			rb.AddForce(velocityChange, ForceMode.VelocityChange);

			// Jump
			if (canJump && Input.GetButton("Jump")) {
				rb.velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
			}
		}
		RotateView ();

		// We apply gravity manually for more tuning control
		rb.AddForce(new Vector3 (0, -gravity * rb.mass, 0));

		grounded = false;
	}

	void OnCollisionStay () {
		grounded = true;    
	}

	float CalculateJumpVerticalSpeed () {
		// From the jump height and gravity we deduce the upwards speed 
		// for the character to reach at the apex.
		return Mathf.Sqrt(2 * jumpHeight * gravity);
	}


	private void RotateView()
	{
		m_MouseLook.LookRotation (transform, Camera.main.transform);
	}
}