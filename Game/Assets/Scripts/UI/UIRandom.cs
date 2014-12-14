﻿using UnityEngine;
using System.Collections;

public class UIRandom : MonoBehaviour {
	public Transform engine;
	public float speed = 3f;
	public float force = 0.1f;
	
	public float turbulanceForce = 1f;
	public float turbulanceFrequence = 1f;
	
	public float stunTime = 1f;
	
	private float stun = 0f;
	private float timePassed = 0f;
	
	private Vector2 destination = new Vector2();
	private Vector2 velocity = new Vector2();
	private Vector2 turbulance = new Vector2();
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void FixedUpdate(){
		Vector2 steering = velocity;
		steering = Vector2.ClampMagnitude(steering, force) + AddTurbulances();
		
		velocity = velocity + Vector2.ClampMagnitude(steering, speed);
		
		//this.transform.rotate(this.velocity.angle());
		
		Debug.DrawRay(transform.position, destination, Color.grey);
		Debug.DrawRay(transform.position, velocity, Color.green);
		
		transform.localEulerAngles = new Vector3(-velocity.y * 3f, transform.localEulerAngles.y, velocity.y * 3f);
		
		rigidbody2D.MovePosition(rigidbody2D.position + velocity * Time.deltaTime);
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

