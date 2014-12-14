using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIMainMenu : MonoBehaviour {

	public GameObject main;
	public GameObject start;
	public GameObject credits;
	private bool selectPlayer = false;
	private bool playerSelected = false;
	private bool[] players = new bool[4]{false,false,false,false};
	public Image[] playerImages;
	public Sprite[] playerSprites;
	public Sprite playerDisable;
	public Button playButton;

	public void Start(){
		start.transform.position = new Vector3(Screen.width/2f*3f, start.transform.position.y, 0f);
		credits.transform.position = new Vector3(Screen.width/2f*3f, credits.transform.position.y, 0f);
		playButton.interactable = false;
	}

	public void Update(){
		if(selectPlayer){
			for(int i = 0; i<players.Length; i++){
				string p = "P"+(i+1);
				if(players[i] != true && (Input.GetAxis(p + "_Vertical") > 0.01 || Input.GetAxis(p + "_Vertical") < -0.01 || Input.GetAxis(p + "_Horizontal") > 0.01 || Input.GetAxis(p + "_Horizontal") < -0.01)){
					if(!playerSelected){
						playerSelected = true;
						playButton.interactable = true;
					}
					players[i] = true;
					playerImages[i].sprite = playerSprites[i];
				}
			}
		}
	}

	public void StartGame(){
		//Application.LoadLevel("main_scene");
		selectPlayer = true;
		SwitchPage(main, start);
	}

	public void ReturnStartGame(){
		selectPlayer = false;
		playerSelected = false;
		playButton.interactable = false;
		for(int i = 0; i<players.Length; i++){
			players[i] = false;
			playerImages[i].sprite = playerDisable;
		}
		SwitchPage(start, main);
	}

	public void LaunchGame(){
		if(playerSelected){
			Application.LoadLevel("main_scene");
		}
	}

	public void Credits(){
		SwitchPage(main, credits);
	}

	public void ReturnCredits(){
		SwitchPage(credits, main);
	}

	public void SwitchPage(GameObject current, GameObject page){
		page.transform.position = new Vector3(Screen.width/2f*3f, page.transform.position.y, 0f);
		iTween.MoveTo(current, new Vector3(-Screen.width/2f, current.transform.position.y, 0f), 1f);
		iTween.MoveTo(page, new Vector3(Screen.width/2f, current.transform.position.y, 0f), 1f);
	}

	public void QuitGame(){
		Application.Quit();
	}
}
