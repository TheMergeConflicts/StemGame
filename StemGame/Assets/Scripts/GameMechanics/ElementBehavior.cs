using UnityEngine;
using System.Collections;

public class ElementBehavior : MonoBehaviour {
	public string elementName;
	public ElementBehavior[] legalCombination;
	public ElementBehavior[] newCompound;

	void OnCollisionEnter2D(Collision2D collider) {

		ElementBehavior collideElement = collider.collider.GetComponent<ElementBehavior> ();
		if (collideElement != null) {
			checkLegalCombination(collideElement);

		}
	}

	void checkLegalCombination(ElementBehavior checkBehavior) {
		int i = 0;
		foreach (ElementBehavior ele in legalCombination) {

			if (checkBehavior.elementName == ele.elementName) {
				Destroy(checkBehavior.gameObject);
				Instantiate(newCompound[i].gameObject, checkBehavior.transform.position, new Quaternion());
				Destroy (this.gameObject);
			}
			i++;
		}
	}


}
