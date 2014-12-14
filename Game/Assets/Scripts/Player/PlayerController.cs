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

    [HideInInspector]
    public GunEntity weapon;

    // LEVEL CONTROL HANDLING
    [HideInInspector]
    public int controlLevel;
    [HideInInspector]
    public int maxControlLevel = 3;

    // Handle stun
    public float stunTime = 0;


    private bool enableControl = true;

    // HANDLE ANIMATIONS
    private Animator animEntity;

    // This put transform and rigidbody in cache
    public override void Awake()
    {
        base.Awake(); //does the caching.
        //Debug.Log ("Awake called!");
    }

	// Use this for initialization
	void Start () {
        startSpeed = speed;

        animEntity = GetComponent<Animator>();
        stunTime = -10;
	}
	
	// Update is called once per frame
	void Update () {
        if (enableControl)
        {
            if (Time.time > stunTime)
            {
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

                if (animEntity)
                {
                    animEntity.SetBool("isStun", false);
                    //Debug.Log("TEST" + Mathf.Abs(inputX + inputY));
                    animEntity.SetBool("isMoving", (Mathf.Abs(inputX) + Mathf.Abs(inputY) > 0.5f));
                }

                velocity = new Vector2(speed.x * inputX, speed.y * inputY);

                if (weapon)
                {
                    bool triggerShoot = false;

                    triggerShoot = Input.GetButton(InputPlayerString + "Shoot");
                    if (triggerShoot)
                    {
                        weapon.Shoot(playerNumber);
                        weapon.hasTriggerBeenRelease = false;
                    }
                    else
                        weapon.hasTriggerBeenRelease = true;
                }
            }
            else
            {
                if (animEntity)
                    animEntity.SetBool("isStun", true);
            }
        }
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
        if (enableControl)
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

    public void GoToLightSpeed()
    {

        enableControl = false;
        animEntity.SetBool("isLightSpeed", true);

        rigidbody2D.collider2D.enabled = false;
        // MOVE
        transform.rotation = Quaternion.identity;
        //.RotateAround(Vector3.zero, 20 * Time.deltaTime);
        velocity = new Vector2(100f, 0f);
    }
}
