using UnityEngine;
using System.Collections;

public class TargetFollow : MonoBehaviour {

	private Transform target;
    private Vector3 offset;

	void Awake () {
		GameObject playerObj = GameObject.FindWithTag ("Player");
		if (playerObj != null) {
			target = playerObj.transform;
			offset = transform.position - target.position;
		}
		if (playerObj == null) {
			Debug.Log ("Cannot find the 'Player' in Scene");
		}
	}
	
	
	void LateUpdate () {
		//ONLY MOVE THE CAMERA TO POSITION WHEN THE PLAYER IS ALIVE
		if (target != null) {
			transform.position = target.position + offset;
		}
        
	
	}
}
