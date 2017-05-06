using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.EyeTracking;

public class HomeAutoMove : MonoBehaviour {


    private GazeAware _gazeAware;
    public bool hasGaze;
    public float riseSpeed;
    //2.5f bottom, 6.2f top
	// Use this for initialization
	void Start () {
        _gazeAware = GetComponentInChildren<GazeAware>();
        riseSpeed = 0.001f;
    }

    // Update is called once per frame
    void FixedUpdate () {

        if (_gazeAware.HasGazeFocus)
        {
            if (transform.position.y <= -1.6f) //lower while looking
            {
                riseSpeed = 0.001f; //reset rise speed
            }
            if (transform.position.y > -2.5f) //lower while looking
            {
                transform.position = transform.position - new Vector3(0, 0.1f);
                riseSpeed = riseSpeed - 0.001f / 60f;
            }
        }
        else
        {
            if (transform.position.y <= 6.2f) //lower while looking
            {
                riseSpeed = riseSpeed + 0.0015f/60f;
                transform.position = transform.position + new Vector3(0, riseSpeed);
            }
        }
        if(riseSpeed < 0.001f)
        {
            riseSpeed = 0.001f;
        }

    }

    IEnumerator MoveUp()
    {
        yield return null;
    }
}
