using UnityEngine;
using System.Collections;

public class FireScript : MonoBehaviour {
    int animTime = 1;
    float startTime;
    SpriteRenderer render;
    public fireStarterPlate plate;
    public AudioSource fireS, debrisS;
    Vector3 originalPos;
    bool fired;
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
                fired = false;
                render.enabled = false;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        Debug.Log("Trigger Enter");
        if (target.tag.Equals("Exploding"))
        {
            Debug.Log("EXPLODE");
            target.GetComponent<ExplodingWall>().Explode();
            debrisS.Play();
        }
    }

	public void fire()
    {
        render.enabled = true;
        startTime = Time.time;
        if (!plate.rightElement && !fired)
        {
            transform.localScale = new Vector2(.5f, .5f);
            transform.position = new Vector3(originalPos.x - .5f, originalPos.y, originalPos.z);
            GetComponent<Collider2D>().enabled = false;
            fireS.clip = Resources.Load("SFX/StemLabLittleFire") as AudioClip;
        } else
        {
            fired = true;
            transform.localScale = new Vector2(2f, 2f);
            transform.position = new Vector3(originalPos.x + 1, originalPos.y, originalPos.z);
            GetComponent<Collider2D>().enabled = true;
            plate.burn();
            fireS.clip = Resources.Load("SFX/StemLabBigFire") as AudioClip;
        }
        if(!fireS.isPlaying)
            fireS.Play();
    }
}
