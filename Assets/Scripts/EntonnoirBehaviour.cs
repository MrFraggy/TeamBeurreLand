using UnityEngine;
using System.Collections;
using MiddleVR_Unity3D;

public class EntonnoirBehaviour : MonoBehaviour {

	public float WaterCapacity = 10;
	private float waterQuantity = 0;
	public Vector3 minHeight = new Vector3(0.0f, 0.0f, 0.0f);
	public Vector3 maxHeight = new Vector3(1.0f, 1.0f, 1.0f);
	public float minRadius = 0.1f;
	public float maxRadius = 0.5f;

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
		if (waterQuantity <= 0)
			waterSplash.enableEmission = false;
		else {
			waterSplash.startSpeed = initialStartSpeed * holeSize;
			waterSplash.enableEmission = true;
		}
	}

	void OnParticleCollision(GameObject other) {
		if (waterQuantity < WaterCapacity)
			waterQuantity += 1.0f - holeSize;

		waterSplash.emissionRate = initialEmissionRate * holeSize;

	}
}
