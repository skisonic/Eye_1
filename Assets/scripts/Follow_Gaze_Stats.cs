using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.EyeTracking;


public class Follow_Gaze_Stats : MonoBehaviour {

    public float lifetime;
    SpriteRenderer rend;

    public enum gazeColor { red, green, blue, yellow, none };
    public gazeColor gaze_color;
    GazePoint gazePoint;
    //public SpriteRenderer[] targetSprites; 

    // Use this for initialization
    void Start () {
        gaze_color = gazeColor.none;
        gazePoint = EyeTracking.GetGazePoint();
    }

    void Update () {
        Vector3 projectThis;
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
            rend = GetComponent<SpriteRenderer>();
            rend.color = Color.white;
            gaze_color = gazeColor.none;
            // change that sonbitch to grey. 
        }
    }


    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "ColorChanger")
        {
            ////Debug.Log("triggered a color changer");
            CannonStats cs = coll.gameObject.GetComponentInParent<CannonStats>();
            SwitchColor(cs);
        }
    }

    public void SwitchColor(CannonStats cs) //change target color 
    {
        rend = GetComponent<SpriteRenderer>();

        switch (cs.gc)
        {
            case CannonStats.gunColor.red:
                rend.color = Color.red;
                gaze_color = gazeColor.red;
                //Debug.Log("Switch Color: red");
                break;
            case CannonStats.gunColor.green:
                rend.color = Color.green;
                gaze_color = gazeColor.green;
                //Debug.Log("Switch Color: green");
                break;
            case CannonStats.gunColor.blue:
                rend.color = Color.blue;
                gaze_color = gazeColor.blue;
                //Debug.Log("Switch Color: blue");
                break;
            case CannonStats.gunColor.yellow:
                rend.color = Color.yellow;
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
