using UnityEngine;
using System.Collections;
using UnityEngine.UI;
/// <summary>
/// This class serves to alert the user if they have the 
/// correct element placed in front of the flamethrower
/// </summary>
public class fireStarterPlate : MonoBehaviour
{


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
    /// <summary>
    /// Finds the appropriate UI components
    /// </summary>
	void Start()
    {
        panel = GameObject.Find("HintPanel2");
        cam = Camera.main;
        text = panel.transform.Find("Text").gameObject.GetComponent<Text>();
        plateOccupied = false;
        spriteR = GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Highlights the UI panel if the element ini the trigger is incorrect
    /// </summary>
    /// <param name="other"> 2D collider object </param>
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.tag == "Grabbable")
        {
            plateOccupied = true;

            if (other.gameObject.name.Contains(elementNeeded))
            {
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


    /// <summary>
    /// If the object leaves, disables the UI panel text
    /// </summary>
    /// <param name="other"> 2D collider object </param>
    void OnTriggerExit2D(Collider2D other)
    {

        if (other.gameObject.tag == "Grabbable")
        {
            plateOccupied = false;
            rightElement = false;
            element = null;
        }
    }
    /// <summary>
    /// Burns the methane element, destroying it
    /// </summary>
    public void burn()
    {
        if (element != null)
        {
            Destroy(element);
            rightElement = false;
        }
    }
	
}
