using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class uiDialogAvatar : MonoBehaviour {

	public static uiDialogAvatar current;
	private Image avatar;
	//0=Commander,1/3=Enemy 4=Brother
	[SerializeField] private Sprite[] allAvatars;
	[SerializeField] private float secondsWait = 10f;

	// Use this for initialization
	void Start () {
		current = this;
		avatar = GetComponent<Image> ();
		avatar.gameObject.SetActive (false);
	}
	
	// Update is called once per frame
	public void AddDialogAvatar (int avatarInt, float newWait) {
		avatar.gameObject.SetActive (true);
		avatar.sprite = allAvatars [avatarInt];
		secondsWait = newWait;
		StartCoroutine (AnimateAvatar());
	}

	IEnumerator AnimateAvatar(){
		
		yield return new WaitForSeconds (secondsWait);
		avatar.gameObject.SetActive (false);
		StopCoroutine (AnimateAvatar());
		yield return null;
	}
}
