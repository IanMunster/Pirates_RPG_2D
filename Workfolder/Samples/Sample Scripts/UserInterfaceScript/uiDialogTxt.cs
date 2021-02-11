using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class uiDialogTxt : MonoBehaviour {

	public static uiDialogTxt current;
	private Image dialogDisplay;
	private Text dialogText;
	private string tekst = "";

	[SerializeField] private float secondsWait = 10f;

	// Use this for initialization
	void Start () {
		current = this;
		dialogDisplay = GetComponent<Image> ();
		dialogText = GetComponentInChildren<Text> ();
		dialogDisplay.gameObject.SetActive (false);
		dialogText.gameObject.SetActive (false);
	}

	// Update is called once per frame
	public void AddDialogTxt (string newTxt, float newWait) {
		dialogDisplay.gameObject.SetActive (true);
		dialogText.gameObject.SetActive (true);
		tekst = newTxt;
		secondsWait = newWait;
		StartCoroutine (AnimateDialog());
	}

	IEnumerator AnimateDialog(){
		dialogText.text = tekst;
		yield return new WaitForSeconds (secondsWait);
		dialogText.text = "";
		dialogDisplay.gameObject.SetActive (false);
		dialogText.gameObject.SetActive (false);
		StopCoroutine (AnimateDialog());
		yield return null;
	}
}
