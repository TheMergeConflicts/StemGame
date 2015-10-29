using UnityEngine;
using System.Collections;

public class ExplodingWall : MonoBehaviour {
    public GameObject[] parts;
	// Use this for initialization
	
    public void Explode()
    {
        foreach (GameObject part in parts)
        {
            float x = Random.Range(-1, 1);
            float y = Random.Range(-1, 1);
            GameObject wallPart = Instantiate(part, transform.position, Quaternion.identity) as GameObject;
            wallPart.AddComponent<Rigidbody2D>();
            wallPart.AddComponent<WallPart>();
            wallPart.AddComponent<BoxCollider2D>();
            wallPart.GetComponent<Rigidbody2D>().gravityScale = 0;
            wallPart.GetComponent<Rigidbody2D>().AddForce(new Vector2(x *20, y *20));
        }
        Destroy(gameObject);
    }
}
