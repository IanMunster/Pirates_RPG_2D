using UnityEngine;
using System.Collections;

public class ObjRotator : MonoBehaviour {

	[SerializeField] private float rotateSpeed;
	private Transform obj;

	// Use this for initialization
	void Start () {
		obj = GetComponent<Transform> ();
	}

	// Update is called once per frame
	void Update () {
		obj.Rotate (0,0,rotateSpeed*Time.deltaTime);
	}
}
