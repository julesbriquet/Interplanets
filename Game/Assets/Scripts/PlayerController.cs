using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    int playerNumber = 1;
    public Vector2 speed;
    public Vector2 velocity;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        velocity = new Vector2(speed.x * inputX, speed.y * inputY);
	}

    void FixedUpdate()
    {
        rigidbody2D.velocity = velocity;
    }
}
