using UnityEngine;
using System.Collections;
using MiddleVR_Unity3D;

public class EntonnoirBehaviour : MonoBehaviour {

	public enum State {SplashEnable, SplashDisable};

	public float WaterCapacity = 10;
	public float MinWater = 0.2f;
	public float WaterMultiplier = 10.0f;
	public GameObject[] children = new GameObject[2];

	private State state = State.SplashDisable;
	private AudioSource dropSound;
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
		dropSound = gameObject.GetComponentInChildren<AudioSource> ();
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

		if (state == State.SplashEnable) {
			waterSplash.emissionRate = initialEmissionRate * holeSize;
			waterSplash.startSpeed = initialStartSpeed * holeSize;
			waterQuantity -= holeSize;
			waterSplash.enableEmission = true;
			float factor = waterQuantity / WaterCapacity;
			children[0].transform.localPosition = Vector3.Lerp(minPosition, maxPosition, factor);
			children[0].transform.localScale = Vector3.Lerp (minScale, maxScale, factor);
			children[0].SetActive (true);
			if (waterQuantity < 0) {
				state = State.SplashDisable;
			}
		}

		else if (state == State.SplashDisable) {
			waterSplash.enableEmission = false;
			children[0].SetActive (false);
			if (waterQuantity > 0) {
				state = State.SplashEnable;
			}
		}

		Debug.Log(waterQuantity);
	}

	void OnParticleCollision(GameObject other) {
		if (waterQuantity < WaterCapacity) 
			waterQuantity += (1.0f - holeSize) * WaterMultiplier;

		dropSound.Play ();

	}
}
