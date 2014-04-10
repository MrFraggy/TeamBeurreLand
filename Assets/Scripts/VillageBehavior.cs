using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class VillageBehavior : MonoBehaviour {

	/** <summary>
	  * The water required to save the village.
	  * </summary>
	  */
	public int WaterRequired = 10;

	public int rainQuantity = 0;

    public Material roofMaterial;

    List<Transform> getChildren(Transform root, string tag)
    {
        List<Transform> list = new List<Transform>();
        Stack<Transform> stack = new Stack<Transform>();
        stack.Push(root);
        while (stack.Count > 0)
        {
            Transform t2 = stack.Pop();

            foreach (Transform t in t2)
            {
                if (t.tag.StartsWith(tag))
                {
                    list.Add(t);
                }
                stack.Push(t);
            }
        }
        //Debug.Log(list.Count+" trouvés");
        return list;
    }

	// Use this for initialization
	void Start () {
        foreach (Transform t in transform)
        {
            if (t.tag.StartsWith("Tree"))
            {
                t.localScale *= 0;
            }
        }
	}

    void Update()
    {

    }
	void OnParticleCollision (GameObject other) {
		if(!other.transform.tag.StartsWith("RainEntonnoir"))
			return;

		++rainQuantity;
        float percent = Mathf.Lerp(0, 1 * 2f,rainQuantity / (WaterRequired*1.0f));
        //Debug.Log(percent);
        foreach (Transform t in transform)
        {
            if (t.tag.StartsWith("Tree"))
            {
                t.localScale = new Vector3(percent,percent,percent);
            }
        }
		if (rainQuantity >= WaterRequired) {
			Debug.Log ("Village Rescued");
			//gameObject.renderer.material.color = Color.green;
			this.collider.enabled = false;

            List<Transform> roofs = getChildren(transform, "Roof");
            foreach (Transform t in roofs)
            {
                t.renderer.material = roofMaterial;// Color.green;
            }
		}
	}
}
