using UnityEngine;
using System.Collections;

public class EnemyChasing : MonoBehaviour {

	[SerializeField] private float rotationSpeed = 10f;
	private bool _isChasing;
	private Transform _target;

	// Use this for initialization
	//GET THE PLAYERS POSITION
	void Start(){
		GameObject playerObj = GameObject.FindGameObjectWithTag ("Player");
		if(playerObj != null){
			_target = playerObj.transform;
		}
	}

	public void StartChasing (bool IsChasing, Transform target){
		//GIVE ALL THE VARS TO PRIVATE VAR
		_isChasing = IsChasing;
		_target = target;
	}

	public void StopChasing (bool IsChasing, Transform target){
		_isChasing = IsChasing;
		_target = target;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if(_isChasing){
			//LOOK AT THE PLAYER
			Quaternion rot = Quaternion.LookRotation (_target.position -  transform.position, transform.TransformDirection(Vector3.forward)*-rotationSpeed);
			transform.rotation = new Quaternion (0f, 0f, rot.z, rot.w);
		}
	}
}