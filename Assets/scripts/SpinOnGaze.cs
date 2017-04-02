using UnityEngine;
using Tobii.EyeTracking;


/// <summary>
/// Spins transform component of object with gaze awareness, on gaze.  duh
/// -ski 
/// </summary>
[RequireComponent(typeof(GazeAware))]
public class SpinOnGaze : MonoBehaviour
{
    private GazeAware _gazeAware;


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
            transform.Rotate(Vector3.forward);
            //Debug.Log("found it");
        }
    }
}