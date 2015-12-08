using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// This class accepts specific elements to trigger
/// the filling of the affected objects, in this case
/// the water pit tiles specified in the editor
/// </summary>
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
	
	/// <summary>
    /// Finds specific UI components, to reduce the change of user error, and initializes the
    /// array of affected objects
    /// </summary>
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
	
	
	/// <summary>
    /// Checks to see if the collider entering the trigger area 
    /// has the required tag and name to activate the plate, and triggers
    /// a sound effect. It then locks the plate to prevent it from accepting additional
    /// elements
    /// </summary>
    /// <param name="other"> The 2d collider of a game object </param>
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


    /// <summary>
    /// If a grabbable object leaves the plate, this method marks it
    /// as unoccupied
    /// </summary>
    /// <param name="other">The 2d collider of a game object </param>
    void OnTriggerExit2D(Collider2D other){
		if (other.gameObject.tag == "Grabbable") {
      
            plateOccupied = false;
		}
	}
	/// <summary>
    /// Fills all the affected pit objects
    /// </summary>
	void performAction(){
		for (int i = 0; i < numAffectedObjects; i++) {
			affectedObjects[i].fill();
		}
		spriteR.color = Color.green;
	}
	/// <summary>
    /// Draws lines in the editor so the developer can see what objects
    /// are connected to the plate.
    /// </summary>
	void OnDrawGizmos(){
		//if (affectedGameObjects != null) {
			Gizmos.color = Color.red;
			for (int i = 0; i < numAffectedObjects; i++) {
				Gizmos.DrawLine (gameObject.transform.position, affectedGameObjects[i].transform.position);
			}

		//}
	}
}
