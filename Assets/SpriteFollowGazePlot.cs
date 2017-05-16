﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.EyeTracking;

public class SpriteFollowGazePlot : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() //wont work with Fixed, i gueess somethin w/gazeplotter
    {
        Vector3 projectThis;
        DeviceStatus deviceStatus;
        GazePoint gazePoint;

        deviceStatus = EyeTrackingHost.GetInstance().EyeTrackingDeviceStatus;
        gazePoint = EyeTracking.GetGazePoint();

        if (deviceStatus != DeviceStatus.Tracking)
        {
            deviceStatus = EyeTrackingHost.GetInstance().EyeTrackingDeviceStatus;
        }
        else
        {

            GazeTracking gazeTracking = EyeTracking.GetGazeTrackingStatus();
            UserPresence userPresence = EyeTracking.GetUserPresence();

            if (userPresence.IsUserPresent)
            {
                gazePoint = EyeTracking.GetGazePoint();
                projectThis = new Vector3(gazePoint.Screen.x, gazePoint.Screen.y, -Camera.main.transform.position.z);
                transform.position = Camera.main.ScreenToWorldPoint(projectThis);
            }
        }
    }
}
