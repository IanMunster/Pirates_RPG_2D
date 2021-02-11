using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	[SerializeField] private float LaserSpeed;
	private Rigidbody2D _rigid;     /*REF TO THE RIGIDBODY*/                                 

    void Start () {     /* Use this for initialization*/
        _rigid = GetComponent<Rigidbody2D> ();	/*Make reference with the Rigidbody2D component*/
		LaserSpeed = 30;
	}

	void Update (){
        _rigid.velocity = transform.up * LaserSpeed;/*Move the laser up*/
	}
}
