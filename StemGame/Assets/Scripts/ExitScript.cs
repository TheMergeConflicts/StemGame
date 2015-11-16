using UnityEngine;
using System.Collections;

public class ExitScript : MonoBehaviour {
    public string level;
	
	void OnTriggerEnter2D(Collider2D target)
    {
        if (target.gameObject.tag.Equals("Player")){
            Application.LoadLevel(level);
        }
    }
}
