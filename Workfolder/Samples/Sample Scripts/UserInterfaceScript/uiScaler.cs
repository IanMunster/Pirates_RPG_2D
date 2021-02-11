using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class uiScaler : MonoBehaviour {

	[SerializeField] private CanvasScaler uiCanvasScaler;
	[SerializeField] private float scaleSpeed = 0.1f;
	//Size.x = MIN,Size.y = NRML,Size.z = MAX,
	private Vector3 size = new Vector3(0.5f, 0.7f, 1f);
	private float crrntSize;

	// Use this for initialization
	void Start () {
		//uiCanvasScaler.GetComponent<CanvasScaler> ();
		crrntSize = uiCanvasScaler.scaleFactor;
	}
	
	// Update is called once per frame
	void Update () {
		crrntSize = uiCanvasScaler.scaleFactor;
		//UI SCALE UP
		if(Input.GetKeyDown(KeyCode.Z)){
			uiCanvasScaler.scaleFactor += scaleSpeed;
			if(crrntSize >= size.z){
				crrntSize = size.z;
			}
		}
		//UI SCALE DOWN
		if(Input.GetKeyDown(KeyCode.X)){
			uiCanvasScaler.scaleFactor -= scaleSpeed;
			if(crrntSize <= size.x){
				crrntSize = size.x;
			}
		}
		//UI SCALE NORMAL
		if(Input.GetKeyDown(KeyCode.Z) && Input.GetKeyDown(KeyCode.X)){
			uiCanvasScaler.scaleFactor = size.y;
		}
	}
}
