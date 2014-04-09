using UnityEngine;
using System.Collections;

public class CloudBehaviour : MonoBehaviour {

	public float m_rainRate; // 0 : no rain, cloud white / 1 : about to rain, cloud black
	public GameObject m_rainPrefab;
	private bool m_isRaining;

	// Use this for initialization
	void Start () {

		m_rainRate = Random.Range(0, 100);
		m_isRaining = false;
	}
	
	// Update is called once per frame
	void Update () {

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
	}

	//Instantiate the rain
	void spawnRain(Vector3 position)
	{
		GameObject rain = (GameObject)Instantiate(m_rainPrefab, position, Quaternion.identity);
		//rain.particleSystem.Stop();
		rain.transform.parent = transform;
		//rain.transform.rotation = Quaternion.Euler(90f,0f,0f);
		//rain.transform.localScale = new Vector3(1f,1f,1f);
		//rain.particleSystem.Emit(1000);
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
