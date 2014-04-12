using UnityEngine;
using System.Collections;

public class TextFollow : MonoBehaviour {

	public Vector3 offset;
	public Transform camera;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		transform.LookAt(transform.parent);
		transform.Rotate (Vector3.up, 180);
		//transform.position = camera.transform.position + offset;
	}
}
