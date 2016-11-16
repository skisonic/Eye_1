﻿using UnityEngine;
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
    int aim_interval;
    const int AIM_INT = 10;

    void Start()
    {
        _gazeAware = GetComponent<GazeAware>();
        aim_interval = AIM_INT;
    }

    // Update is called once per frame
    void Update () {

        if (_gazeAware.HasGazeFocus)
        {
            aim_interval--;
            gazePoint = EyeTracking.GetGazePoint();
            gazePoint3 = new Vector3(gazePoint.Viewport.x, gazePoint.Viewport.y, 0);

            if (aim_interval == 0)
            {
                gameObject.transform.Rotate(Vector3.forward, Vector3.Angle(gameObject.transform.position, gazePoint3));

                Vector3 targetDir = gameObject.transform.position - gazePoint3;
                float angle = Vector3.Angle(targetDir, transform.forward);

                Debug.Log("game object posisiton " + gameObject.transform.position + "gazepoint position " + gazePoint3 + Vector3.Angle(gameObject.transform.position, gazePoint3));
                Debug.Log("Corrected? " + angle);
                aim_interval = AIM_INT;
            }
            //Debug.Log("found it");
        }
    }

    void FixedUpdate()
    {
        Vector3 fwd = transform.TransformDirection(Vector3.forward);
        Vector3 targetDir = gazePoint3 - gameObject.transform.position;
        RaycastHit hit;

        Physics.Raycast(transform.position, targetDir, out hit, 10);
        Debug.DrawLine(transform.position, targetDir);
        if (Physics.Raycast(transform.position, targetDir, out hit, 10))
        {
            print("There is something in front of the object!");
            Debug.Log("roenoen " + hit.transform.name);
        }
    }
}
