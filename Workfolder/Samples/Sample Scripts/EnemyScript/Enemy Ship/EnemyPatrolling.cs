using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyPatrolling : MonoBehaviour {

	[SerializeField] private float minDistance = 5;
	[SerializeField] private GameObject[] waypoints;
	private bool _isPatrolling;
	private int waypointsCount;
	private int crrntWaypoint;
	private Transform patrolDirection;
	private float waypointRange;


	// Use this for initialization
	void Awake () {
		if(waypoints == null){
			waypoints = GameObject.FindGameObjectsWithTag ("WayPoint");
		}
		waypointsCount = waypoints.Length;
		crrntWaypoint = 0;
	}
	public void StartPatrolling(bool IsPatrolling){
		_isPatrolling = IsPatrolling;
	}
	public void StopPatrolling(bool IsPatrolling){
		_isPatrolling = IsPatrolling;
	}

	// Update is called once per frame
	void FixedUpdate () {
		if(_isPatrolling && waypoints != null){
			//patroldirectie is waypoint[crrntwaypoint].positie
			patrolDirection = waypoints[crrntWaypoint].transform;
			//UPDATE THE DISTANCE FROM WAYPOINT
			waypointRange = Vector2.Distance (transform.position, patrolDirection.position);
			//Als de minimale range waypoint = current waypoint +1;
			if(waypointRange < minDistance){
				crrntWaypoint++;
				//crrntWaypoint = Random.Range(0, waypointsCount);
				if(crrntWaypoint >= waypointsCount){
					crrntWaypoint = 0;
				}
			}
			//LOOK AT THE WAYPOINT
			Quaternion rot = Quaternion.LookRotation (patrolDirection.position -  transform.position, transform.TransformDirection(Vector3.forward)*-1);
			transform.rotation = new Quaternion (0f, 0f, rot.z, rot.w);
		}
	}
}