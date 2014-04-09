using UnityEngine;
using System.Collections;
using MiddleVR_Unity3D;

public class EntonnoirBehaviour : MonoBehaviour {

	public float WaterCapacity = 10;
	public float MinWater = 0.2f;
	public Vector3 minHeight = new Vector3(0.0f, 0.0f, 0.0f);
	public Vector3 maxHeight = new Vector3(1.0f, 1.0f, 1.0f);
	public float minRadius = 0.1f;
	public float maxRadius = 0.5f;
	public GameObject[] children = new GameObject[2];

	private float waterQuantity = 0;
	private vrJoystick entonnoirHole;
	private ParticleSystem waterSplash = null;
	private float holeSize;
	private float initialEmissionRate;
	private float initialStartSpeed;
	private Vector3 minPosition;
	private Vector3 maxPosition;
	private Vector3 minScale;
	private Vector3 maxScale;

	// Use this for initialization
	void Start () {
		waterSplash = gameObject.GetComponentInChildren<ParticleSystem> ();
		waterSplash.enableEmission = false;
		holeSize = 1.0f;
		initialEmissionRate = waterSplash.emissionRate;
		initialStartSpeed = waterSplash.startSpeed;
		if (MiddleVR.VRDeviceMgr != null) {
			entonnoirHole = MiddleVR.VRDeviceMgr.GetJoystick ("RazerHydra.Joystick0");
		}

		minPosition = children[0].transform.localPosition;
		maxPosition = children [1].transform.localPosition;
		minScale = children [0].transform.localScale;
		maxScale = children [1].transform.localScale;
		children [0].SetActive (false);
		children [1].SetActive (false);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (entonnoirHole == null)
			return;
		float axis = entonnoirHole.GetAxisValue(2);
		holeSize = 1.0f - axis;

		if (waterQuantity > 0)
			waterQuantity -= holeSize;
		if (waterQuantity <= MinWater) {
			waterSplash.enableEmission = false;
			children[0].SetActive(false);
		}
		else {
			waterSplash.startSpeed = initialStartSpeed * holeSize;
			waterSplash.enableEmission = true;
			float factor = waterQuantity / WaterCapacity;
			children[0].transform.localPosition = Vector3.Lerp(minPosition, maxPosition, factor);
			children[0].transform.localScale = Vector3.Lerp (minScale, maxScale, factor);
			children[0].SetActive (true);
		}
	}

	void OnParticleCollision(GameObject other) {
		if (waterQuantity < WaterCapacity)
			waterQuantity += 1.0f - holeSize;

		waterSplash.emissionRate = initialEmissionRate * holeSize;

	}
}
