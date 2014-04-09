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
	public GameObject[] children = new GameObject[3];

	private float waterQuantity = 0;
	private vrJoystick entonnoirHole;
	private ParticleSystem waterSplash = null;
	private float holeSize;
	private float initialEmissionRate;
	private float initialStartSpeed;

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
			children [0].SetActive (false);
			children [1].SetActive (false);
			children [2].SetActive (false);
		}
		else {
			waterSplash.startSpeed = initialStartSpeed * holeSize;
			waterSplash.enableEmission = true;
			if (waterQuantity < WaterCapacity / 3.0f) {
				children[0].SetActive(true);
				children[1].SetActive(false);
				children[2].SetActive(false);
			}
			else if (waterQuantity < WaterCapacity / 3.0f * 2.0f) {
				children[0].SetActive(false);
				children[1].SetActive(true);
				children[2].SetActive(false);
			}
			else {
				children[0].SetActive(false);
				children[1].SetActive(false);
				children[2].SetActive(true);
			}
		}
	}

	void OnParticleCollision(GameObject other) {
		if (waterQuantity < WaterCapacity)
			waterQuantity += 1.0f - holeSize;

		waterSplash.emissionRate = initialEmissionRate * holeSize;

	}
}
