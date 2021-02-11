using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class PlanetAlert : MonoBehaviour {

	[SerializeField] private List<GameObject> enemyShips;
	[SerializeField] private int maxEnemys = 10;
	[SerializeField] private int enemyCount = 5;
	//Spawn.x=spawnWait, Spawn.y=StartWait, Spawn.z=WaveWait
	[SerializeField] private Vector3 spawn = new Vector3(2f, 3f, 5f);
	private List<GameObject> enemysPool;
	private GameObject enemy;
	private Transform target;
	private SpriteRenderer alertIcon;
	private Transform alertIconTrans;
	private bool onAlert;
	private bool spawning;

	private int crrntScene;
    //audio
    private AudioSource source;
    public AudioClip alarmSound;
    public AudioClip enemySpawnSound;

    void Awake()
    {
        source = GetComponent<AudioSource>();
		crrntScene = SceneManager.GetActiveScene ().buildIndex;
		if(crrntScene<0||crrntScene>6){
			crrntScene = 0;
		}
    }

    // Use this for initialization
    void Start () {
		//SET THE POOLINGLIST, AND ADD INACTIVE GAMEOBJECTS
		enemysPool = new List<GameObject> ();
		for(int i=0; i<enemyShips.Count;i++){
			enemy = enemyShips [i];
			for(int p=0; p<maxEnemys; p++){
				GameObject obj = (GameObject)Instantiate (enemy);
				//make enemyPlanet parent of enemy
				//obj.transform.parent = transform;
				obj.SetActive (false);
				enemysPool.Add (obj);
			}
		}
		alertIcon = GetComponentInChildren<SpriteRenderer> ();
		alertIconTrans = alertIcon.transform;
		alertIcon.enabled = false;
		target = transform;
		onAlert = false;
		spawning = false;
	}
	void Update(){
		alertIconTrans.Rotate (0,0,45*Time.deltaTime);
	}
	void OnTriggerEnter2D (Collider2D other) {
		if (other.CompareTag("Player")) {
			onAlert = true;
            source.PlayOneShot(alarmSound,0.5f);
			// DIALOG
			uiDialogAvatar.current.AddDialogAvatar(crrntScene-1, 4f);
			uiDialogTxt.current.AddDialogTxt ("It seems like we have an intruder!", 5f);
        }
	}
	void OnTriggerStay2D (Collider2D other){
		if(other.CompareTag("Player") && onAlert && !spawning){
			alertIcon.enabled = true;
			if(!spawning){
                StartCoroutine ("Spawn");
            }
		}
	}

    public IEnumerator Spawn () {
		spawning = true;
		yield return new WaitForSeconds (spawn.x);
        
		//!! DIALOG
		uiDialogAvatar.current.AddDialogAvatar(crrntScene-1, 3f);
		uiDialogTxt.current.AddDialogTxt ("SHOOT HIM DOWN!", 3f);
        while (true) {         
            for (int i = 0; i<enemyCount; i++) {
				Vector2 spawnPosition = new Vector2 (target.position.x, target.position.y);
				Quaternion spawnRotation = Quaternion.identity;
				//INT P AS IN POOL
				for(int p=0; p<enemysPool.Count; p++) {
					if (!enemysPool[p].activeInHierarchy) {
                        source.PlayOneShot(enemySpawnSound, 0.8f);
                        enemysPool [p].transform.position = spawnPosition;
						enemysPool [p].transform.rotation = spawnRotation;
						enemysPool [p].SetActive (true);
						break;
					}
				}
				yield return new WaitForSeconds (spawn.y);
			}
			yield return new WaitForSeconds (spawn.z);
		}
	}
	void OnTriggerExit2D (Collider2D other){
		if (other.CompareTag ("Player")) {
            this.source.Stop();
            onAlert = false;
			//!! DIALOG
			uiDialogAvatar.current.AddDialogAvatar(crrntScene-1, 3f);
			uiDialogTxt.current.AddDialogTxt ("Yeah you better run!", 3f);
			alertIcon.enabled = false;
			StopCoroutine ("Spawn");
			if(spawning){
				StopCoroutine ("Spawn");
				spawning = false;
			}
        }
	}
}

