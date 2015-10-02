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
	public GameObject doorObject; 
	private Door door;
	private bool doorOpened;
	private bool plateOccupied;
	private Camera cam;

	// Use this for initialization
	void Start () {
		cam = Camera.main;
		text = panel.transform.Find ("Text").gameObject.GetComponent<Text> ();
		plateOccupied = false;
		spriteR = GetComponent<SpriteRenderer> ();
		door = doorObject.GetComponent<Door> ();


	}

	// Update is called once per frame
	void Update () {
		if(plateOccupied){
			panel.SetActive(true);
		}
		else{
			Debug.Log("faklse");
			//panel.SetActive(false);
		}
	}

	void OnTriggerEnter2D(Collider2D other){

		if (other.gameObject.tag == "Grabbable") {
			plateOccupied = true;
			if (other.gameObject.name == elementNeeded) { 
				SetupUI(true);
				openDoor ();

			} else {
				SetupUI(false);
			}
		}
	}

	void SetupUI(bool correctItem){
		Vector3 UIpos = cam.WorldToViewportPoint(transform.position);
		Rect rect_old = text.rectTransform.rect;
		Rect rect_new = new Rect(UIpos.x, UIpos.y, rect_old.width, rect_old.height);
		panel.GetComponent<RectTransform>().anchoredPosition = new Vector2 (UIpos.x + offsetX, UIpos.y + offsetY);
		if(correctItem){

			text.text = "Success";

		}
		else{
			text.text = "Need " + elementNeeded;
		}

	}

	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.tag == "Grabbable") {
			plateOccupied = false;
		}
	}

	void openDoor(){
		doorOpened = door.Open ();
		spriteR.color = Color.green;
	}

	void OnDrawGizmos(){
		if (doorObject != null) {
			Gizmos.color = Color.red;
			Gizmos.DrawLine (gameObject.transform.position, doorObject.transform.position);
		}
	}
}
