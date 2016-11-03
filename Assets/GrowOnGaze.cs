using UnityEngine;
using System.Collections;
using Tobii.EyeTracking;


/// <summary>
/// Gaze aware component grows on gaze.  duh
/// -ski 
/// </summary>
[RequireComponent(typeof(GazeAware))]
public class GrowOnGaze : MonoBehaviour
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

            transform.localScale *= 1.05f;
        }
        else
        {
            if(transform.localScale.x > 1.0f || Input.GetKey(KeyCode.O))
            {
                transform.localScale *= .96f;
                Debug.Log("magnitude: " + transform.localScale);
            }
            else
            {
                transform.localScale = Vector3.one;
            }
        }
    }
}