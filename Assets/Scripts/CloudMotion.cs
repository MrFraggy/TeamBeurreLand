using UnityEngine;
using System.Collections;

public class CloudMotion : MonoBehaviour {

	public int m_maxCloud;
	public Transform m_generationPoint;
	public int m_generationWidth;
	public Transform m_suppressionPoint;
	public int m_suppressionWidth;
	public int m_cloudGenerationHeight;
	public float m_timeToWait;

	private ArrayList m_clouds;

	public GameObject m_cloudPrefab1;
	public GameObject m_cloudPrefab2;
	public GameObject m_cloudPrefab3;
	public ArrayList m_cloudPrefabs;

	// Use this for initialization
	void Start () {
		m_clouds = new ArrayList();
		m_cloudPrefabs = new ArrayList ();
		m_cloudPrefabs.Add (m_cloudPrefab1.gameObject);
		m_cloudPrefabs.Add (m_cloudPrefab2.gameObject);
		m_cloudPrefabs.Add (m_cloudPrefab3.gameObject);
		StartCoroutine(instanciateClouds());
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		for(int i=0; i < m_clouds.Count; ++i){
			GameObject cloud = (GameObject)m_clouds[i];
			if((cloud.transform.position.x > m_suppressionPoint.position.x-m_suppressionWidth/2) && (cloud.transform.position.x < m_suppressionPoint.position.x+m_suppressionWidth/2)){
				Debug.Log ("DESTROY");
				m_clouds.RemoveAt(i);
				Destroy(cloud);
			}
		}
	}

	IEnumerator instanciateClouds(){
		while(true){
			float xPos = Random.Range(m_generationPoint.position.x + 10, m_generationPoint.position.x -10);
			float yPos = Random.Range(m_generationPoint.position.y - m_cloudGenerationHeight/2, m_generationPoint.position.y + m_cloudGenerationHeight/2);
			float zPos = Random.Range(m_generationPoint.position.z + m_generationWidth/2, m_generationPoint.position.z - m_generationWidth/2);
			Vector3 position = new Vector3(xPos, yPos , zPos);
			int randomCloud = Random.Range (1, 3);
			if(m_clouds.Count < m_maxCloud){
				Debug.Log ("INSTANCIATE");
				GameObject cloud = (GameObject)Instantiate((GameObject)m_cloudPrefabs[randomCloud], position, m_generationPoint.rotation);
				cloud.transform.parent = transform;
				cloud.transform.localScale = new Vector3(1f,1f,1f);
				cloud.transform.Rotate(0,0, 100);
				m_clouds.Add(cloud.gameObject);
			}
			yield return new WaitForSeconds(m_timeToWait);
		}
	}
}
