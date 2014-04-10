using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CloudMotion : MonoBehaviour {

	//public int m_maxCloud;
	public int m_generationArea;
	public float averageHeight = 50f;
    public float heightRange = 5;
	public float m_timeToWait;


	private List<CloudBehaviour> m_clouds;

	public CloudBehaviour[] m_cloudPrefabs;
	public Transform[] m_spawnPoints;

	// Use this for initialization
	void Start () {
		m_clouds = new List<CloudBehaviour>();
		StartCoroutine(instanciateClouds());
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	}

    public void removeCloud(CloudBehaviour cloud)
    {
        m_clouds.Remove(cloud);
        Destroy(cloud.gameObject);
    }

	IEnumerator instanciateClouds(){
		while(true){
			int randomCloud = Random.Range (1, 3);
			int randomSpawn = Random.Range(1, m_spawnPoints.Length);
			float randomXPosIntoSpawn = Random.Range (m_spawnPoints[randomSpawn].position.x -m_generationArea/2, m_spawnPoints[randomSpawn].position.x + m_generationArea/2);
			float randomZPosIntoSpawn = Random.Range (m_spawnPoints[randomSpawn].position.z -m_generationArea/2, m_spawnPoints[randomSpawn].position.z + m_generationArea/2);
			if(m_clouds.Count < m_spawnPoints.Length){
				CloudBehaviour cloud = CloudBehaviour.Instantiate((CloudBehaviour)m_cloudPrefabs[randomCloud], 
				                                                  new Vector3(randomXPosIntoSpawn,m_spawnPoints[randomSpawn].position.y, randomXPosIntoSpawn)
				                                                  , m_spawnPoints[randomSpawn].rotation) 
																	as CloudBehaviour;
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
