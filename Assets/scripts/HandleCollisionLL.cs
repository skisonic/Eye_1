using UnityEngine;
using System.Collections;
using System.Linq;

public class HandleCollisionLL : MonoBehaviour
{

    public bool sphere_coll, current;
    public int index;
    float left, right, top, bottom;
    string[] target_tags = { "Mouser", "Gazer", "Sphere_pf" };

    // Use this for initialization
    void Start () {
        sphere_coll = false;
        

        left = GetLimits().Left;
        right = GetLimits().Right;
        top = GetLimits().Top;
        bottom = GetLimits().Bottom;

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider other)
    {
        if (target_tags.Contains(other.gameObject.tag))
        {
            if (index > other.gameObject.GetComponent<HandleCollisionLL>().index)
            {
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
    }

    void FindNewPosition()
    {
        transform.position =  new Vector3(Random.Range(left, right), Random.Range(bottom, top));
        sphere_coll = false;
    }


    private Limits GetLimits()
    {
        Limits val = new Limits();
        Vector3 lowerLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 upperRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        val.Left = lowerLeft.x + 5f;
        val.Right = upperRight.x - 5f;
        val.Top = upperRight.y - 5f;
        val.Bottom = lowerLeft.y + 5f;
        return val;
    }

    public class Limits
    {
        public float Left { get; set; }
        public float Right { get; set; }
        public float Top { get; set; }
        public float Bottom { get; set; }
    }

}
