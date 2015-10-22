using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HintUI : MonoBehaviour {
    Text text;
    ArrayList elements;
    GameObject[] obs;
    string water = "Hydrogen + Oxygen = Water;";
    string CO2 = "Carbon + Oxygen = CO2;";
    string methane = "Carbon + Hydrogen = Methane;";
    string hint;
    void Start () {
        text = GetComponent<Text>();
        obs = GameObject.FindGameObjectsWithTag("Grabbable");
        elements = new ArrayList();
        findElements();
        composeHint();
        text.text = hint;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

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

    void composeHint()
    {
        if(elements.Contains("Hydrogen") && elements.Contains("Carbon"))
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
