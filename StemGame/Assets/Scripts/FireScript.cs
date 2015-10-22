using UnityEngine;
using System.Collections;

public class FireScript : MonoBehaviour {
    int animTime = 1;
    float startTime;
    SpriteRenderer render;
	void Start () {
        render = GetComponent<SpriteRenderer>();

	}
	
    void Update()
    {
        if(render.enabled)
        {
          if (Time.time - startTime > animTime)
            {
                render.enabled = false;
            }
        }
    }

	public void fire()
    {
        render.enabled = true;
        startTime = Time.time;
    }
}
