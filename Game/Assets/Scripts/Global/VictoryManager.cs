using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VictoryManager : MonoBehaviour {
	public GameObject[] players;
	public Text victoryText;
	private Vector3[] start = new Vector3[4]{
		new Vector3(-15.74f, -4.6f, 0.57f),
		new Vector3(-30.81f, -4.9f, 4.31f),
		new Vector3(-37.67f, -14.96f, 6.77f),
		new Vector3(-60.99f, -15.71f, 21.48f)
	};
	private Vector3[] destinations = new Vector3[4]{
		new Vector3(1.63f, 0.71f, -3.22f),
		new Vector3(-6.42f, 4.27f, 1.32f),
		new Vector3(-5.92f, -5.92f, 4.02f),
		new Vector3(-28.79f, -3.06f, 20.85f)
	};

	private Vector3[] destRotation = new Vector3[4]{
		new Vector3(350.61f, 9.4832f, 16.867f),
		new Vector3(333.98f, 356.49f, 22.896f),
		new Vector3(1.001f, 2.0783f, 15.379f),
		new Vector3(354.39f, 358.91f, 21.547f)
	};

	void Awake (){
		for(int i = 0; i < StateManager.playersRank.Length; i++){
			GameObject player = players[StateManager.playersRank[i]-1];
			player.transform.eulerAngles = destRotation[i];
			player.transform.position = start[i];
			player.SetActive(true);
		}
	}

	// Use this for initialization
	void Start () {
		victoryText.text = "Player "+StateManager.playersRank[0];
		for(int i = 0; i < StateManager.playersRank.Length; i++){
			GameObject player = players[StateManager.playersRank[i]-1];
			iTween.MoveTo(player, destinations[i], 2.5f);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
