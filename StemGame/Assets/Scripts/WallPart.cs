using UnityEngine;
using System.Collections;
/// <summary>
/// Causes the game object to become more transparent over time, then self destruct
/// </summary>
public class WallPart : MonoBehaviour {
    float life = 4;
	// Use this for initialization
	void Start () {
        life = Time.time + life;
	}
	
	// Update is called once per frame
	void Update () {
        Vector4 currColor = GetComponent<SpriteRenderer>().color;
        GetComponent<SpriteRenderer>().color = new Vector4(currColor.x, currColor.y, currColor.z, currColor.w - (.1f / life));
	    if (Time.time > life)
        {
            Destroy(gameObject);
        }
	}
}
