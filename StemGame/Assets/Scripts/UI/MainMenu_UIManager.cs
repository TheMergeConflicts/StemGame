using UnityEngine;
using System.Collections;

public class MainMenu_UIManager : MonoBehaviour {

	public GameObject OptionsPanel;
	public GameObject CreditsPanel;

	// Use this for initialization
	void Start () {
        if (!PlayerPrefs.HasKey("LevelsCompleted"))
        {

            PlayerPrefs.SetInt("LevelsCompleted", 1);
        }
	}
	
	public void HandleStartBtn(){
		Debug.Log ("Start");
		Application.LoadLevel ("LevelSelect");
	}

	public void HandleOptionsBtn(){
		Debug.Log ("Options");
		OptionsPanel.SetActive (true);	
	}

	public void HandleCreditsBtn(){
		Debug.Log ("Credits");
		CreditsPanel.SetActive (true);
	}

	public void HandleQuitBtn(){
		Debug.Log ("Quit");
		Application.Quit ();
	}

	public void HandleOptionsBtnBack(){
		OptionsPanel.SetActive (false);
	}

	public void HandleCreditsBtnBack(){
		CreditsPanel.SetActive (false);
	}
}
