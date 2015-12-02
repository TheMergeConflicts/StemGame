using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LevelPanelManager : MonoBehaviour {

	int levelsCompleted;

	public GameObject[] registeredLevels;

	// Use this for initialization
	void Start () {
		levelsCompleted = PlayerPrefs.GetInt ("LevelsCompleted") - 1;
		SetLockImages ();
	}

	void SetLockImages(){
		for(int i = 0; i < registeredLevels.Length; i++){
			GameObject level = registeredLevels[i];
			Button button = level.GetComponent<Button>();
			GameObject lockImage = level.transform.Find("LockImage").gameObject;

			if(i <= levelsCompleted){
				button.interactable = true;
				lockImage.SetActive(false);
			}
			else{
				button.interactable = false;
				lockImage.SetActive(true);
			}

		}
	}
}
