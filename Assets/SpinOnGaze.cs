using UnityEngine;
using Tobii.EyeTracking;

[RequireComponent(typeof(GazeAware))]
public class SpinOnGaze : MonoBehaviour
{
    private GazeAware _gazeAware;

    GazePoint gazePoint;


    void Start()
    {
        _gazeAware = GetComponent<GazeAware>();
    }

    void Update()
    {
        if (_gazeAware.HasGazeFocus)
        {
            gazePoint = EyeTracking.GetGazePoint();
            transform.Rotate(Vector3.forward);
            //Debug.Log("found it");
        }
    }
}