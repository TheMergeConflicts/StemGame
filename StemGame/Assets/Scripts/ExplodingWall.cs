using UnityEngine;
using System.Collections;

public class ExplodingWall : MonoBehaviour {
    public GameObject[] parts;
	// Use this for initialization
	
    public void Explode()
    {
        float div = (2*Mathf.PI)/parts.Length;
        float angle = 0;
        foreach (GameObject part in parts)
        {
            float x = Mathf.Sin(angle);
            float y = Mathf.Cos(angle);
            angle += div;
            GameObject wallPart = Instantiate(part, transform.position, Quaternion.identity) as GameObject;
            wallPart.AddComponent<WallPart>();
            wallPart.AddComponent<BoxCollider2D>();
            wallPart.GetComponent<Rigidbody2D>().AddForce(new Vector2(x *100, y *100));
        }
        Destroy(gameObject);
    }
}
