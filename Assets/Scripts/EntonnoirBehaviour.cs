using UnityEngine;
using System.Collections;

public class EntonnoirBehaviour : MonoBehaviour {

	public int WaterCapacity = 10;
	private int waterQuantity = 0;
	public Vector3 minHeight = new Vector3(0.0f, 0.0f, 0.0f);
	public Vector3 maxHeight = new Vector3(1.0f, 1.0f, 1.0f);
	public float minRadius = 0.1f;
	public float maxRadius = 0.5f;

	private ParticleSystem waterSplash = null;
	// Use this for initialization
	void Start () {
		waterSplash = gameObject.GetComponentInChildren<ParticleSystem> ();
		waterSplash.enableEmission = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnParticleCollision(GameObject other) {
		if (waterQuantity < WaterCapacity)
			++waterQuantity;
		else
			waterSplash.enableEmission = true;

	}
}
