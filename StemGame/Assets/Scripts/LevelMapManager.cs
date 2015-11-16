using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelMapManager : MonoBehaviour {

	enum MODE { ZONES, PANEL};
    MODE mode = MODE.ZONES;
    GameObject panel;
	// Update is called once per frame
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
