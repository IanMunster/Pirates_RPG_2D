using UnityEngine;
using System.Collections;

public class EnemyLookAtPlayer : MonoBehaviour {

	private Transform player;
	public Transform turret;
	// Use this for initialization
	void Awake () {
		GameObject playerObj = GameObject.FindGameObjectWithTag ("Player");
		if(playerObj != null){
			player = playerObj.transform;
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if(player != null){
			Quaternion rot = Quaternion.LookRotation (player.position -  transform.position, transform.TransformDirection(Vector3.forward)*-1);
			turret.rotation = new Quaternion (0f, 0f, rot.z, rot.w);
		}

	}
}
