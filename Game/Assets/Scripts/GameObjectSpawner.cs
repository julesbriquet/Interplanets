using System.Collections;
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


    // This put transform and rigidbody in cache
    public override void Awake()
    {
        base.Awake(); //does the caching.
        //Debug.Log ("Awake called!");
    }

	// Use this for initialization
	void Start () {
        StartCoroutine(SpawnWave());
	}
	
	// Update is called once per


    IEnumerator SpawnWave()
    {
        yield return new WaitForSeconds(startWait);
        Vector3 initialPosition = this.transform.position;
        //int randomVar = Random.Range(

        for (int i = 0; i < collectibleCount; i++)
        {
            Vector3 spawnPosition = this.transform.position + new Vector3(Random.Range(-spawnPositionRange.x, spawnPositionRange.x), Random.Range(-spawnPositionRange.y, spawnPositionRange.y), 0);
            Quaternion spawnRotation = Quaternion.identity;
            GameObject spawnedObj = (GameObject)Instantiate(getRandomCollectible(), spawnPosition, spawnRotation);
            spawnedObj.transform.parent = this.transform.parent;
            yield return new WaitForSeconds(spawnWait);
        }

        for (int i = 0; i < asteroidCount; i++)
        {
            Vector3 spawnPosition = this.transform.position + new Vector3(Random.Range(-spawnPositionRange.x, spawnPositionRange.x), Random.Range(-spawnPositionRange.y, spawnPositionRange.y), 0);
            Quaternion spawnRotation = Quaternion.identity;
            GameObject spawnedObj = (GameObject)Instantiate(getRandomAsteroids(), spawnPosition, spawnRotation);
            spawnedObj.transform.parent = this.transform.parent;
            yield return new WaitForSeconds(spawnWait);
        }
    }


    private GameObject getRandomCollectible()
    {
        float[] cumulativeWeights = new float[collectibleObjects.Length];
        cumulativeWeights[0] = collectibleWeights[0];
        for (int i = 1; i < 2; ++i)
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
        for (int i = 1; i < 2; ++i)
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
