using UnityEngine;
using System.Collections;

public class ExitScript : MonoBehaviour {
    public string level;
    public int nextLevel;
	int completedLevel;

	void Start(){
		completedLevel = PlayerPrefs.GetInt ("LevelsCompleted");
	}

	void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag.Equals("Player")){
            if(nextLevel > completedLevel) { PlayerPrefs.SetInt("LevelsCompleted", nextLevel); } else
            {
                PlayerPrefs.SetInt("LevelsCompleted",completedLevel);
            }
            PlayerPrefs.Save();
            Application.LoadLevel(level);
        }
    }
}
