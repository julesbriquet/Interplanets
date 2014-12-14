using UnityEngine;
using System.Collections;

public class UIRandom : MonoBehaviour {
	public float turbulanceForce = 1f;
	public float turbulanceFrequence = 1f;
	private float timePassed = 0f;
	private Vector2 turbulance = new Vector2();
	
	void FixedUpdate(){
		rigidbody2D.MovePosition(rigidbody2D.position + AddTurbulances() * Time.deltaTime);
	}
	
	Vector2 AddTurbulances(){
		timePassed += Time.deltaTime;
		if(timePassed > turbulanceFrequence){
			timePassed = 0f;
			turbulance = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized * turbulanceForce;
		} else {
			turbulance = new Vector2();
		}
		return turbulance;
	}
}

