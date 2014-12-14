using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MainGameScript : MonoBehaviour {

    public GameObject[] playerGameObjList;
    private List<Player> playerList;

    public int energyToWin;
    public List<int> winPlayer;

    private int numberOfPlayer;

	// Use this for initialization
	void Start () {

        numberOfPlayer = 0;
        playerList = new List<Player>();

        Transform parentSpawner = GameObject.FindGameObjectWithTag("ForeGround").GetComponent<Transform>();

        Vector3 spawnPosition = Vector3.zero;
        Quaternion spawnRotation = Quaternion.identity;

        int initPos = 2;


        for (int i = 0; i < StateManager.players.Length; ++i)
        {
            spawnPosition = new Vector3(-7, initPos, 0);
            GameObject spawnedObj = (GameObject)Instantiate(playerGameObjList[StateManager.players[i] - 1], spawnPosition, spawnRotation);
            Player playerSpawned = spawnedObj.GetComponent<Player>();
            playerSpawned.energyToLightSpeed = energyToWin;
            playerList.Add(playerSpawned);
            initPos -= 2;
            numberOfPlayer++;
        }

        /*if (StateManager.players.Length >= 1)
        {
            spawnPosition = new Vector3(-7, 2, 0);
            GameObject spawnedObj = (GameObject)Instantiate(playerGameObjList[0], spawnPosition, spawnRotation);
            Player playerSpawned = spawnedObj.GetComponent<Player>();
            playerSpawned.energyToLightSpeed = energyToWin;
            playerList.Add(playerSpawned);
        }

        if (StateManager.players.Length >= 2)
        {
            spawnPosition = new Vector3(-7, 0, 0);
            GameObject spawnedObj = (GameObject)Instantiate(playerGameObjList[1], spawnPosition, spawnRotation);
            Player playerSpawned = spawnedObj.GetComponent<Player>();
            playerSpawned.energyToLightSpeed = energyToWin;
            playerList.Add(playerSpawned);
        }

        if (StateManager.players.Length >= 3)
        {
            spawnPosition = new Vector3(-7, -2, 0);
            GameObject spawnedObj = (GameObject)Instantiate(playerGameObjList[2], spawnPosition, spawnRotation);
            Player playerSpawned = spawnedObj.GetComponent<Player>();
            playerSpawned.energyToLightSpeed = energyToWin;
            playerList.Add(playerSpawned);
        }

        if (StateManager.players.Length >= 4)
        {
            spawnPosition = new Vector3(-7, -4, 0);
            GameObject spawnedObj = (GameObject)Instantiate(playerGameObjList[3], spawnPosition, spawnRotation);
            Player playerSpawned = spawnedObj.GetComponent<Player>();
            playerSpawned.energyToLightSpeed = energyToWin;
            playerList.Add(playerSpawned);
        }*/
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        for (int i = 0; i < numberOfPlayer; ++i)
        {
            if (playerList[i].energyLevel >= energyToWin)
            {
                winPlayer.Add(playerList[i].GetPlayerNumber());
                Debug.Log("Player " + playerList[i].GetPlayerNumber() + " win!!");
                playerList[i].playerControl.GoToLightSpeed();
                playerList.RemoveAt(i);
                numberOfPlayer--;
            }
        }

        if (numberOfPlayer == 0)
        {
            // END GAME!
            StateManager.playersRank = winPlayer.ToArray();
            StartCoroutine("launchEndScene");
        }
	}

    IEnumerator launchEndScene()
    {
        yield return new WaitForSeconds(1);

        Application.LoadLevel(2);
    }
}
