using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.EyeTracking;


public class CannonStats : MonoBehaviour {


    public enum gunColor { red, green, blue, yellow };
    public Material[] gunMats;

    public gunColor gc;

    public bool powerOn;
    private bool firstPower = true;
    float activeTimer;

    MeshRenderer mesh;

    public const float MIN_TIMER = 6.0f;
    public const float MAX_TIMER = 15.0f;
    // Use this for initialization
    void Start () {
        mesh = GetComponent<MeshRenderer>();
        //Physics.IgnoreCollision(Follow_Gaze_Stats.GetComponent<Collider>(), GetComponent<Collider>());
        PowerOn();
	}

    // Update is called once per frame
    void Update()
    {
        activeTimer -= Time.deltaTime;
        if (activeTimer <= 0 && powerOn)
        {
            //Debug.Log("depower " + activeTimer);
            PowerOff();
        }

        
        if (GetComponent<GazeAware>().HasGazeFocus)
        {
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Alpha1))
            {
                PowerOn();
            }
        }
    }

    public void PowerOn()
    {
        powerOn = true;
        activeTimer = Random.Range(MIN_TIMER, MAX_TIMER);
        if (firstPower == true)
        {
            activeTimer *= 1.5f;
            firstPower = false;
        }
        mesh.material = gunMats[(int)gc];
        GetComponent<FireCannonOnGaze>().enabled = true;
        GetComponent<AimCannonWithGaze>().enabled = true;
        SwitchColor();
    }

    public void PowerOff()
    {

        powerOn = false;
        mesh.material = gunMats[4];
        GetComponent<FireCannonOnGaze>().enabled = false;
        GetComponent<AimCannonWithGaze>().enabled = false;
    }


    public void SwitchColor()
    {
        mesh = GetComponent<MeshRenderer>();

        switch (gc)
        {
            case gunColor.red:
                mesh.material = gunMats[0];
                break;
            case gunColor.green:
                mesh.material = gunMats[1];
                break;
            case gunColor.blue:
                mesh.material = gunMats[2];
                break;
            case gunColor.yellow:
                mesh.material = gunMats[3];
                break;
            default:
                Debug.Log("error no gun color assigned");
                break;
        }

    }
}
