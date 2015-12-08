using UnityEngine;
using System.Collections;
/// <summary>
/// This script opens a new level
/// </summary>
public class ExitScript : MonoBehaviour {
    public string level;
    public int nextLevel;
	int completedLevel;
    /// <summary>
    /// Gets the number of levels the players have completed
    /// </summary>
	void Start(){
		completedLevel = PlayerPrefs.GetInt ("LevelsCompleted");
	}
    /// <summary>
    /// If the player enters the trigger area, this method opens the next level specified
    /// by the dev eloper. It then double checks whether this level has a higher number than
    /// the number of levels completed, and adjusts the number of levels completed accordingly
    /// </summary>
    /// <param name="target"> The 2d collider of a game object </param>
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
