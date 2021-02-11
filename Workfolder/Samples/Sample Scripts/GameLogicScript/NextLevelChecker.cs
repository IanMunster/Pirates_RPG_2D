using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NextLevelChecker : MonoBehaviour {

    /*
	 * Does the player hit the top of level?
	 * Are all enemys dead?
	 * if not, get message to return and kill them
	 * if so go to the next level
	 * -> Stretch the background over Y
	 * load new level *PLAY A SOUND*
	 * -> Stretch the NEW background over Y
	 * <- Retrun the NEW background to normal
	 *
	*/
	public static NextLevelChecker current;
     //audio
	[SerializeField] private AudioSource source;
	[SerializeField] private AudioClip teleportSound;
	[SerializeField] private List<string> commanderDialog_noData;
	[SerializeField] private List<string> commanderDialog_enemy;
	[SerializeField] private int waitCount;
	private Transform bg;
	private Transform _player;
	private Vector2 limitStretch = new Vector2(0,1000);
	private float stretchY = 5f;
	private float crrntStretch;
	private bool playingDialog;
	private bool canStretch;
	private int enemyCount;
	private int crrntLvlnumber;
	private int crrntScore;
	private int minimalScore;
	private int repeat;
	private int noData;
	private int enemyD;


	void Awake()
	{
		current = this;
		source = GetComponent<AudioSource>();
		noData = commanderDialog_noData.Count;
		enemyD = commanderDialog_enemy.Count;
	}

	// Use this for initialization
	void Start () {
		bg = GameObject.FindWithTag ("BackGround").transform;
		_player = GameObject.FindWithTag ("Player").transform;
	}

	// Update is called once per frame
	void Update () {
		enemyCount = GameObject.FindGameObjectsWithTag ("Enemy").Length;
		crrntStretch = bg.localScale.y;
		crrntLvlnumber = GameController.crrntScene - 1;
	}

	void OnTriggerEnter2D (Collider2D other){
		if(other.CompareTag("Player")) {
			crrntScore = GameController.current.LoadData ("Score");
			minimalScore = crrntLvlnumber * 50;
			if (crrntScore < minimalScore && !playingDialog && repeat == 0) {
				StartCoroutine (CommanderDialog1 ());
			} else if (enemyCount != 0 && !playingDialog && repeat == 0) {
				StartCoroutine (CommanderDialog2 ());
			} else if (enemyCount == 0 && crrntScore >= minimalScore) {
				uiDialogAvatar.current.AddDialogAvatar(0, 5f);
				uiDialogTxt.current.AddDialogTxt("Nice work! Jack", 5f);
				canStretch = true;
				source.PlayOneShot(teleportSound, 1f);
				StartCoroutine (AnimateBG());
			}
		}
	}
	IEnumerator CommanderDialog1(){
		playingDialog = true;
		repeat = 1;
		for(int i=0; i<noData; i++){
			uiDialogAvatar.current.AddDialogAvatar (0, waitCount);
			uiDialogTxt.current.AddDialogTxt (commanderDialog_noData[i],waitCount);
			yield return new WaitForSeconds (waitCount);
		}
		uiPlayerWarning.current.AddWarningTxt ("NEEDED DATA: "+(minimalScore - crrntScore)+".", 8f, new Vector3(99,99,99));
		playingDialog = false;
		repeat = 0;
		StopCoroutine(CommanderDialog1());
		yield return null;
	}

	IEnumerator CommanderDialog2(){
		playingDialog = true;
		repeat = 1;
		for(int i=0; i<enemyD; i++){
			uiDialogAvatar.current.AddDialogAvatar (0, waitCount);
			uiDialogTxt.current.AddDialogTxt (commanderDialog_enemy[i],waitCount);
			yield return new WaitForSeconds (waitCount);
		}
		uiPlayerWarning.current.AddWarningTxt ("ENEMY LEFT: "+enemyCount+".", 8f, new Vector3(99,99,99));
		playingDialog = false;
		repeat = 0;
		StopCoroutine(CommanderDialog2());
		yield return null;
	}

	IEnumerator AnimateBG(){
		while(canStretch){
			bg.localScale +=  new Vector3 (0f,stretchY,0f);
			_player.position = this.transform.position;
			_player.rotation = this.transform.rotation;
			yield return new WaitForSeconds (0.01f);
			if(crrntStretch >= limitStretch.y){
				canStretch = false;
				GameController.current.NxtLevel ();
				yield return null;
			}
		}
	}

	public bool AsteroidStopper(){
		return canStretch;
	}
}
