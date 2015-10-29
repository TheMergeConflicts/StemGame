using UnityEngine;
using System.Collections;

public class WallPart : MonoBehaviour {
    float life = 1;
	// Use this for initialization
	void Start () {
        life = Time.time + life;
	}
	
	// Update is called once per frame
	void Update () {
	    if (Time.time > life)
        {
            Destroy(gameObject);
        }
	}
}
