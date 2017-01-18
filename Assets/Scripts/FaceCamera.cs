using System.Collections;
using UnityEngine;

public class FaceCamera : MonoBehaviour {


	void Update () {
		this.transform.LookAt(Camera.main.transform.position);
		this.transform.Rotate(new Vector3(0,180,0));
	}
}
