using UnityEngine;
using System.Collections;

/// <summary>
/// UI manage for levelSelect scene.
/// </summary>
public class LevelSelect_UIManager : MonoBehaviour {

	public GameObject[] levelObjects;

	/// <summary>
	/// This method will be called for loading a level
	/// </summary>
	/// <param name="levelNum">the number of the level to be loaded</param>
	public void HandleLevel(int levelNum){
		Application.LoadLevel ("Level" + levelNum);
	}
}
