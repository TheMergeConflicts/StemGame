using UnityEngine;
using System.Collections;

public class LevelSelect_UIManager : MonoBehaviour {

	public GameObject[] levelObjects;

	public void HandleLevel(int levelNum){
		Application.LoadLevel ("Level" + levelNum);
	}
}
