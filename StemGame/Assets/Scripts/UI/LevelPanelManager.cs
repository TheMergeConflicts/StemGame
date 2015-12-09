using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Level panel manager.
/// </summary>
public class LevelPanelManager : MonoBehaviour {

	int levelsCompleted;

	public GameObject[] registeredLevels;

	/// <summary>
	/// Start this instance.
	/// </summary>
	void Start () {
		levelsCompleted = PlayerPrefs.GetInt ("LevelsCompleted");
		Debug.Log (levelsCompleted+"");
		SetLockImages ();
	}

	/// <summary>
	/// Refresh the states of the locking state of all levels, and set the locking image.
	/// </summary>
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
