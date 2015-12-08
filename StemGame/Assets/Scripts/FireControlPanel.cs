using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// This script activates the fire script
/// </summary>
public class FireControlPanel : MonoBehaviour {
    bool isPlayer;
    public FireScript flame;
    public GameObject panel;
    private Text text;
    private bool plateOccupied;

    /// <summary>
    /// Finds the appropriate UI components to display a hint
    /// </summary>
    void Start () {
        panel = GameObject.Find("HintPanel2");
        text = panel.transform.Find("Text").gameObject.GetComponent<Text>();
    }
	
	/// <summary>
    /// If the player is in the trigger, and presses space
    /// then the fire is activated
    /// </summary>
	void Update () {
	    if (isPlayer && Input.GetKey(KeyCode.Space))
        {
            flame.fire();
        }
        
    }
    /// <summary>
    /// If the collider has the tag player, set isPlayer to true
    /// and display a hint
    /// </summary>
    /// <param name="target">2D collider object</param>
    void OnTriggerEnter2D(Collider2D target)
    {
        text.text = "Press Space";
        plateOccupied = true;
        if (target.tag.Equals("Player"))
        {
            isPlayer = true;
            
        }
    }
    /// <summary>
    /// Sets isplayer to false, deactivates hint
    /// </summary>
    /// <param name="target">2D collider object</param>
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
