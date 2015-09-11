using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	private SpriteRenderer spriteR;
	private Collider2D col;
	public bool open = false;


	// Use this for initialization
	void Start () {
		spriteR = GetComponent<SpriteRenderer> ();
		col = GetComponent<Collider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (open) {
			col.enabled = false;
			spriteR.color = Color.green;
		} 
		else {
			col.enabled = true;
			spriteR.color = Color.red;
		}
	}
}
