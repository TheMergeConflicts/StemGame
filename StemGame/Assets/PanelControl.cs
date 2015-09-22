using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PanelControl : MonoBehaviour {
	private TempManager man;
	RectTransform trans;
	public GameObject target;
	public Text text;
	// Use this for initialization
	void Start () {
		man = target.GetComponent<TempManager> ();
		trans = GetComponent<RectTransform> ();
		trans.sizeDelta = new Vector2 (trans.sizeDelta.x, 1);
	}
	
	// Update is called once per frame
	void Update () {
		float scale = 127.31f;
		if(man){
			float temp = man.getTemp();
			text.text = temp.ToString() + "K";
			float height = scale * temp / 1000f;
			trans.sizeDelta = new Vector2 (trans.sizeDelta.x, height);
		}
	}
}
