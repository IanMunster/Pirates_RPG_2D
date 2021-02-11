using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionMenuScript : MonoBehaviour {

	[SerializeField] private Sprite[] allOptionScreens;
	private Image optionScreen;
	private int allOptionScreenLength;
	private int crrntScreen;

	// Use this for initialization
	void Awake () {
		GameController.current.SaveData("Level", GameController.crrntScene);
		optionScreen = GetComponent<Image> ();
		crrntScreen = 0;
		allOptionScreenLength = allOptionScreens.Length -1;
	}

	void Update(){
		if(crrntScreen != -1 && crrntScreen != allOptionScreenLength+1){
			optionScreen.sprite = allOptionScreens [crrntScreen];
		}
	}

	public void NextScreen(){
		crrntScreen++;
		if(crrntScreen > allOptionScreenLength){
			crrntScreen = 0;
		}
	}

	public void PrevScreen (){
		crrntScreen--;
		if(crrntScreen < 0){
			crrntScreen = allOptionScreenLength;
		}
	}

	public void ExitOptions (){
		GameController.current.LoadSceneByInt (GameController.current.LoadData("Level"));
	}
}
