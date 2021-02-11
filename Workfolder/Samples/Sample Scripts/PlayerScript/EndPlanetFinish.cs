using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EndPlanetFinish : MonoBehaviour {

	[SerializeField] private int waitCount;
	[SerializeField] private List<string> brotherDialogs;
	[SerializeField] private List<string> victoryDialogs;
	private int repeat;
	private int brotherDialogCount;
	private int victoryDialogCount;
	private bool playingBrother;
	private bool playingVictory;
	private float transSpeed;

	// Use this for initialization
	void Start () {
		repeat = 0;
		brotherDialogCount = brotherDialogs.Count;
		victoryDialogCount = victoryDialogs.Count;
		playingBrother = false;
		playingVictory = false;
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.CompareTag("Player") && !playingBrother && !playingVictory && repeat == 0){
			StartCoroutine(BrotherDialog());
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if(other.CompareTag("Player")){
			if(Time.time > waitCount){
				PlayerMove.current.canMove = false;
			}
			if(Time.time > waitCount*2){
				transSpeed = Vector2.Distance (other.transform.position, this.transform.position)*Time.deltaTime;
				Quaternion rot = Quaternion.LookRotation (other.transform.position - transform.position, transform.TransformDirection(Vector3.forward));
				other.transform.rotation = new Quaternion (0f,0f,rot.z,rot.w);
				other.transform.position = Vector3.MoveTowards (other.transform.position, this.transform.position, transSpeed);
			}
			if(repeat == 2 && !playingBrother && !playingVictory){
				StartCoroutine(VictoryDialog());
			}
		}
	}

	IEnumerator BrotherDialog (){
		playingBrother = true;
		repeat = 1;
		for(int i=0; i<brotherDialogCount; i++){
			uiDialogAvatar.current.AddDialogAvatar (4, waitCount);
			uiDialogTxt.current.AddDialogTxt (brotherDialogs[i],waitCount);
			yield return new WaitForSeconds (waitCount);
		}
		playingBrother = false;
		repeat = 2;
		StopCoroutine (BrotherDialog());
		yield return null;
	}

	IEnumerator VictoryDialog(){
		playingVictory = true;
		repeat = 3;
		for(int i=0; i<victoryDialogCount; i++){
			uiCenterTxt.current.AddCenterTxt (victoryDialogs [i], waitCount);
			yield return new WaitForSeconds (waitCount);
		}
		playingVictory = false;
		GameController.current.Victory ();
		StopCoroutine (VictoryDialog());
		yield return null;
	}
}
