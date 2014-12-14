using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class VictoryManager : MonoBehaviour {
	public GameObject[] players;
	public Text victoryText;
	private Vector3[] start = new Vector3[4]{
		new Vector3(-15.74f, -4.6f, 0.57f),
		new Vector3(),
		new Vector3(),
		new Vector3()
	};
	private Vector3[] destinations = new Vector3[4]{
		new Vector3(1.63f, 0.71f, -3.22f),
		new Vector3(-6.42f, 4.27f, 1.32f),
		new Vector3(-5.92f, -5.92f, 4.02f),
		new Vector3()
	};

	void Awake (){
		/*for(int i = 0; i < StateManager.playersRank.Length; i++){
			GameObject player = players[StateManager.playersRank[i]-1];
			player.transform.position = start[i];
			player.SetActive(true);
		}*/
	}

	// Use this for initialization
	void Start () {
		/*victoryText.text = "Player "+StateManager.playersRank[0];
		iTween.MoveTo(players[StateManager.playersRank[0]-1], destinations[0], 2.5f);*/
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
