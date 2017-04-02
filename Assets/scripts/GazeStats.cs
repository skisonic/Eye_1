using UnityEngine;
using System.Collections;
using Tobii.EyeTracking;


public class GazeStats : MonoBehaviour {

    private GazeAware _gazeAware;

    public bool hasGaze;

    void Start()
    {
        _gazeAware = GetComponent<GazeAware>();
    }

    void Update()
    {
        if (_gazeAware.HasGazeFocus)
        {

            GazePoint gazePoint;

            gazePoint = EyeTracking.GetGazePoint();
            hasGaze = true;
            //Debug.Log("found it");
        }
    }
}
