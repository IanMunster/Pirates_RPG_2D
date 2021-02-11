using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class uiPlayerScore : MonoBehaviour {

	public static uiPlayerScore current;
	private Text scoreText;
	private int score;

	// Use this for initialization
	void Start () {
		current = this;
		scoreText = GetComponent<Text> ();
		//SCORE UI
		score = GameController.current.LoadData("Score");
		UpdateScore ();
	}
	void Update(){
		scoreText.text.ToUpper ();
	}

	//SCORE UI START
	public void AddScore(int newScoreValue){
		score += newScoreValue;
		UpdateScore();
	}
	void UpdateScore(){
		if(scoreText != null){
			scoreText.text = "COLLECTED DATA: " + score;
			GameController.current.SaveData ("Score", score);
		}
	}
}