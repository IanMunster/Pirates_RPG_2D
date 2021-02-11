using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

	public static PlayerMove current;
	[SerializeField] private float speed;
	private float yInput;
	private Vector3 velocity;
	public bool canMove;

	//[SerializeField] private  float rotationSpeed;//NEEDED FOR KEYBOARD GAMEPLAY
	//private float xInput; //NEEDED FOR KEYBOARD GAMEPLAY

	void Awake(){
		current = this;
		canMove = true;
	}


	//PhysicsUpdateBehaviour2D once per physics-step
	void FixedUpdate(){
		//Getting the Input needed for movement
		if(canMove){
			yInput = Input.GetAxis ("Vertical");
			//get Current rotation
			Quaternion rot = transform.rotation;
		/*
		//(THIS IS FOR KEYBOARD GAMEPLAY!)
		xInput = Input.GetAxis ("Horizontal");
		//Get the Z from rotation
		float z2 = rot.eulerAngles.z;
		//change Z based on Input 
		z2 -= xInput * rotationSpeed * Time.deltaTime;
		//make the rot Quaternion
		rot = Quaternion.Euler(0,0,z2);
		//(END OF KEYBOARD GAMEPLAY!)
		*/
		//(THIS IS FOR MOUSE GAMEPLAY!)
		//change Z based on Mouseposition
		Vector3 diff = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;
		diff.Normalize ();
		//Make Z in the Degrees of the MousePosition
		float z = Mathf.Atan2 (diff.y, diff.x) * Mathf.Rad2Deg;
		//make the rot Quaternion
		rot = Quaternion.Euler (0f,0f,z-90);
		//(END OF MOUSE GAMEPLAY!)
		//make the Transform rotate to rot
		transform.rotation = rot;
		//move the ship
		Vector3 pos = transform.position;
		velocity = new Vector3 (0, yInput * speed * Time.deltaTime, 0);
		pos += rot * velocity;
		//update position
		transform.position = pos;

		}
	}
}
