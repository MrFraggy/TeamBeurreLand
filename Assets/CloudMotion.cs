using UnityEngine;
using System.Collections;

public class CloudMotion : MonoBehaviour {

	public int m_maxCloud;

	private ArrayList m_clouds;

	public GameObject m_cloudPrefab;

	// Use this for initialization
	void Start () {
		m_clouds = new ArrayList();
	}
	
	// Update is called once per frame
	void Update () {

		float xPos = Random.Range(30.0f, -30.0f);
		float zPos = Random.Range(35.0f, 45.0f);
		Vector3 position = new Vector3(xPos, 20.0f, zPos);

		if(m_clouds.Count < m_maxCloud){
			GameObject cloud = (GameObject)Instantiate(m_cloudPrefab, position, Quaternion.identity);
			cloud.transform.parent = transform;
			cloud.transform.localScale = new Vector3(1f,1f,1f);
			m_clouds.Add(cloud);
		}
	}
}
