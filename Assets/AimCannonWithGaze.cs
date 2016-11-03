using UnityEngine;
using System.Collections;
using Tobii.EyeTracking;


/// <summary>
/// look at the camera to activate it.
/// times out after a certain period of time
/// 1 fire key?
/// lets run that shit.
/// USE THIS FOR CANNONS_2
/// </summary>

[RequireComponent(typeof(GazeAware))]
public class AimCannonWithGaze : MonoBehaviour {


    private GazeAware _gazeAware;
    public GameObject bullet_pf;
    GameObject cannon;
    GameObject sphereL, sphereR;
    GazePoint gazePoint;
    Vector3 gazePoint3;

    GameObject bullet;

    void Start()
    {
        _gazeAware = GetComponent<GazeAware>();
    }

    // Update is called once per frame
    void Update () {
        if (_gazeAware.HasGazeFocus)
        {
            gazePoint = EyeTracking.GetGazePoint();
            gazePoint3 = new Vector3(gazePoint.Viewport.x, gazePoint.Viewport.y, 0);

            gameObject.transform.Rotate(Vector3.forward, Vector3.Angle(gameObject.transform.position, gazePoint3));
            //Debug.Log("found it");
        }
    }
}
