using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour {
	
	public float enemyHealth = 100;
	[SerializeField] private float dmgPerShot = 10;
	[SerializeField] private int pointPerKill = 10;
	private float enemyStartHealth;

	//ANIMATION
	[SerializeField] private Animator hitAnim;
	[SerializeField] private SpriteRenderer shieldSprite;
	private Vector3 color;
	private bool gotHit = false;

	void Awake(){
		enemyStartHealth = enemyHealth;
		hitAnim = hitAnim.GetComponent<Animator>();
		shieldSprite = shieldSprite.GetComponentInChildren<SpriteRenderer> ();
		color.x = shieldSprite.color.r;
		color.y = shieldSprite.color.g;
		color.z = shieldSprite.color.b;
	}

	// Update is called once per frame
	void Update () {
		hitAnim.SetBool ("GotHit", gotHit);
		//ShieldAlpha
		shieldSprite.color = new Color(color.x,color.y,color.z, enemyHealth/enemyStartHealth);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag ("PlayerLazer")) {
			gotHit = true;
			enemyHealth -= dmgPerShot;
			if (enemyHealth == 0) {
                //GET AN EXPLOSION!!
                GameObject enemyExplosion = EnemyExplosionSupply.getExplosion.GetPooledExplosion();
				enemyExplosion.transform.position = transform.position;
				enemyExplosion.transform.rotation = transform.rotation;
				enemyExplosion.SetActive (true);
				uiPlayerScore.current.AddScore (pointPerKill);
				this.gameObject.SetActive (false);
				ResetHealth ();
			}
		}
	}
	void ResetHealth (){
		gotHit = false;
		enemyHealth = enemyStartHealth;
	}
	void OnTriggerExit2D(Collider2D other){
		if (other.CompareTag ("PlayerLazer")) {
			gotHit = false;
			other.gameObject.SetActive (false);
		}
	}
}