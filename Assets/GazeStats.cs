using UnityEngine;
using System.Collections;
using Tobii.EyeTracking;


public class GazeStats : MonoBehaviour {

    private GazeAware _gazeAware;

    GazePoint gazePoint;

    public bool hasGaze;

    void Start()
    {
        _gazeAware = GetComponent<GazeAware>();
    }

    void Update()
    {
        if (_gazeAware.HasGazeFocus)
        {
            gazePoint = EyeTracking.GetGazePoint();
            hasGaze = true;
            //Debug.Log("found it");
        }
    }
}
