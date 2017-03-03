using UnityEngine;
using System.Collections;
using Tobii.EyeTracking;


public class HandleGazeLL : MonoBehaviour {

    private GazeAware _gazeAware;
    GameObject sphere;
    GazePoint gazePoint;
    public bool hasGaze;

    // Use this for initialization
    void Start () {
        _gazeAware = GetComponent<GazeAware>();
        hasGaze = false;

    }

    // Update is called once per frame
    void Update () {

        if (_gazeAware.HasGazeFocus){
            hasGaze = true;
            Debug.Log("do have it");
            // report gazing
        }
        else
        {
            hasGaze = false;
        }
    }
}
