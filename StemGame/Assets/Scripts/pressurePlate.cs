using UnityEngine;
using System.Collections;

public class pressurePlate : MonoBehaviour {

	private SpriteRenderer spriteR;
	public GameObject doorObject; 
	private Door door;

	// Use this for initialization
	void Start () {
		spriteR = GetComponent<SpriteRenderer> ();
		door = doorObject.GetComponent<Door> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log(123);
		if(other.gameObject.name == "Player"){
			openDoor();

		}

	}

	void openDoor(){
		door.Open ();
		spriteR.color = Color.green;
	}

	void OnDrawGizmos(){
		if (doorObject != null) {
			Gizmos.color = Color.red;
			Gizmos.DrawLine (gameObject.transform.position, doorObject.transform.position);
		}
	}
}
