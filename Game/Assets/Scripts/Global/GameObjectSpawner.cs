using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectSpawner : CachedBase {


    public GameObject[] collectibleObjects;
    public GameObject[] asteroidObjects;
    public float[] collectibleWeights;
    public float[] asteroidsWeights;
    public int asteroidCount;
    public int collectibleCount;
    public float spawnWait;
    public float startWait;
    public Vector2 spawnPositionRange;


    private Transform parentSpawner;
    private List<GameObject> prevInstanciedObj;

    // This put transform and rigidbody in cache
    public override void Awake()
    {
        base.Awake(); //does the caching.
        //Debug.Log ("Awake called!");
    }

	// Use this for initialization
	void Start () {
        InvokeRepeating("SpawnWave", startWait, spawnWait);
        parentSpawner = GameObject.FindGameObjectWithTag("MiddleGround").GetComponent<Transform>();

        prevInstanciedObj = new List<GameObject>();
	}
	


    void SpawnWave()
    {
        Vector3 initialPosition = this.transform.position;
        //int randomVar = Random.Range(

        // Spawn asteroids
        for (int i = 0; i < asteroidCount; i++)
        {
            Vector3 spawnPosition = this.transform.position + new Vector3(Random.Range(-spawnPositionRange.x, spawnPositionRange.x), Random.Range(-spawnPositionRange.y, spawnPositionRange.y), 0);
            Quaternion spawnRotation = Quaternion.identity;
            GameObject spawnedObj = (GameObject)Instantiate(getRandomAsteroids(), spawnPosition, spawnRotation);
            spawnedObj.transform.parent = parentSpawner;

            prevInstanciedObj.Add(spawnedObj);
        }


        // Spawn Collectibles
        for (int i = 0; i < collectibleCount; i++)
        {
            bool badPositionInstantiation = false; 

            Vector3 spawnPosition = this.transform.position + new Vector3(Random.Range(0, spawnPositionRange.x), Random.Range(-spawnPositionRange.y, spawnPositionRange.y), 0);
            Quaternion spawnRotation = Quaternion.identity;

            
            GameObject spawnedObj = (GameObject)Instantiate(getRandomCollectible(), spawnPosition, spawnRotation);

            foreach (GameObject obj in prevInstanciedObj)
            {
                if (obj.collider2D.bounds.Intersects(spawnedObj.collider2D.bounds)) {
                    badPositionInstantiation = true;
                }
            }

            if (badPositionInstantiation) {
                Destroy(spawnedObj);
                i--;
            }
            else {
                spawnedObj.transform.parent = parentSpawner;

                prevInstanciedObj.Add(spawnedObj);
            }
        }

        

        prevInstanciedObj.Clear();
    }


    private GameObject getRandomCollectible()
    {
        float[] cumulativeWeights = new float[collectibleObjects.Length];
        cumulativeWeights[0] = collectibleWeights[0];
        for (int i = 1; i < collectibleWeights.Length; ++i)
        {
            cumulativeWeights[i] = cumulativeWeights[i - 1] + collectibleWeights[i];
        }

        float r = Random.Range(0, cumulativeWeights[collectibleObjects.Length - 1]);

        for (int i = 0; i < collectibleObjects.Length; ++i)
            if (r <= cumulativeWeights[i])
                return collectibleObjects[i];

        return null;
    }

    private GameObject getRandomAsteroids()
    {
        float[] cumulativeWeights = new float[asteroidObjects.Length];
        cumulativeWeights[0] = asteroidsWeights[0];
        for (int i = 1; i < asteroidsWeights.Length; ++i)
        {
            cumulativeWeights[i] = cumulativeWeights[i - 1] + asteroidsWeights[i];
        }

        float r = Random.Range(0, cumulativeWeights[asteroidObjects.Length - 1]);

        for (int i = 0; i < asteroidObjects.Length; ++i)
            if (r <= cumulativeWeights[i])
                return asteroidObjects[i];

        return null;
    }

}
