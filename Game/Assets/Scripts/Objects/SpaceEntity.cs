using UnityEngine;
using System.Collections;

public class SpaceEntity : CachedBase {

    public bool haveRotation = true;

    private Vector3 startEulerAngles;
    public float rotationSpeed;

	// Use this for initialization
	void Start () {
        startEulerAngles = this.transform.eulerAngles;

        if (haveRotation)
        {
            rotationSpeed = Random.Range(10, 60);
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (haveRotation)
            this.transform.eulerAngles = startEulerAngles + Vector3.forward * Mathf.MoveTowardsAngle(transform.eulerAngles.z, transform.eulerAngles.z + rotationSpeed, rotationSpeed * Time.deltaTime);
	}
}
