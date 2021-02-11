using UnityEngine;
using System.Collections;

public class Blackhole : MonoBehaviour {
	[SerializeField] private float ForceSpeed;
	private Transform PullOBJ;
	private GameObject playerShip;
	private PlayerShield _playerShield;

    //audio
	[SerializeField] private AudioClip voidSound;
    private AudioSource source;

    void Awake() {
        source = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start() {
		PullOBJ = GetComponent<Transform> ();
		playerShip = GameObject.FindGameObjectWithTag ("Player");
		if(playerShip != null) {
			_playerShield = playerShip.GetComponent<PlayerShield> ();
		}
    }
	//IF someone enters its vecinity
    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            source.PlayOneShot(voidSound, 1f);
        }
    }

	public void OnTriggerStay2D(Collider2D other) {
		if (other.CompareTag("Player") || other.CompareTag("Enemy")) {
			PullOBJ = other.transform;
			PullOBJ.transform.position = Vector2.MoveTowards (PullOBJ.transform.position, this.transform.position, ForceSpeed * Time.deltaTime);
		}
	}

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            source.Stop();
        }
    }

    // Update is called once per frame
    void Update() {
		if(playerShip != null) {
			float range = Vector2.Distance (playerShip.transform.position, transform.position);
			if(range < 1) {
				_playerShield.RepeatDamagingShield (100, 1);
			}
		}
    }
}



