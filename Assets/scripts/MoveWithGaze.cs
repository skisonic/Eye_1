using UnityEngine;
using System.Collections;
using Tobii.EyeTracking;



public class MoveWithGaze : MonoBehaviour {


    GazePoint gazePoint;

    // Use this for initialization
    void Start () {
        gazePoint = EyeTracking.GetGazePoint();
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (transform.position.x > gazePoint.Screen.x)
        {
            transform.position += Vector3.right;
            //Debug.Log("found it");
        }
        else if (transform.position.x < gazePoint.Screen.x)
        {
            transform.position -= new Vector3 (.25f,0,0);
            //Debug.Log("found it");
        }
        gazePoint = EyeTracking.GetGazePoint();
    }
}
