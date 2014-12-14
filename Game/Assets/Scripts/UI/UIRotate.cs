using UnityEngine;
using System.Collections;

public class UIRotate : MonoBehaviour {
	public float speed = 1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround(Vector3.forward, speed * Time.deltaTime);
	}
}
