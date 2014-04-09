using UnityEngine;
using System.Collections;

public class RainDetector : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter (Collider other) {
		if (other.tag == "Rain") {
			Debug.Log ("Patafouin");
		}
		
	}
}
