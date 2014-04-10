using UnityEngine;
using System.Collections;

public class CloudBehaviour : MonoBehaviour {

	//public float m_rainRate = 0; // 0 : no rain, cloud white / 1 : about to rain, cloud black
	public GameObject m_rain;
	//private bool m_isRaining = false;
    public float timeToLive = 30f;
    public CloudMotion manager;
    public float speed = 1f;
    public AudioSource rainSound;

	// Use this for initialization
	void Start () {
        spawnRain();
        rainSound.Stop();
        rainSound.Play();
        
	}
	
	// Update is called once per frame
	void Update () {
        rainSound.transform.position = transform.position;
        Vector3 dir = new Vector3 (1.0f, 0, 0) * speed * Time.deltaTime;
        Debug.DrawRay(transform.position, new Vector3(-5, -transform.position.y, 0), Color.red);
		//MOTION
//		if ((transform.position.x < 0.0f && transform.position.x > -50.0f) || (transform.position.x > 0.0f && transform.position.x < 50.0f)) {
//			transform.position += dir;
//		}


        

        //Time to leave
        timeToLive -= Time.deltaTime;
//        if (timeToLive < 0)
//        {
//            //if (m_isRaining)
//            //{
//            rainSound.Stop();
//                suppressRain();
//                
//            //}
//            Color c = renderer.material.color;
//            c.a -= 0.3f * Time.deltaTime;
//            renderer.material.color = c;
//
//            if (c.a < 0.1f)
//            {
//                if(manager != null)
//                    manager.removeCloud(this);
//            }
//            return;
//        }

        // Sound
        /*RaycastHit hitInfo;
        if (Physics.Raycast(transform.position, new Vector3(-5, -transform.position.y, 0), out hitInfo, 1000f))
        {
            audio.transform.position = hitInfo.point;
            if (!audio.isPlaying)
                audio.Play();
        }*/

		//RAIN
		/*if(m_isRaining){
			m_rainRate = m_rainRate - Time.deltaTime*10;

			if (m_rainRate <= 0.0f){
				// Suppress rain
				suppressRain();
				m_rainRate = Random.Range(0, 100);
			}
		} else {

			m_rainRate = m_rainRate + Time.deltaTime*10;
			if (m_rainRate >= 100.0f){
				// Create rain
				spawnRain();
				m_rainRate = Random.Range(0, 100);
			}
		}
        float grey = Mathf.Max((100f-m_rainRate)/100f, 0);
        Color c2 = new Color(grey, grey, grey,1.0f);
        //Debug.Log(c2);
		renderer.material.color = c2;*/
	}

	//Instantiate the rain
	void spawnRain()
	{
		/*GameObject rain = (GameObject)Instantiate(m_rainPrefab, transform.position, Quaternion.identity);
		rain.transform.parent = transform;
		rain.transform.Rotate (0,90,0);
		rain.transform.Translate (0, -5, 0);*/
        m_rain.particleSystem.Play();
        //m_isRaining = true;
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
		/*foreach(Transform child in this.transform){
			if(child.tag.StartsWith("Rain")){
				Destroy(child.gameObject);
			}
		}*/
        m_rain.particleSystem.Stop();
        //m_isRaining = false;
	}
}
