 using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerShoot : MonoBehaviour {

	//SHOOTING VARS
	[SerializeField] private GameObject lazer;					/*THE LAZER OBJECT OF THE PLAYER*/
	[SerializeField] private float maxLazers = 20f;				/*MAX POOLED AMOUNT*/
	[SerializeField] private List<GameObject> playerCannons; 	/*THE MUZZLEPOINTS OF THE PLAYER (MULTIPLE)*/
	[SerializeField] private float fireRate = 0.3f;			/*PLAYERS FIRERATE*/
	private List<GameObject> lazers;							/*LIST OF THE POOLED LAZERS*/
	private float nextFire = 0.0f;								/*TIME CHECK FOR NEXT SHOT*/
	//OVERHEATING VARS
	private bool canShoot = true;		/*CAN THE PLAYER SHOOT*/
	private bool isShooting = false;    /*IS THE PLAYER CURRENTLY SHOOTING*/
	private float heat = 10f;			/*HEAT PER SHOT*/
	private float overHeat;				/*CURRENT OVERHEATING*/
	private float maxHeat = 100f;		/*MAX OVERHEATING*/
	//COOLDOWN VARS
	private bool canCooldown = false;
	private float cooldown = 1f;		//COOLDOWN DECREASE RATE
	private float coolWait = 0.0f;		//RESETS THE COOLDOWN WAIT
	private float cooldownRate = 5f;	//COOLDOWN SEC AFTER SHOT
	//Animation vars
	private Animator[] cannonAnims;

    //audio vars
    public AudioClip shootSound;
    private AudioSource source;
    private float volLowRange = .3f;
    private float volHighRange = .8f;


    void Awake()

    {
        source = GetComponent<AudioSource>();
    }

    void Start () {						/* Use this for initialization*/
		lazers = new List<GameObject> ();		
		/*SET THE POOLINGLIST, AND ADD INACTIVE GAMEOBJECTS*/
		for(int l=0; l<maxLazers; l++){
			GameObject obj = (GameObject)Instantiate (lazer);
			//make lazer a child of Player 
			//obj.transform.parent = transform; (UNLESS THE MOVEMENT ROTATION IS BETTER, THIS SHOULD BE OFF.)
			obj.SetActive (false);
			lazers.Add (obj);
		}
		//GET THE CANNON ANIMATIONS
		cannonAnims = new Animator[playerCannons.Count];
		for(int i=0; i<cannonAnims.Length;i++){
			cannonAnims[i] = playerCannons[i].GetComponent<Animator>();
		}
	}

	void Update (){
		uiPlayerOverheated.current.addHeat (overHeat);
		for(int i=0; i<cannonAnims.Length; i++){
			cannonAnims [i].SetBool ("IsOverheated", !canShoot);
		}

	}

	void FixedUpdate () {	/* FixedUpdate is called once per physstep*/
		OverHeating ();
		if (Input.GetButton ("Fire1") && Time.time > nextFire && canShoot) {    /*THE PLAYERSHOOT FUNCTION*/
            float vol = Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(shootSound, vol);
            nextFire = Time.time + fireRate;
			isShooting = true;
			for (int i=0; i<playerCannons.Count; i++) {		/*SET THE POOLED LAZER TO ACTIVE*/
				for (int l = 0; l < lazers.Count; l++) {
					if (!lazers [l].activeInHierarchy) {
						lazers [l].transform.position = playerCannons[i].transform.position;
						lazers [l].transform.rotation = playerCannons[i].transform.rotation;
						lazers [l].SetActive (true);
						cannonAnims [i].SetTrigger ("IsFiringTrigger");
						break;
					}
				}
			}
		} else {
			isShooting = false;
		}
	}

	void OverHeating(){
		//OVERHEAT EVERY SHOT
		if (isShooting) {
			overHeat += heat;	
			canCooldown = false;
		} 
		if (!isShooting && Time.time > coolWait && !canCooldown) {
			coolWait = Time.time + cooldownRate;
			canCooldown = true;
		}
		if (!isShooting && canCooldown){
		//COOLDOWN WHEN NOT FIRING
			overHeat -= cooldown;
		}
		//IF IT EXCEEDS THE LIMIT, YOU CANT SHOOT
		if (overHeat >= maxHeat) {	
			overHeat = maxHeat;
			canShoot = false;
			uiPlayerWarning.current.AddWarningTxt ("WARNING! GUNS OVERHEATED.", 3f, new Vector3(99,00,00));
		}
		//PLAYER CAN ONLY SHOOT AFTER MAXHEATH IS DECREASED ENOUGH
		if(overHeat < maxHeat-20){
			canShoot = true;
		}
		if (overHeat <= 0) {
			overHeat = 0;
			canShoot = true;
		} 
	}
}
