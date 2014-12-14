using UnityEngine;
using System.Collections;

public class UIFollow : MonoBehaviour {

	public Transform target;

	public float force = 0.5f;
	public float speed = 3f;

	private Vector3 velocity = new Vector3();

	void LateUpdate(){
		Vector3 destination = target.transform.position - transform.position;
		destination.z = 0f;
			
		Vector3 steering = destination - velocity;
		steering = Vector3.ClampMagnitude(steering, force);
		velocity = Vector3.ClampMagnitude(velocity + steering, speed);

		transform.position = transform.position + velocity * Time.deltaTime;
	}
}