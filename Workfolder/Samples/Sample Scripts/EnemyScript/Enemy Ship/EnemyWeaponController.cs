using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyWeaponController : MonoBehaviour {
	/*THE MUZZLEPOINT OF THE ENEMY*/
	[SerializeField] private List<Transform> enemyCannons;
	/*ENEMY'S FIRE RATE(COULD BE A RANDOM RANGE!!)*/
	[SerializeField]private float fireRate;
	/*ENEMY's FIRE DELAY (SO IT DOENST START SHOOTING FROM THE START)*/
	[SerializeField]private float fireDelay;
	//ENEMY CANNON ANIMATIONS
	private Animator[] cannonAnimators;

	void Start() {
		cannonAnimators = new Animator[enemyCannons.Count];
		for (int i = 0; i < cannonAnimators.Length; i++) {
			cannonAnimators [i] = enemyCannons [i].GetComponent<Animator> ();
		}
	}

	public void StartFiring(){
		/*INVOKES A FUNCTION, BY NAME IN X SECONDS, THEN REPEAT EVERY X*RATE*/
		InvokeRepeating ("Fire", fireDelay, fireRate);
	}

	public void StopFiring(){
		CancelInvoke ();
	}

	/*ENEMY FIRING FUNCTION*/
	void Fire () {		
		/*SET THE POOLED LAZER TO ACTIVE*/
		for (int i = 0; i < enemyCannons.Count; i++) {
			GameObject enemylazer = EnemyLazerSupply.getSupply.GetPooledObject();
			if (enemylazer == null) {
				return;
			} else {
				enemylazer.transform.position = enemyCannons[i].position;
				enemylazer.transform.rotation = enemyCannons[i].rotation;
				enemylazer.SetActive (true);
				cannonAnimators [i].SetTrigger ("IsFiringTrigger");
			}
		}
	}

	//IF THE ENEMY GOT DISABLED, STOP FIRING
	void OnDisable(){
		CancelInvoke ();
	}
}