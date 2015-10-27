using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class FireControlPanel : MonoBehaviour {
    bool isPlayer;
    public FireScript flame;
    public GameObject panel;
    private Text text;
    private bool plateOccupied;

    // Use this for initialization
    void Start () {
        text = panel.transform.Find("Text").gameObject.GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
	    if (isPlayer && Input.GetKey(KeyCode.Space))
        {
            flame.fire();
        }
        
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        text.text = "Press Space";
        plateOccupied = true;
        if (target.tag.Equals("Player"))
        {
            isPlayer = true;
            
        }
    }

    void OnTriggerExit2D(Collider2D target)
    {
        if (target.tag.Equals("Player"))
        {
            plateOccupied = false;
            text.text = "";
            isPlayer = false;
        }

    }
}
