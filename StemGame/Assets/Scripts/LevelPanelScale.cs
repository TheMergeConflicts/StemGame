using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelPanelScale : MonoBehaviour {
    Vector2 originalSize;
    public GameObject panel;
	// Use this for initialization
	void Start () {
        originalSize = panel.GetComponent<RectTransform>().rect.size;
        panel.GetComponent<RectTransform>().rect.size.Set(0,0);
        panel.active = false;
      
    }
	
	public void scaleUp()
    {
        panel.active = true;
        float x = panel.GetComponent<RectTransform>().rect.size.x;
        float y = panel.GetComponent<RectTransform>().rect.size.y;
        while (x <originalSize.x && y < originalSize.y)
        {
            x += Time.deltaTime;
            y += Time.deltaTime;
            panel.GetComponent<RectTransform>().rect.size.Set(x, y);
        }
    }

    public void close()
    {
        panel.GetComponent<RectTransform>().rect.size.Set(0, 0);
        panel.active = false;
    }
}
