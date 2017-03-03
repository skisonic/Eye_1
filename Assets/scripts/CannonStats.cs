using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.EyeTracking;


public class CannonStats : MonoBehaviour {


    public enum gunColor { red, green, blue, yellow };
    public Material[] gunMats;

    public gunColor gc;

    float lifetime;
    float startTime, activeTimer;
    public bool powerOn;

    MeshRenderer mesh;

    // Use this for initialization
    void Start () {
        lifetime = 20.0f;
        powerOn = true;
        mesh = GetComponent<MeshRenderer>();

        activeTimer = Random.Range(4.0f, 8.0f);
        startTime = Time.time;
        SwitchColor();
	}

    // Update is called once per frame
    void Update()
    {
        activeTimer -= Time.deltaTime;
        if (activeTimer <= lifetime && powerOn)
        {
            Debug.Log("depower " + activeTimer);
            //PowerOff();
        }

        /*
        if (GetComponent<GazeAware>().HasGazeFocus)
        {
            if(Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Alpha1))
            {
                PowerOn();
            }
        }
        */
    }

    public void PowerOn()
    {
        powerOn = true;
        activeTimer = Random.Range(5.0f, 9.0f);
        mesh.material = gunMats[(int)gc];
        GetComponent<FireCannonOnGaze>().enabled = true;
        GetComponent<AimCannonWithGaze>().enabled = true;
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
