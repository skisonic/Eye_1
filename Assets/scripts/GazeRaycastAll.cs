using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.EyeTracking;


public class GazeRaycastAll : MonoBehaviour {

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit[] hits;
        DeviceStatus deviceStatus;

        deviceStatus = EyeTrackingHost.GetInstance().EyeTrackingDeviceStatus;

        if (deviceStatus != DeviceStatus.Tracking)
        {
            deviceStatus = EyeTrackingHost.GetInstance().EyeTrackingDeviceStatus;
        }
        else
        {
            UserPresence userPresence = EyeTracking.GetUserPresence();
            if (userPresence.IsUserPresent)
            {
                GazePoint gazePoint;
                gazePoint = EyeTracking.GetGazePoint();
                Vector3 gazePos = new Vector3(gazePoint.Screen.x, gazePoint.Screen.y, -Camera.main.transform.position.z);
                hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(gazePos), 30.0F);
                for (int i = 0; i < hits.Length; i++)
                {
                    RaycastHit hit = hits[i];
                    Renderer rend = hit.transform.GetComponent<Renderer>();

                    if (hit.collider.gameObject.tag == "Gazer")
                    {
                        hit.collider.gameObject.GetComponent<Targets_Container_Attn>().hitMe = true;
                    }
                }
            }
        }
    }
}
