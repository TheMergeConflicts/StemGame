using UnityEngine;
using System.Collections;
/// <summary>
/// Activates the fire sprites and adjusts their scale based on the 
/// presence of methane in the fire starte plate
/// </summary>
public class FireScript : MonoBehaviour {
    int animTime = 1;
    float startTime;
    SpriteRenderer render;
    public fireStarterPlate plate;
    public AudioSource fireS, debrisS;
    Vector3 originalPos;
    bool fired;
    /// <summary>
    /// Initializes the components in their default, non-burning state
    /// </summary>
	void Start () {
        render = GetComponent<SpriteRenderer>();
        transform.localScale =new Vector2(.5f, .5f);
        originalPos = transform.position;
        render.enabled = false;
        GetComponent<Collider2D>().enabled = false;
    }
	/// <summary>
    /// If the renderer is enables, starts a timer, after which it disables
    /// the renderer
    /// </summary>
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
    /// <summary>
    /// If the fire contacts an exploding wall, destroys the wall
    /// </summary>
    /// <param name="target"> 2D collider, preferebly belonging to an exploding wall</param>
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
    /// <summary>
    /// Turns on the renderer for the fire, and determines size based
    /// on the presence of methane in the fire starter plate
    /// </summary>
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
