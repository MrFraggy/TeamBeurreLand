using UnityEngine;
using System.Collections;

public class VillageBehavior : MonoBehaviour {

	/** <summary>
	  * The water required to save the village.
	  * </summary>
	  */
	public int WaterRequired = 10;

	private int rainQuantity;

	// Use this for initialization
	void Start () {
		rainQuantity = 0;
	}

	void OnParticleCollision (GameObject other) {
		++rainQuantity;

		if (rainQuantity >= WaterRequired) {
			Debug.Log ("Village Rescued");
			gameObject.renderer.material.color = Color.green;
			this.collider.enabled = false;
		}
	}
}
