using UnityEngine;
using System.Collections;
using System;

[System.Serializable]
public class Boundary
{
    public float xBorderMin, xBorderMax, yBorderMin, yBorderMax;
}

public class PlayerController : CachedBase {

    public int playerNumber = 1;
    public Vector2 speed;
    public float rotationSpeed;
    public Vector2 velocity;

    public Boundary bounds;

    private String InputPlayerString;
    private Vector2 startSpeed;

    // LEVEL CONTROL HANDLING
    [HideInInspector]
    public int controlLevel;
    [HideInInspector]
    public int maxControlLevel = 3;

    // This put transform and rigidbody in cache
    public override void Awake()
    {
        base.Awake(); //does the caching.
        //Debug.Log ("Awake called!");
    }

	// Use this for initialization
	void Start () {
        startSpeed = speed;
	}
	
	// Update is called once per frame
	void Update () {

        if (playerNumber == 1)
        {
            InputPlayerString = "P1_";
        }
        if (playerNumber == 2)
        {
            InputPlayerString = "P2_";
        }

        float inputX = Input.GetAxis(InputPlayerString + "Horizontal");
        float inputY = Input.GetAxis(InputPlayerString + "Vertical");
        //Debug.Log("Vertical: " + inputY + " Hori: " + inputX);


        velocity = new Vector2(speed.x * inputX, speed.y * inputY);
	}

    void FixedUpdate()
    {

        // HANDLING MOVEMENTS
        if (controlLevel < 3)
            rigidbody2D.AddForce(velocity);
        else
            rigidbody2D.velocity = velocity;


        // HANDLING ROTATION
        rigidbody2D.rotation = Mathf.Clamp(rigidbody2D.rotation, -50f, 50f);
        float rot = rigidbody2D.rotation;
        if (velocity.y < 0.01 && velocity.y > -0.01)
        {
            //Debug.Log("Test");
            rigidbody2D.MoveRotation(Mathf.Lerp(rot, velocity.y * rotationSpeed * 1.2f, 3.5f * Time.deltaTime));
        }
        else
            rigidbody2D.MoveRotation(Mathf.Lerp(rot, velocity.y * rotationSpeed, 2f * Time.deltaTime));

        //Debug.Log(rigidbody2D.rotation);

        ScreenLimitControl();
        
    }

    void ScreenLimitControl()
    {
        Vector3 camPosition = Camera.main.transform.position;

        float xMin = bounds.xBorderMin + camPosition.x;
        float xMax = bounds.xBorderMax + camPosition.x;

        float yMin = bounds.yBorderMin + camPosition.y;
        float yMax = bounds.yBorderMax + camPosition.y;

        rigidbody2D.position = new Vector2(
            Mathf.Clamp(rigidbody2D.position.x, xMin, xMax),
            Mathf.Clamp(rigidbody2D.position.y, yMin, yMax)
            );
    }


}
