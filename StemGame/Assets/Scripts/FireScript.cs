using UnityEngine;
using System.Collections;

public class FireScript : MonoBehaviour {
    int animTime = 1;
    float startTime;
    SpriteRenderer render;
    public fireStarterPlate plate;
    Vector3 originalPos;
	void Start () {
        render = GetComponent<SpriteRenderer>();
        transform.localScale =new Vector2(.5f, .5f);
        originalPos = transform.position;
        render.enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }
	
    void Update()
    {
        if(render.enabled)
        {
          if (Time.time - startTime > animTime)
            {
                transform.position = originalPos;
                render.enabled = false;
            }
        }
    }

    

	public void fire()
    {
        render.enabled = true;
        startTime = Time.time;
        if (plate.rightElement)
        {
            transform.localScale = new Vector2(2f, 2f);
            transform.position = new Vector3(originalPos.x + 1, originalPos.y, originalPos.z);
            GetComponent<Collider2D>().enabled = true;
            plate.burn();
        } else
        {
            transform.localScale = new Vector2(.5f, .5f);
            transform.position = new Vector3(originalPos.x - .5f, originalPos.y, originalPos.z);
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
