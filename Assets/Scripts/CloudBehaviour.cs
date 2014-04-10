using UnityEngine;
using System.Collections;

public class CloudBehaviour : MonoBehaviour {

	public float m_rainRate = 0; // 0 : no rain, cloud white / 1 : about to rain, cloud black
	public GameObject m_rainPrefab;
	private bool m_isRaining = false;
    public float timeToLive = 30f;
    public CloudMotion manager;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

		//MOTION
		if ((transform.position.x < 0.0f && transform.position.x > -50.0f) || (transform.position.x > 0.0f && transform.position.x < 50.0f)) {
			transform.position += new Vector3 (Time.deltaTime * Random.Range (1, 5), 0, 0);
		}

        //Time to leave
        timeToLive -= Time.deltaTime;
        if (timeToLive < 0)
        {
            if (m_isRaining)
            {
                suppressRain();
                m_isRaining = false;
            }
            Color c = renderer.material.color;
            c.a -= 0.3f * Time.deltaTime;
            renderer.material.color = c;

            if (c.a < 0.1f)
            {
                manager.removeCloud(this);
            }
            return;
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
        float grey = Mathf.Max((100f-m_rainRate)/100f, 0);
        Color c2 = new Color(grey, grey, grey,1.0f);
        //Debug.Log(c2);
		renderer.material.color = c2;
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
