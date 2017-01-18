using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GamePlayerController : NetworkBehaviour {

	public GameObject bulletPrefab;
	public Transform bulletSpawn;


	void Update () {

		if (!isLocalPlayer) {
			return;
		}
		float x = Input.GetAxis ("Horizontal") * Time.deltaTime * 150.0f;
		float z = Input.GetAxis ("Vertical") * Time.deltaTime * 3.0f;

		transform.Rotate (0, x, 0);
		transform.Translate (0, 0, z);

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
}
