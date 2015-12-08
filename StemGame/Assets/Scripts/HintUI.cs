using UnityEngine;
using UnityEngine.UI;
using System.Collections;
/// <summary>
/// This class scans the scene and prepares a text message for the user
/// based on the elements in the scene
/// </summary>
public class HintUI : MonoBehaviour {
    Text text;
    public  ArrayList elements;
    public GameObject[] obs;
    string water = "Hydrogen + Oxygen = Water; Water + COLD = ICE\n";
    string CO2 = "Carbon + Oxygen = CO2;\n";
    string methane = "(Carbon + Hydrogen) + HEAT = Methane; Methane + fire = BIG FIRE;\n";
    public string hint;
    /// <summary>
    /// finds all the Grabbable (element) objects in the scene
    /// </summary>
    void Start () {
        text = GetComponent<Text>();
        obs = GameObject.FindGameObjectsWithTag("Grabbable");
        elements = new ArrayList();
        findElements();
        composeHint();
        text.text = hint;
	}
	
	
    /// <summary>
    /// Composes an arraylist of elements present in the scene, removing duplicates
    /// </summary>
    void findElements()
    {
        foreach (GameObject obj in obs)
        {
            if (!elements.Contains(obj.name))
            {
                elements.Add(obj.name);
            }
        }
    }
    /// <summary>
    /// Prepares a text message based on the available compounds that
    /// can be created, and writes the message to the UI canvas
    /// </summary>
    void composeHint()
    {
       
        if (elements.Contains("Hydrogen") && elements.Contains("Carbon"))
        {
            hint += (" "+methane);
        }
        if (elements.Contains("Hydrogen") && elements.Contains("Oxygen"))
        {
            hint += (" " + water);
        }
        if (elements.Contains("Carbon") && elements.Contains("Oxygen"))
        {
            hint += (" " + CO2);
        }

    }
}
