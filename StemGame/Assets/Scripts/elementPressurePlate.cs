using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class elementPressurePlate : MonoBehaviour {
	
	
	public GameObject panel;
	private Text text;
	public string elementNeeded;
	public float offsetX;
	public float offsetY;
	
	
	private SpriteRenderer spriteR;
	public GameObject[] affectedGameObjects;
	Door[] affectedObjects;
	private int numAffectedObjects;
	private bool plateOccupied;
	private Camera cam;
	
	// Use this for initialization
	void Start () {
		cam = Camera.main;
		text = panel.transform.Find ("Text").gameObject.GetComponent<Text> ();
		plateOccupied = false;
		spriteR = GetComponent<SpriteRenderer> ();
		//door = doorObject.GetComponent<Door> ();

		numAffectedObjects = affectedGameObjects.Length;
		affectedObjects = new Door[numAffectedObjects];

		for (int i = 0; i < numAffectedObjects; i++) {
			affectedObjects[i] = affectedGameObjects[i].GetComponent<Door>();
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		if(plateOccupied){
			panel.SetActive(true);
		}
		else{
			Debug.Log("false");
			panel.SetActive(false);
		}
	}
	
	void OnTriggerEnter2D(Collider2D other){
		
		if (other.gameObject.tag == "Grabbable") {
			Debug.Log("NICK");
			plateOccupied = true;
			if (other.gameObject.name == elementNeeded) { 
				SetupUI(true);
				performAction();
				
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
	
	void performAction(){
		for (int i = 0; i < numAffectedObjects; i++) {
			affectedObjects[i].Open ();
		}
		spriteR.color = Color.green;
	}
	
	void OnDrawGizmos(){
		if (affectedObjects != null) {
			Gizmos.color = Color.red;
			for (int i = 0; i < numAffectedObjects; i++) {
				Gizmos.DrawLine (gameObject.transform.position, affectedObjects[i].gameObject.transform.position);
			}

		}
	}
}
