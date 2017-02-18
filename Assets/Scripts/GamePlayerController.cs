using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GamePlayerController : NetworkBehaviour {

	public GameObject bulletPrefab;
	public Transform bulletSpawn;
	public float speed = 6f;            // The speed that the player will move at.
	Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
	float camRayLength = 100f;          // The length of the ray from the camera into the scene.


	[SyncVar]
	public string pname = "player";

	void OnGUI()
	{
		if(isLocalPlayer)
		{
			pname = GUI.TextField(new Rect (25,Screen.height - 40,100,30), pname);
			if(GUI.Button(new Rect(130,Screen.height - 40,80,30), "Change"))
			{
				CmdChangeName(pname);
			}
		}
	}

	[Command]
	public void CmdChangeName(string newName)
	{
		pname = newName;
	}
	void Awake ()
	{
		playerRigidbody = GetComponent <Rigidbody> ();
	}
	void Start () {
        if (isLocalPlayer)
	SmoothCameraFollow.target = this.transform;

	}

	void Update () {

		if (!isLocalPlayer) {
			return;
		}
		this.GetComponentInChildren<TextMesh>().text = pname;
		Move ();
		Turning ();

		if (Input.GetMouseButtonDown (0)) 
		{
			CmdFire();
		}	


	}

	[Command]
	void CmdFire()
	{
		//Creating the bullet from prefab
		GameObject bullet = (GameObject)Instantiate (bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);

		//Add velocity to the bullet
		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward*6.0f;

		//Spawn the bullet on the Clients
		NetworkServer.Spawn(bullet);

		//Destroy bullet after 2 seconds
		Destroy(bullet,2);
	}

	public override void OnStartLocalPlayer()
	{
		GetComponent<MeshRenderer> ().material.color = Color.blue;
	}

	void Turning ()
	{
		// Create a ray from the mouse cursor on screen in the direction of the camera.
		Ray camRay = Camera.main.ScreenPointToRay (Input.mousePosition);

		// Create a RaycastHit variable to store information about what was hit by the ray.
		RaycastHit floorHit;

		// Perform the raycast and if it hits something on the floor layer...
		if(Physics.Raycast (camRay, out floorHit, camRayLength))
		{
			// Create a vector from the player to the point on the floor the raycast from the mouse hit.
			Vector3 playerToMouse = floorHit.point - transform.position;

			// Ensure the vector is entirely along the floor plane.
			playerToMouse.y = 0f;

			// Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
			Quaternion newRotation = Quaternion.LookRotation (playerToMouse);

			// Set the player's rotation to this new rotation.
			playerRigidbody.MoveRotation (newRotation);
		}
	}


	void Move ()
	{
		var moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
		moveDirection = Camera.main.transform.TransformDirection(moveDirection);
		moveDirection.y = 0;


		playerRigidbody.MovePosition(playerRigidbody.position + moveDirection * speed * Time.deltaTime);
	}

}
