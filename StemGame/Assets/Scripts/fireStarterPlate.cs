using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class fireStarterPlate : MonoBehaviour {


    public GameObject panel;
    private Text text;
    public string elementNeeded;
	public float offsetX;
	public float offsetY;
	
	
	private SpriteRenderer spriteR;

    public bool rightElement;
	private int numAffectedObjects;
	private bool plateOccupied;
	private Camera cam;
    GameObject element;
	// Use this for initialization
	void Start () {
        panel = GameObject.Find("HintPanel2");
        cam = Camera.main;
		text = panel.transform.Find ("Text").gameObject.GetComponent<Text> ();
		plateOccupied = false;
		spriteR = GetComponent<SpriteRenderer> ();
		//door = doorObject.GetComponent<Door> ();


	
		
	}

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnTriggerEnter2D(Collider2D other){
		
		if (other.gameObject.tag == "Grabbable") {
			Debug.Log("NICK");
			plateOccupied = true;

			if (other.gameObject.name.Contains(elementNeeded)) { 
				Debug.Log("right object");
                rightElement = true;
                element = other.gameObject;
            }
            else
            {
             
                rightElement = false;
                text.text = elementNeeded + " needed";
                element = null;
            }
        }
	}
	

	
	void OnTriggerExit2D(Collider2D other){

        if (other.gameObject.tag == "Grabbable") {
			plateOccupied = false;
            rightElement = false;
            element = null;
		}
	}
	
    public void burn()
    {
        if (element != null)
        {
            Destroy(element);
            rightElement = false;
        }
    }
	
}
