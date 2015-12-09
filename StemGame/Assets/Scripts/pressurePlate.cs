using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Pressure plate.
/// </summary>
public class pressurePlate : MonoBehaviour {
	public GameObject panel;
	private Text text;
	public string elementNeeded;
	public float offsetX;
	public float offsetY;


	private SpriteRenderer spriteR;
	public GameObject[] doorObjects; 
	private Door[] doors;
	private bool doorOpened;
	private bool plateOccupied;
	private Camera cam;

	// Use this for initialization
	void Start () {
        panel = GameObject.Find("HintPanel2");
		cam = Camera.main;
		text = panel.transform.Find ("Text").gameObject.GetComponent<Text> ();
		plateOccupied = false;
		spriteR = GetComponent<SpriteRenderer> ();
		SetDoors ();
        doorOpened = false;

	}

	/// <summary>
	/// Initialize the doors array.
	/// </summary>
	void SetDoors(){
		doors = new Door[doorObjects.Length];
		for(int i = 0; i < doorObjects.Length; i++){
			doors[i] = doorObjects[i].GetComponent<Door>();
		}
	}

	void OnTriggerEnter2D(Collider2D other){

		if (other.gameObject.tag == "Grabbable" || other.gameObject.tag == "Player") {
			plateOccupied = true;
			if (other.gameObject.name.Contains(elementNeeded)) { 
				
				openDoor ();
                if (!doorOpened) {
                    gameObject.GetComponent<AudioSource>().Play();
                }
                doorOpened = true;
            }
            else
            {
                text.text = elementNeeded + " needed";
            }
        }
	}


	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.tag == "Grabbable" || other.gameObject.tag == "Player") {
			plateOccupied = false;
            text.text = "";
		}
	}

	/// <summary>
	/// Opens the door.
	/// </summary>
	void openDoor(){
		for(int i = 0; i < doorObjects.Length; i++){
			doors[i].Open();
		}
		spriteR.color = Color.green;
	}

	/// <summary>
	/// Draws a line between a pressure plate and the doors linked
	/// </summary>
	void OnDrawGizmos(){
		if (doorObjects.Length > 0) {
			Gizmos.color = Color.red;
			for(int i = 0; i < doorObjects.Length; i++){
				Gizmos.DrawLine (gameObject.transform.position, doorObjects[i].transform.position);
			}
		}
	}
}
