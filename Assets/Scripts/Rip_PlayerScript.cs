using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class Rip_PlayerScript : MonoBehaviour {

	private Animator anim;
	public float speed = 10f;            // The speed that the player will move at.
	Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
	private float inputH;
	private float inputV;
	private bool shooting;


	void Start () {

		anim = GetComponent<Animator> ();
		playerRigidbody = GetComponent<Rigidbody> ();
	}


	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown(0)){
			shooting = true;
			print ("I pressed 1");
			anim.Play("standing_aim_overdraw", -1, 0f);
		}
		else
		{
			shooting = false;
		}

		inputH = Input.GetAxis ("Horizontal");
		inputV = Input.GetAxis ("Vertical");

		anim.SetFloat ("inputH", inputH);
		anim.SetFloat ("inputV", inputV);


		var moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
		if (!shooting) {
			playerRigidbody.MovePosition (playerRigidbody.position + moveDirection * Time.deltaTime);
		}
	}




	void Move ()
	{
		var moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
		moveDirection = Camera.main.transform.TransformDirection(moveDirection);
		moveDirection.y = 0;
		anim.SetFloat("vSpeed",Input.GetAxis("Vertical"));
		anim.SetFloat("hSpeed",Input.GetAxis("Horizontal"));
		playerRigidbody.MovePosition(playerRigidbody.position + moveDirection * speed * Time.deltaTime);
	}



}
