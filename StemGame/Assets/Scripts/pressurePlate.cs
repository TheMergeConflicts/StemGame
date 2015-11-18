using UnityEngine;
using System.Collections;
using UnityEngine.UI;

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

	void SetDoors(){
		doors = new Door[doorObjects.Length];
		for(int i = 0; i < doorObjects.Length; i++){
			doors[i] = doorObjects[i].GetComponent<Door>();
		}
	}

	// Update is called once per frame
	

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

	void openDoor(){


		for(int i = 0; i < doorObjects.Length; i++){
			doors[i].Open();
		}
		spriteR.color = Color.green;
	}

	void OnDrawGizmos(){
		if (doorObjects.Length > 0) {
			Gizmos.color = Color.red;
			for(int i = 0; i < doorObjects.Length; i++){
				Gizmos.DrawLine (gameObject.transform.position, doorObjects[i].transform.position);
			}


		}
	}
}
