using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CloudMotion : MonoBehaviour {

	public int m_maxCloud;
	public Transform m_generationPoint;
	public int m_generationWidth;
	public Transform m_suppressionPoint;
	public int m_suppressionWidth;
	public float averageHeight = 50f;
    public float heightRange = 5;
	public float m_timeToWait;

	private List<CloudBehaviour> m_clouds;

	public CloudBehaviour[] m_cloudPrefabs;

	// Use this for initialization
	void Start () {
		m_clouds = new List<CloudBehaviour>();
		StartCoroutine(instanciateClouds());
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		/*for(int i=0; i < m_clouds.Count; ++i){
			GameObject cloud = (GameObject)m_clouds[i];
			if((cloud.transform.position.x > m_suppressionPoint.position.x-m_suppressionWidth/2) && (cloud.transform.position.x < m_suppressionPoint.position.x+m_suppressionWidth/2)){
				Debug.Log ("DESTROY");
				m_clouds.RemoveAt(i);
				Destroy(cloud);
			}
		}*/
	}

    public void removeCloud(CloudBehaviour cloud)
    {
        m_clouds.Remove(cloud);
        Destroy(cloud.gameObject);
    }

	IEnumerator instanciateClouds(){
		while(true){
			float xPos = Random.Range(-10, 10);
            float yPos = Random.Range(-heightRange / 2, heightRange / 2);
			float zPos = Random.Range(-m_generationWidth/2, m_generationWidth/2);
            Vector3 position = new Vector3(xPos, averageHeight+yPos, zPos);
			int randomCloud = Random.Range (1, 3);
			if(m_clouds.Count < m_maxCloud){
				//Debug.Log ("INSTANCIATE");
                CloudBehaviour cloud = CloudBehaviour.Instantiate((CloudBehaviour)m_cloudPrefabs[randomCloud], position, m_generationPoint.rotation) as CloudBehaviour;
				cloud.transform.parent = transform;
				cloud.transform.localScale = new Vector3(1f,1f,1f);
				cloud.transform.Rotate(0,0, 100);
                cloud.manager = this;
				m_clouds.Add(cloud);
			}
			yield return new WaitForSeconds(m_timeToWait);
		}
	}
}
