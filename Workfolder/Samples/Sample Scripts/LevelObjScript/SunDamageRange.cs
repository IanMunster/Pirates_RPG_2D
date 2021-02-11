using UnityEngine;
using System.Collections;

public class SunDamageRange : MonoBehaviour {

	[SerializeField] private float sunDmg = 20f;
	[SerializeField] private float dmgTime = 1f;

	private float _sunDmg;
	private float _dmgTime;
	private float nxtDmg = 0.0f;
	private float nxtRate = 0.5f;

	private GameObject player;
	private PlayerShield _playerShield;

    //audio
    private AudioSource source;
    public AudioClip sunSound;

    void Awake() {
		source = GetComponent<AudioSource>();
		player = GameObject.FindWithTag ("Player");
		if(player != null){
			_playerShield = player.GetComponent<PlayerShield> ();
		}
    }
		
	void OnTriggerEnter2D (Collider2D other){
		if(other.CompareTag("Player")){
			_dmgTime = dmgTime;
			_sunDmg = sunDmg;
            source.PlayOneShot(sunSound, 0.6f);
        }
	}
	void OnTriggerStay2D (Collider2D other){
		if(other.CompareTag("Player")){
            _playerShield.RepeatDamagingShield (_sunDmg, _dmgTime);
			if(Time.time > nxtDmg){
				nxtDmg = Time.time + nxtRate;
				_sunDmg++;
				_dmgTime-= 0.1f;
			}
		}
	}

	void OnTriggerExit2D (Collider2D other){
		if(other.CompareTag("Player")){
			_sunDmg = 0;
			_dmgTime = 0;
			_playerShield.StopRepeatDMG ();
            source.Stop();
		}
	}
}
