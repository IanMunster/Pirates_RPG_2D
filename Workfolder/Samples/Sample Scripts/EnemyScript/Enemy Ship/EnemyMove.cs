using UnityEngine;
using System.Collections;

public class EnemyMove : MonoBehaviour {

	public float enemySpeed;
	//speed.x=Minspeed, speed.y=NormalSpeed, speed.z = MaxSpeed
	public Vector3 speed;

	void Awake (){
		speed = new Vector3(0,enemySpeed,enemySpeed*2);
	}
	// FixedUpdate is called once per physicsstep
	void FixedUpdate () {
		//Move the Enemy And Give it its Rotation values;
		Quaternion rotation = transform.rotation;
		float z = rotation.eulerAngles.z;
		rotation = Quaternion.	Euler(0f,0f,z);
		transform.rotation = rotation;
		Vector3 pos = transform.position;
		Vector3 velocity = new Vector3 (0f, enemySpeed * Time.deltaTime, 0f);
		pos += rotation * velocity;
		transform.position = pos;
	}
}
