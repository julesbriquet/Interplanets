using UnityEngine;
using System.Collections;

public class ShipMovementTest : MonoBehaviour {
	public Transform engine;
	public float speed = 3f;
	public float turbulance = 0.25f;
	public float force = 0.1f;
	public Vector2 destination = new Vector2();
	public Vector2 velocity = new Vector2();

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float inputX = Input.GetAxis("P1_Horizontal");
		float inputY = Input.GetAxis("P1_Vertical");

		destination = new Vector2(inputX, inputY).normalized * speed;
	}

	void FixedUpdate(){

		Vector2 steering = destination - velocity;
		steering = Vector2.ClampMagnitude(steering, force) + (new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * turbulance);
		
		velocity = velocity + Vector2.ClampMagnitude(steering, speed);
		
		//this.transform.rotate(this.velocity.angle());

		Debug.DrawRay(transform.position, destination, Color.grey);
		Debug.DrawRay(transform.position, velocity, Color.green);

		transform.localEulerAngles = new Vector3(-velocity.y * 3f, transform.localEulerAngles.y, velocity.y * 3f);

		rigidbody2D.MovePosition(rigidbody2D.position + velocity * Time.deltaTime);
	}
}
