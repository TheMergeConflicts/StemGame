using UnityEngine;
using System.Collections;

public class ExitScript : MonoBehaviour {
    public string level;

	int completedLevel;

	void Start(){
		completedLevel = PlayerPrefs.GetInt ("LevelsCompleted");
	}

	void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag.Equals("Player")){
			PlayerPrefs.SetInt("LevelsCompleted", ++completedLevel);
            Application.LoadLevel(level);
        }
    }
}
