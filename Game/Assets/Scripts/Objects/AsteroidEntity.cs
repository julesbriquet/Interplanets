using UnityEngine;
using System.Collections;

public class AsteroidEntity : SpaceEntity {

    public int asteroidDamage;
    public int speedVelocity;

	// Use this for initialization
	void Start () {
        speedVelocity = Random.Range(0, 2);
	}

    void Update() {
        this.transform.position += Vector3.left * Time.deltaTime * speedVelocity;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            Player playerEntity = other.gameObject.GetComponent<Player>();

            playerEntity.TakeDamage(asteroidDamage);

            //Destroy(gameObject);
        }
    }
}
