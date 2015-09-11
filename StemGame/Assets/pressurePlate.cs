using UnityEngine;
using System.Collections;

public class pressurePlate : MonoBehaviour {

	private SpriteRenderer spriteR;
	public Door door;

	// Use this for initialization
	void Start () {
		spriteR = GetComponent<SpriteRenderer> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log(123);
		if(other.gameObject.name == "PlayerCube"){
			openDoor();

		}

	}

	void openDoor(){
		door.open = true;
		spriteR.color = Color.green;
	}
}
