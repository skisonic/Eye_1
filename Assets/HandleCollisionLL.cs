using UnityEngine;
using System.Collections;

public class HandleCollisionLL : MonoBehaviour
{

    public bool sphere_coll, current;
    float xBound = 5.0f;
    float yBound = 4.0f;
    public int index;
 
    // Use this for initialization
    void Start () {
        sphere_coll = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider other)
    {
        if (index > other.gameObject.GetComponent<HandleCollisionLL>().index)
        {
            if (other.gameObject.tag == "Sphere_pf")
            {
                sphere_coll = true;
                FindNewPosition();
            }
        }
        else
        {
            //Debug.Log("wtf");
        }
    }

    void FindNewPosition()
    {
        transform.position =  new Vector3(Random.Range(-xBound, xBound), Random.Range(-2.0f, yBound));
        sphere_coll = false;
    }
}
