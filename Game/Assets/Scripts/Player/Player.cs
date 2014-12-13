using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

 

    private PlayerController playerControl;


    // ENERGY LEVEL HANDLING
    public float energyLevel;

    // WEAPON HANDLING
    // TODO

    // ARMOR HANDLING
    // TODO

	// Use this for initialization
	void Start () {
        energyLevel = 0;

        playerControl = GetComponent<PlayerController>();
        playerControl.controlLevel = 1;
	}

    // Update is called once per frame
    void Update()
    {
        energyLevel += Time.deltaTime;
    }

    public void LevelUpControl()
    {
        playerControl.controlLevel++;

        if (playerControl.controlLevel == 2)
        {
            rigidbody2D.drag = 4;
            playerControl.speed = new Vector2(14, 14);
            playerControl.rotationSpeed = 4;
        }
        if (playerControl.controlLevel == 3)
        {
            rigidbody2D.drag = 0;
            playerControl.speed = new Vector2(6, 6);
            playerControl.rotationSpeed = 50;
        }
    }

    public void GetEnergy(int energy)
    {
        this.energyLevel += energy;
    }
}
