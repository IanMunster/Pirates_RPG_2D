using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class uiDistanceMeter : MonoBehaviour {

	[SerializeField] private RectTransform navigator;
	[SerializeField] private float minDistance = 50f;
	private GameObject player;
	private GameObject planet;
	private GameObject exit;

	private float planetDistance;
	private float exitDistance;
	private float x;
	private Vector3 navigationDirection;
	private bool passedPlanet;

	// Use this for initialization
	void Awake () {
		player = GameObject.FindWithTag ("Player");
		planet = GameObject.FindWithTag ("EnemyPlanet");
		exit = GameObject.FindWithTag ("LvlTeleport");
		if(exit == null){
			exit = GameObject.FindWithTag ("Finish");
		}
		passedPlanet = false;
	}
		
	// Update is called once per frame
	void FixedUpdate (){
		if(planet != null){
			planetDistance = Vector2.Distance (player.transform.position, planet.transform.position);
			exitDistance = Vector2.Distance (player.transform.position, exit.transform.position);
			if(planetDistance > minDistance && !passedPlanet){
				navigationDirection = planet.transform.position - player.transform.position;
			}
			if(planetDistance <= minDistance){
				passedPlanet = true;
			}
			if(exitDistance <= minDistance){
				navigationDirection = navigationDirection * -1;
			}
		}
		navigationDirection = exit.transform.position - player.transform.position;

		Quaternion rot = navigator.rotation;
		x = navigationDirection.x;
		float z = rot.eulerAngles.z;
		z = x * -1 % 180;
		rot = Quaternion.Euler (0f,0f,z);
		navigator.rotation = rot;
	}
}