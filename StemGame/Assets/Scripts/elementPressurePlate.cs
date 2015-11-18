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
	WaterPit[] affectedObjects;
	private int numAffectedObjects;
	private bool plateOccupied;
    private bool locked;
	
	// Use this for initialization
	void Start () {
        panel = GameObject.Find("HintPanel2");
		text = panel.transform.Find ("Text").gameObject.GetComponent<Text> ();
		plateOccupied = false;
		spriteR = GetComponent<SpriteRenderer> ();
		//door = doorObject.GetComponent<Door> ();

		numAffectedObjects = affectedGameObjects.Length;
		affectedObjects = new WaterPit[numAffectedObjects];

		for (int i = 0; i < numAffectedObjects; i++) {
			affectedObjects[i] = affectedGameObjects[i].GetComponent<WaterPit>();
		}
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerEnter2D(Collider2D other){
		
		if (other.gameObject.tag == "Grabbable" && !locked) {
			
			plateOccupied = true;
			if (other.gameObject.name.Contains(elementNeeded)) { 
				Debug.Log("right object");
				performAction();
                Destroy(other.gameObject);
                gameObject.GetComponent<AudioSource>().pitch = 1.5f;
                gameObject.GetComponent<AudioSource>().Play();
                plateOccupied = false;
                locked = true;
            }  else
            {
                
                text.text = elementNeeded + " needed";
            }
		}
	}
	

	
	void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.tag == "Grabbable") {
      
            plateOccupied = false;
		}
	}
	
	void performAction(){
		for (int i = 0; i < numAffectedObjects; i++) {
			affectedObjects[i].fill();
		}
		spriteR.color = Color.green;
	}
	
	void OnDrawGizmos(){
		//if (affectedGameObjects != null) {
			Gizmos.color = Color.red;
			for (int i = 0; i < numAffectedObjects; i++) {
				Gizmos.DrawLine (gameObject.transform.position, affectedGameObjects[i].transform.position);
			}

		//}
	}
}
