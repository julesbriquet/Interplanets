using UnityEngine;
using System.Collections;

public class UIMainMenu : MonoBehaviour {

	public void StartGame(){
		Application.LoadLevel("main_scene");
	}

	public void HowToPlay(){
		//Application.LoadLevel("main_scene");
	}

	public void QuitGame(){
		Application.Quit();
	}
}
