using UnityEngine;
using System.Collections;
using Tobii.EyeTracking;



public class MoveWithGaze : MonoBehaviour {


    private Vector3 mousePosition;
    public float moveSpeed = 0.1f;


    GazePoint gazePoint;

    // Use this for initialization
    void Start () {
        gazePoint = EyeTracking.GetGazePoint();
    }

    // Update is called once per frame
    void Update () {
        if (gazePoint.IsValid) // real Tobii version
        {
            //Debug.Log("unfdefined gazepoint : " + gazePoint);
            /*
            if (transform.position.x > gazePoint.Screen.x)
            {
                transform.position += Vector3.right;
                //Debug.Log("found it");
            }
            else if (transform.position.x < gazePoint.Screen.x)
            {
                transform.position -= new Vector3 (.25f,0,0);
                //Debug.Log("found it");
            }*/
        }
        else
        {

            //transform.Rotate(Input.mousePosition);
            if (Input.GetMouseButton(0))
            {
                mousePosition = Input.mousePosition;
                //mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                //transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
                transform.localPosition = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, 10), mousePosition, moveSpeed);
                Debug.Log("mousePosition = " + mousePosition);
            }
            //if (Input.GetMouseButton(1) && transform.rotation.z != Vector3.Angle(transform.position, (Input.mousePosition)))
            if (Input.GetMouseButton(1))
            {
                mousePosition = Input.mousePosition;
                mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
                //transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);
                float myTemp;
                myTemp = Vector3.Angle(transform.position, (Input.mousePosition));
                transform.Rotate(Vector3.forward, myTemp);
                Debug.Log("mousePosition = " + mousePosition);
            }

            
            Ray ray = new Ray(Input.mousePosition, transform.position);
            RaycastHit hit;
            //Debug.Log("points = " + points);
            //Debug.Log("moues = " + Input.mousePosition);
            Physics.Raycast(ray, out hit);
            //if (hit.collider.gameObject.name == "SphereR" ) //simulate gaze with raycast
            {
                //Debug.Log("eyy");
            }

            /*
            if (hit.collider.gameObject.name == "SphereR" || spheres[2].GetComponent<HandleGazeLL>().hasGaze) //simulate gaze with raycast
            {
                points++;
                score += points;
                if (points == 1) score += 5; // extra points for actually clicking
                hit.collider.gameObject.GetComponent<Renderer>().material = materials[0];
                spheres[2].GetComponent<Renderer>().material.color += new Color(points / 10.0f, 0, 0);
                Debug.Log("nailed sphere R");
            }*/
        }
        gazePoint = EyeTracking.GetGazePoint();

    }
}
