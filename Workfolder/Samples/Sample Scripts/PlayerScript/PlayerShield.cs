using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerShield : MonoBehaviour {

	[SerializeField] private float shieldCapacity;// = 100;
	[SerializeField] private float maxShield = 100;
	private PlayerLifes _playerlifes;
	//animation vars
	[SerializeField] private Animator hitAnim;
	[SerializeField] private SpriteRenderer shieldSprite;
	private Vector3 color; //X=Red, Y=Green, Z=Blue
	[SerializeField] private GameObject playerExplosion;
	private AudioSource hitSource;
	[SerializeField] private AudioClip hitSound;
	private int repeat;
	private float timeOut;
	private float waitTimeOut;
	//DAMAGE VAR
	[SerializeField] private bool canGetDamage = true;
	private bool isGettingDamage = false;
	private bool interuptDamage = false;
	private float damageAmount = 0f;
	private float damageDuration = 0f;
	//REGEN VAR
	[SerializeField] private bool canRegen = true;
	private bool isRegenerating = false;
	private bool interuptRegen = false;
	private float regenWarmUp;
	private float regenAmount;
	private float regenDuration;

	// Use this for initialization
	void Start () {
		//GET THE PLAYER
		GameObject playerObj = GameObject.FindWithTag ("Player");
		if(playerObj != null){
			_playerlifes = playerObj.GetComponent<PlayerLifes> ();
		}
		hitAnim = hitAnim.GetComponent<Animator> ();
		hitSource = GetComponent <AudioSource> ();
		timeOut = 0.0f;
		waitTimeOut = 2f;
		shieldCapacity = 100;
		regenWarmUp = 5;
		regenAmount = 100;
		regenDuration = 50;
		color.x = shieldSprite.color.r;
		color.y = shieldSprite.color.g;
		color.z = shieldSprite.color.b;
	}

	// Update is called once per frame
	void Update(){
		hitAnim.SetBool ("GotHit", isGettingDamage);
		uiPlayerShield.current.AddShieldValue (shieldCapacity);
		if(isGettingDamage && !hitSource.isPlaying && repeat == 0){
			repeat = 1;
			hitSource.PlayOneShot (hitSound);
		}
		if (!hitSource.isPlaying && Time.time > timeOut){
			timeOut = Time.time + waitTimeOut;
			repeat = 0;
		}
	}

	void FixedUpdate (){
		shieldSprite.color = new Color (color.x,color.y,color.z,shieldCapacity/100);
		if(shieldCapacity < 20){
			shieldSprite.color = new Color (color.x,color.y,color.z,0);
			uiPlayerAvater.current.UpdateAvatar ("LowLife");
			uiPlayerWarning.current.AddWarningTxt ("WARNING! SHIELD DEPLETED!", 3f, new Vector3(99,00,00));
		}
		if (shieldCapacity <= 0f) {
			shieldCapacity = 0;
			StopAllCoroutines ();
			//PLAYER SHIELD LOST
			Instantiate(playerExplosion, transform.position,transform.rotation);

			_playerlifes.LoseLife ();
		} else if (!isGettingDamage && !isRegenerating && shieldCapacity > 0 && shieldCapacity < 100 && !interuptRegen) {
			uiPlayerAvater.current.UpdateAvatar ("Normal");
			canRegen = true;
			RegenerateShield (5, 100, 50);
		}
		if(shieldCapacity >= maxShield){
			shieldCapacity = maxShield;
			StopRegeneration ();
		}
	}
	public void ResetShield () {
		shieldCapacity = maxShield;
	}

//CHECK COLLISION!
	void OnTriggerEnter2D(Collider2D other){
		switch(other.gameObject.tag){
		case "EnemyLazer":
			RepeatDamagingShield (10, 1);
			other.gameObject.SetActive (false);
			break;
		case "Asteroid":
			RepeatDamagingShield (20, 1);
			break;
		default:
			return;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.CompareTag ("Asteroid") || other.CompareTag("Sun")) {
			StopRepeatDMG ();
		}
	}

//DAMAGE THE PLAYER
	public void RepeatDamagingShield(float damageAmount, float damageDuration){
		interuptRegen = true;
		canRegen = false;
		uiPlayerAvater.current.UpdateAvatar ("Hit");
		if (!isGettingDamage && canGetDamage) {
			StartCoroutine (DamageShield (damageAmount, damageDuration));
		}
	}

	IEnumerator DamageShield(float damageAmount,float  damageDuration){
		StopRegeneration ();

		if (shieldCapacity > 0f && !interuptDamage) {
			isGettingDamage = true;
			float amountDMG = 0;
			float DMGperLoop = damageAmount / damageDuration;
			while (amountDMG < damageAmount && !interuptDamage) {
				shieldCapacity -= DMGperLoop;
				amountDMG += DMGperLoop;
				yield return new WaitForSeconds (1f);
			}
			isGettingDamage = false;
			interuptRegen = false;
		}
	}

	public void StopRepeatDMG (){
		StopCoroutine (DamageShield (damageAmount, damageDuration));
		if(isGettingDamage){
			isGettingDamage = false;
		}
	}

//REGENERATE THE PLAYERS SHIELD
	public void RegenerateShield(float regenWarmUp, float regenAmount, float regenDuration){
		if(!isRegenerating && canRegen){
			StartCoroutine (Regeneration (regenWarmUp, regenAmount, regenDuration));
		}
	}

	IEnumerator Regeneration(float regenWarmUp, float regenAmount, float regenDuration){
		if (shieldCapacity < maxShield && !interuptRegen && canRegen) {
			isRegenerating = true;
			yield return new WaitForSeconds (regenWarmUp);
			float amountRGN = 0;
			float RGNperLoop = regenAmount / regenDuration;
			while (amountRGN < regenAmount && !interuptRegen) {
				shieldCapacity += RGNperLoop;
				amountRGN += RGNperLoop;
				yield return new WaitForSeconds (1f);
			}
		} else if (shieldCapacity >= 100) {
			shieldCapacity = 100;
			isRegenerating = false;
			yield return null;
		} else if (interuptRegen) {
			StopRegeneration ();
			yield return null;
		} else {
			yield return interuptDamage = false;
		}
	}
	void StopRegeneration(){
		StopCoroutine (Regeneration(regenWarmUp, regenAmount, regenDuration));
		isRegenerating = false;
	}
}