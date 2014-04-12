using UnityEngine;
using System.Collections;

public class Objectives : MonoBehaviour {

	public int villagesRescued = 0;
	public int maxVillagesToRescue = 3;
	public GameObject text;
	public Camera camera;

	// Use this for initialization
	void Start () {
		text.renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.P))
			rescued();
	}

	public void rescued() {
		villagesRescued++;
		if(villagesRescued >= maxVillagesToRescue) {
			Debug.Log("Game over");
			text.transform.parent = GameObject.Find("CameraStereo0").transform;
			text.renderer.enabled = true;
		}
	}
}
