using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.EyeTracking;


public class Follow_Gaze_Stats : MonoBehaviour {

    public float lifetime;
    SpriteRenderer rend;
    SpriteRenderer[] rends;

    public enum gazeColor { red, green, blue, yellow, none };
    public gazeColor gaze_color;
    Color redCannonCol, greenCannonCol, yellowCannonCol, blueCannonCol;

    //public SpriteRenderer[] targetSprites; 

    // Use this for initialization
    void Start () {
        gaze_color = gazeColor.none;
        rends = GetComponentsInChildren<SpriteRenderer>();
        //Debug.Log("rends size of = " + rends.GetLength(0));

        rend = GetComponent<SpriteRenderer>();

        ColorUtility.TryParseHtmlString("#FD2768FF", out redCannonCol);
        ColorUtility.TryParseHtmlString("#71F67AFF", out greenCannonCol);
        ColorUtility.TryParseHtmlString("#F2ED56FF", out yellowCannonCol);
        ColorUtility.TryParseHtmlString("#1396C0FF", out blueCannonCol);
    }

    void Update () {
        Vector3 projectThis;
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
                GazeTracking gazeTracking = EyeTracking.GetGazeTrackingStatus();
                gazePoint = EyeTracking.GetGazePoint();
                projectThis = new Vector3(gazePoint.Screen.x, gazePoint.Screen.y, -Camera.main.transform.position.z);
                transform.position = Camera.main.ScreenToWorldPoint(projectThis);



                //countdown color change back to original
                if (lifetime >= 0)
                {
                    lifetime -= Time.deltaTime;
                }
                else
                {
                    if (gaze_color != gazeColor.none)
                    {
                        SwitchColorWhite();
                    }
                    //rend.color = Color.white;
                    //gaze_color = gazeColor.none;
                    // change that sonbitch to grey. 
                }
            }
        }
    }


    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("ColorChanger"))
        {
            Debug.Log("1 triggered a color changer");
            //this is the working one
            CannonStats cs = coll.gameObject.GetComponent<CannonStats>();
            SwitchColor(cs);
        }
    }

    void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("ColorChanger"))
        {
            Debug.Log("2 triggered a color changer");
            CannonStats cs = coll.gameObject.GetComponentInParent<CannonStats>();
            SwitchColor(cs);
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("ColorChanger"))
        {
            Debug.Log("3 triggered a color changer");
            CannonStats cs = coll.gameObject.GetComponentInParent<CannonStats>();
            SwitchColor(cs);
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("ColorChanger"))
        {
            Debug.Log("4 triggered a color changer");
            CannonStats cs = coll.gameObject.GetComponentInParent<CannonStats>();
            SwitchColor(cs);
        }
    }

    public void SwitchColorWhite() //change target color 
    {
        rends[1].color = Color.white;
        rends[1].color = Color.white;
        gaze_color = gazeColor.none;
        //Debug.Log("Switch Color: none");
    }

    public void SwitchColor(CannonStats cs) //change target color 
    {
        switch (cs.gc)
        {
            case CannonStats.gunColor.red:
                rend.color = Color.red;
                rends[1].color = redCannonCol;
                gaze_color = gazeColor.red;
                //Debug.Log("Switch Color: red");
                break;
            case CannonStats.gunColor.green:
                rend.color = Color.green;
                rends[1].color = greenCannonCol;
                gaze_color = gazeColor.green;
                //Debug.Log("Switch Color: green");
                break;
            case CannonStats.gunColor.blue:
                rend.color = Color.blue;
                rends[1].color = blueCannonCol;
                gaze_color = gazeColor.blue;
                //Debug.Log("Switch Color: blue");
                break;
            case CannonStats.gunColor.yellow:
                rend.color = Color.yellow;
                rends[1].color = yellowCannonCol;
                gaze_color = gazeColor.yellow;
                //Debug.Log("Switch Color: yellow");
                break;
            default:
                Debug.Log("error no gun color assigned");
                break;
        }
        lifetime = 7.0f;

    }
}
