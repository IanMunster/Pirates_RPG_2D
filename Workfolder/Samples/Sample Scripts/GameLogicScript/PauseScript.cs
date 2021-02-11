using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour {
	//[SerializeField] private Canvas PauseUI;
	public static PauseScript current;
	private Canvas pauseUI;
    private bool paused = false;
	void Awake (){
		GameObject pauseUIObj = GameObject.FindWithTag ("PauseUI");
		if(pauseUIObj != null){
			current = this;
			pauseUI = pauseUIObj.GetComponent<Canvas> ();
		}
	}

    public void Resume() {
		paused = !paused;
    }
		
    void Update() {
		if(pauseUI != null){
			if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) {
				paused = !paused;
			}
			if (paused) {
				pauseUI.gameObject.SetActive(true);
				Time.timeScale = 0;
			}
			else if (!paused) {
				pauseUI.gameObject.SetActive(false);
				Time.timeScale = 1;
			}
		}
	}
}