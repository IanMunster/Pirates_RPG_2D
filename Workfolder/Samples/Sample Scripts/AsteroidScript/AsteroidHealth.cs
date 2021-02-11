using UnityEngine;
using System.Collections;

public class AsteroidHealth : MonoBehaviour {

	[SerializeField] private float asteroidHealth = 100;
	[SerializeField] private float dmgPerShot = 25;
	[SerializeField] private int pointPerKill = 10;

	void Start(){

	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("PlayerLazer")){
			asteroidHealth -= dmgPerShot;
			other.gameObject.SetActive (false);
			if(asteroidHealth == 0){
				//GET A EXPLOSION
				GameObject asteroidExplosion = AsteroidExplosion.getExplosion.GetPooledExplosion();
				asteroidExplosion.transform.position = transform.position;
				asteroidExplosion.transform.rotation = transform.rotation;
				asteroidExplosion.SetActive (true);

				gameObject.SetActive (false);
				ResetHealth ();
				uiPlayerScore.current.AddScore (pointPerKill);
			}
		}
	}
	void ResetHealth (){
		asteroidHealth = 100;
	}
}
