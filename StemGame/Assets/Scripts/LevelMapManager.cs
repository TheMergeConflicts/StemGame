using UnityEngine;
using UnityEngine.UI;
using System.Collections;
/// <summary>
/// This class helps the player select specific science zones, and opens up the level
/// selection panel for each zone
/// </summary>
public class LevelMapManager : MonoBehaviour {

	enum MODE { ZONES, PANEL};
    MODE mode = MODE.ZONES;
    GameObject panel;
	/// <summary>
    /// Uses a switch statement to go between the panel and the map
    /// The ZONES state uses raycasting to select a map zone square,
    /// and switches to the PANEL state, which opens the level selection panel
    /// </summary>
	void Update () {
        switch (mode)
        {
            case MODE.ZONES:
                if (Input.GetMouseButtonDown(0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                   
                    RaycastHit hit;
                    if (Physics.Raycast(ray.origin, ray.direction, out hit))
                    {
                        Collider coll = hit.collider;
                        string name = coll.gameObject.name;
                        panel = GameObject.Find(name + "Panel");
                        panel.GetComponent<LevelPanelScale>().scaleUp();
                        mode = MODE.PANEL;
                    }
                }
                break;
            case MODE.PANEL:
                if (Input.GetKey(KeyCode.Escape))
                {
                    panel.GetComponent<LevelPanelScale>().close();
                    mode = MODE.ZONES;
                    panel = null;
                }
                break;
        }
	}
}
