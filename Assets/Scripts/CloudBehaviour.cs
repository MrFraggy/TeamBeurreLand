using UnityEngine;
using System.Collections;

public class CloudBehaviour : MonoBehaviour {

	public float m_rainRate; // 0 : no rain, cloud white / 1 : about to rain, cloud black
	public GameObject m_rainPrefab;
	private bool m_isRaining;
	private ParticleSystem particleSystem;
	// Use this for initialization
	void Start () {

		m_rainRate = Random.Range(0, 100);
		//m_isRaining = false;
		spawnRain(this.transform.position);
		particleSystem = gameObject.GetComponent<ParticleSystem>();

	}
	
	// Update is called once per frame
	void Update () {
		return;
		//MOTION
		if ((transform.position.x < 0.0f && transform.position.x > -50.0f) || (transform.position.x > 0.0f && transform.position.x < 50.0f)) {
			transform.position += new Vector3 (Time.deltaTime * Random.Range (1, 5), 0, 0);
		}
		//RAIN
		if(m_isRaining == true){
			m_rainRate = m_rainRate - Time.deltaTime*10;

			if (m_rainRate <= 0.0f){

				// Suppress rain
				suppressRain();
				m_rainRate = Random.Range(0, 100);
				m_isRaining = false;
			}
		}else if (m_isRaining ==false){

			m_rainRate = m_rainRate + Time.deltaTime*10;
			if (m_rainRate >= 100.0f){
				// Create rain
				spawnRain(this.transform.position);
				m_rainRate = Random.Range(0, 100);
				m_isRaining = true;
			}
		}

		colorCloud(m_rainRate);

	}

	//Instantiate the rain
	void spawnRain(Vector3 position)
	{
		GameObject rain = (GameObject)Instantiate(m_rainPrefab, position, Quaternion.identity);
		rain.transform.parent = transform;
		rain.transform.Rotate (0,90,0);
		rain.transform.Translate (0, -5, 0);
	}

	void colorCloud(float m_rainRate){
		float  colorIndex = m_rainRate;
		if(colorIndex < 30){
			colorIndex = 30;
		}else if(colorIndex > 270){
			colorIndex = 270;
		}
		gameObject.renderer.material.color = new Color(colorIndex/10, colorIndex/10, colorIndex/10);
	}

	// Suppress the rain
	void suppressRain()
	{
		foreach(Transform child in this.transform){
			if(child.tag == "Rain"){
				Destroy(child.gameObject);
			}
		}
	
	}
}
