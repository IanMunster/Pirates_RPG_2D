using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

	public static GameController current;
	public static int crrntScene;
	private bool isRetrying = false;
	private bool isGameOver = false;
	private bool isVictorious = false;
	private int firstGame;

	void Start (){
		if(LoadData("FirstGame") == 0){
			LoadSceneByName ("MainMenu_00");
			SaveData ("FirstGame", 1);
		}
	}
	// Use this for initialization
	void Awake (){
		current = this;
		crrntScene = SceneManager.GetActiveScene ().buildIndex;
		isRetrying = false;
		isGameOver = false;
		isVictorious = false;
	}
	void Update (){
		if(Input.GetKeyDown(KeyCode.R)){
			if(isRetrying){
				SceneManager.LoadScene(crrntScene);
			}
			if(isGameOver){
				QuitGame ();
			}
		}
		if(isVictorious){
			QuitGame ();
		}
	}

	public void Retry(){
		uiCenterTxt.current.AddCenterTxt ("You died! Press R to try again.", 20f);
		isRetrying = true;
	}
	public void GameOver (){
		uiCenterTxt.current.AddCenterTxt ("GAME OVER, Press R to continue", 20f);
		isGameOver = true;
	}
	public void Victory (){
		isVictorious = true;
	}

	public void StartCrrntScene (){
		SceneManager.LoadScene(crrntScene);
	}
	public void NxtLevel (){
		SceneManager.LoadScene(crrntScene+1);
	}
	public void ResumeGame(){
		PauseScript.current.Resume ();
	}
	public void QuitGame(){
		Screen.fullScreen = false;
		DeleteData ();
		LoadSceneByName ("MainMenu_00");
	}

	public void LoadSceneByName(string name){
		SceneManager.LoadScene (name);
	}
	public void LoadSceneByInt(int interger){
		SceneManager.LoadScene (interger);
	}
		
	public void SaveData (string data, int value){
		PlayerPrefs.SetInt (data, value);
		PlayerPrefs.Save ();
	}
	public int LoadData (string data){
		return PlayerPrefs.GetInt (data);
	}
	public void DeleteData (){
		PlayerPrefs.DeleteAll ();
		PlayerPrefs.Save ();
	}
}
