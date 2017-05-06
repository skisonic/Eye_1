﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.EyeTracking;


public class Targets_Container_Attn : MonoBehaviour {


    public Sprite[] target_sprites;
    private int type; //0:look 1:click
    float gazeKillTimer; //amoutn of time to gaze at a gazer
    float gazeTolTimer; //timer for gazer without reset
    int clickKillCount; //number of clicks to kill a clicker
    HandleGazeLL gazeHandler;

    const float KILL_GAZE_TIME = 45.0f; //multiply this shit by 60 for now. we're using 3/4s rn
    const int KILL_CLICK_COUNT = 3; //number of mouesd clicks required to kill a mouser
    const float GAZE_TOLERANCE = 0.2f;  //time allowed for gaze to fall off a gazer without reset

    int rand;
    int value;
    public bool counted = false;
    SpriteRenderer rend;

    int mouseInputMode = 1; //0:click, 1:mouseover
    float mouseKillTimer = 10.0f;
    int continuousGazeMode; //if 1 gaze moves off gazer, restart timer (requires continues gaze)
    int continuousMouseMode; //if 1 mouse moves off moueser, restart timer (requires continues mouseover)

    private GM_Attention gm;
    // Use this for initialization
    void Start () {

        gm = GameObject.Find("GameManager").GetComponent<GM_Attention>();
        continuousGazeMode = 1; 
        continuousMouseMode = 1;
    }


    public int ReturnPoints()
    {
        if(type == 0)
        {
            return 3;
        }
        else if (type == 1)
        {
            return 1;
        }
        else
        {
            Debug.Log("Error returning points/type");
            return 0;
        }
    }
	
    public void Init(int type_in)
    {
        GazeAware gaze_aware;
        rend = GetComponentInChildren<SpriteRenderer>();
        gaze_aware = GetComponent<GazeAware>();
        gazeHandler = GetComponent<HandleGazeLL>();
        //Debug.Log("initiating. type =  " + type);

        if (type_in == 0)
        {
            rend.sprite = target_sprites[0]; //0 = red = gaze
            rend.color = new Color(0.5f, 0.5f, 0.5f, 1f);
            gazeKillTimer = KILL_GAZE_TIME;
            value = 3;
        }
        else if (type_in == 1)
        {
            rend.sprite = target_sprites[1]; //1 = orange = click
            rend.color = new Color(0.5f, 0.5f, 0.5f, 1f);
            clickKillCount = KILL_CLICK_COUNT;
            value = 1;
        }
        else
        {
            rend.sprite = target_sprites[1]; //1 = orange = click
            clickKillCount = KILL_CLICK_COUNT;
            value = 1;
            Debug.Log("Error Type_in param: defaulting to 1");
        }
        type = type_in;
    }

	// Update is called once per frame
	void FixedUpdate ()
    {
        if (type == 0) //gazer
        {
            if (gazeHandler.hasGaze)
            {
                clickKillCount--;
                rend.color += new Color(0.15f, 0.15f, 0.15f);

                gazeKillTimer--;
                gazeTolTimer = GAZE_TOLERANCE;
                rend.color += new Color(0.1f, 0.1f, 0.1f);
                //Debug.Log("has gaze from handler. gazeKillTime left = " + gazeKillTimer);
            }
            else
            {
                if (continuousGazeMode == 1)
                {
                    gazeTolTimer -= Time.deltaTime;
                    if (gazeTolTimer > 0) //run down tolerance before reset
                    {
                        //Debug.Log("gaze tol timer = " + gazeTolTimer);
                        gazeKillTimer--;
                        rend.color += new Color(0.0085f, 0.0085f, 0.0085f);
                    }
                    else //reset gazer to full.
                    {
                        rend.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                        gazeKillTimer = KILL_GAZE_TIME;
                    }
                }
            }

            if (gazeKillTimer <= 0)
            {
                gm.UpdateOnTargetDeath(value);
                gameObject.SetActive(false);
                //Destroy(gameObject);
            }
        }
        else if (type == 1) //mouser
        {
            if (mouseInputMode == 0)
            {
                // require 5 clicks to kill
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    // Casts the ray and get the first game object hit
                    //Physics.Raycast(ray, out hit);
                    //Debug.Log("This hit " + hit.collider.gameObject.name + " at " + hit.point);

                    if (Physics.Raycast(ray, out hit))
                    {
                        if (hit.collider.gameObject == gameObject)
                        {
                            clickKillCount--;
                            rend.color += new Color(0.15f, 0.15f, 0.15f);
                        }
                    }
                }

            }
            else if (mouseInputMode == 1)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    RaycastHit[] hits;

                    hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition), 100.0F);
                    for (int i = 0; i < hits.Length; i++)
                    {
                        hit = hits[i];
                        Debug.Log("hit " + hit.collider.gameObject.name);
                    }

                }

                // Casts the ray and get the first game object hit
                //Physics.Raycast(ray, out hit);
                //Debug.Log("This hit " + hit.collider.gameObject.name + " at " + hit.point);

                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject == gameObject)
                    {
                        mouseKillTimer--;

                        if (mouseKillTimer <= 0)
                        {
                            clickKillCount--;
                            mouseKillTimer = 10.0f;
                            rend.color += new Color(0.15f, 0.15f, 0.15f);
                        }
                    }
                }
                else
                {
                    if(continuousMouseMode == 1)
                    {
                        //reset to full life. this is all hacky, should probably clean it up
                        mouseKillTimer = 10.0f;
                        clickKillCount = KILL_CLICK_COUNT;
                        rend.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    }
                }
            }

            if (clickKillCount <= 0)
            {
                gm.UpdateOnTargetDeath(value);
                gameObject.SetActive(false);
                //Destroy(gameObject);
            }
        }
        else
        {
            Debug.Log("Error Type undefined");
        }
    }
}
