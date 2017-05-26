using UnityEngine;
using System.Collections;

public class Cam : MonoBehaviour {

	public float speed;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.localRotation = Quaternion.Euler(transform.localRotation.eulerAngles+ Vector3.up*speed*Input.GetAxis("Horizontal")+
			Vector3.right*speed*Input.GetAxis("Vertical"));
	}
}
