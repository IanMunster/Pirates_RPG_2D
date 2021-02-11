using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HomePlanet : MonoBehaviour {

	[SerializeField] private int waitCount;
	[SerializeField] private List<string> homeDialogs;
	[SerializeField] private List<string> extraDialogs;
	private int repeat;
	private int dialogCount;
	private int extraCount;
	private bool isPlaying;
	private bool isPlayingExtra;

	// Use this for initialization
	void Start () {
		repeat = 0;
		dialogCount = homeDialogs.Count;
		extraCount = extraDialogs.Count;
		isPlaying = false;
		isPlayingExtra = false;
	}
		
	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Player") && !isPlaying && repeat==0f){
			StartCoroutine(HomePlanetDialog());
		}
	}

	void OnTriggerStay2D(Collider2D other){
		PlayerMove.current.canMove = false;
		if(other.CompareTag("Player") && Input.GetButton("Fire1") && repeat == 2f && !isPlayingExtra){
			StartCoroutine (HomeExtraDialog());
		}
		if(Time.time > waitCount*4){
			PlayerMove.current.canMove = true;
		}
	}

	IEnumerator HomePlanetDialog(){
		isPlaying = true;
		repeat = 1;
		for(int i=0; i<dialogCount; i++){
			uiDialogAvatar.current.AddDialogAvatar (0,waitCount);
			uiDialogTxt.current.AddDialogTxt (homeDialogs[i],waitCount);
			yield return new WaitForSeconds (waitCount);
		}
		isPlaying = false;
		repeat = 2;
		StopCoroutine (HomePlanetDialog());
		yield return null;
	}

	IEnumerator HomeExtraDialog(){
		isPlayingExtra = true;
		repeat = 1;
		for(int i=0; i<extraCount; i++){
			uiDialogAvatar.current.AddDialogAvatar (0,waitCount);
			uiDialogTxt.current.AddDialogTxt (extraDialogs[i],waitCount);
			yield return new WaitForSeconds (waitCount);
		}
		isPlayingExtra = false;
		repeat = 2;
		StopCoroutine (HomeExtraDialog());
		yield return null;
	}
}
