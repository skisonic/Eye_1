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

    const int KILL_CLICK_COUNT = 3; //number of mouesd clicks required to kill a mouser
    const float KILL_GAZE_TIME = 0.75f; // we're using 3/4s rn
    const float KILL_MOUSEOVER_TIME = 1.0f; //number of seconds * KILL_CLICK_COUNT = death (hacky. fix.)
    const float GAZE_TOLERANCE = 0.2f;  //time allowed for gaze to fall off a gazer without reset
    const float DEFAULT_PART_EMIT_RATE = 10.0f;  //default
    const float DEFAULT_PART_SPEED_MULT = 1.0f;  //default
    float particle_speed_step = 0.001f;

    int value;
    SpriteRenderer rend;

    int mouseInputMode; //0:click, 1:mouseover
    int continuousGazeMode; //if 1 gaze moves off gazer, restart timer (requires continues gaze)
    int continuousMouseMode; //if 1 mouse moves off moueser, restart timer (requires continues mouseover)
    float mouseKillTimer; // time in seconds required for mouseover to kill mouser

    private GM_Attention gm;
    private ParticleSystem ps;
    public bool counted = false;
    public bool hitMe;
    // Use this for initialization
    void Start () {

        gm = GameObject.Find("GameManager").GetComponent<GM_Attention>();
        ps = gameObject.GetComponentInChildren<ParticleSystem>();

        continuousGazeMode = 1; 
        continuousMouseMode = 1;

        mouseInputMode = 1;
        mouseKillTimer = KILL_MOUSEOVER_TIME;
        hitMe = false;
    }

    public void Init(int type_in)
    {
        rend = GetComponentInChildren<SpriteRenderer>();
        //Debug.Log("initiating. type =  " + type);

        if (type_in == 0)
        {
            gameObject.tag = "Gazer";
            rend.sprite = target_sprites[0]; //0 = red = gaze
            rend.color = new Color(0.5f, 0.5f, 0.5f, 1f);
            gazeKillTimer = KILL_GAZE_TIME;
            value = 3;
        }
        else if (type_in == 1)
        {
            gameObject.tag = "Mouser";
            rend.sprite = target_sprites[1]; //1 = yellow = click
            rend.color = new Color(0.5f, 0.5f, 0.5f, 1f);
            clickKillCount = KILL_CLICK_COUNT;
            value = 1;
        }
        else
        {
            gameObject.tag = "Mouser";
            rend.sprite = target_sprites[1]; //1 = orange = click
            clickKillCount = KILL_CLICK_COUNT;
            value = 1;
            Debug.Log("Error Type_in param: defaulting to 1");
        }
        type = type_in;
    }

	// Update is called once per frame
	void Update ()
    {
        if (type == 0) //gazer
        {
            if (hitMe)
            {
                gazeKillTimer -= Time.deltaTime;
                gazeTolTimer = GAZE_TOLERANCE;

                ps.Play();
                var main = ps.main;
                main.startSpeedMultiplier += particle_speed_step;
                var emission = ps.emission;
                //emission.rateOverTime = emission.rateOverTime.constant + 18.0f/(1.0f/gazeKillTimer);
                emission.rateOverTime = emission.rateOverTime.constant * 1.2f;
                rend.color += new Color(0.011f, 0.011f, 0.011f);
                //Debug.Log("emissionrate = " + emission.rateOverTime.constant);
            }
            else
            {
                if (continuousGazeMode == 1)
                {
                    //reset to full life. this is a lil hacky, should probably clean it up
                    gazeTolTimer -= Time.deltaTime;
                    if (gazeTolTimer > 0) //run down tolerance before reset
                    {
                        //Debug.Log("gaze tol timer = " + gazeTolTimer);
                        gazeKillTimer -= Time.deltaTime;
                        rend.color += new Color(0.011f, 0.011f, 0.011f);
                        //rend.color += new Color(0.0085f, 0.0085f, 0.0085f);
                    }
                    else //reset gazer to full.
                    {
                        ps.Stop();
                        ps.Clear();
                        var main = ps.main;
                        main.startSpeedMultiplier = DEFAULT_PART_SPEED_MULT;
                        var emission = ps.emission;
                        emission.rateOverTime = DEFAULT_PART_EMIT_RATE;
                        gazeKillTimer = KILL_GAZE_TIME;
                        rend.color = new Color(0.5f, 0.5f, 0.5f, 1f);
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
            if (mouseInputMode == 0) //not modified for overlaps
            {
                // require KILL_CLICK_COUNT clicks to kill
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    //Casts the ray and get the first game object hit
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
            else if (mouseInputMode == 1) // 1:mouseover moude
            {
                /*
                if (Input.GetKeyDown(KeyCode.Mouse0)) //clicking (deprecated)
                {
                    Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit);
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
                */

                //set by external raycastall
                if (hitMe)
                {
                    mouseKillTimer -= Time.deltaTime;

                    ps.Play();
                    var main = ps.main;
                    main.startSpeedMultiplier += particle_speed_step;
                    var emission = ps.emission;
                    emission.rateOverTime = emission.rateOverTime.constant * 1.2f;
                    //emission.rateOverTime = emission.rateOverTime.constant + (200.0f * Time.deltaTime);
                    //Debug.Log("emission.rateOverTime = " + emission.rateOverTime.constant);
                    rend.color += new Color(0.009f, 0.009f, 0.009f);
                }
                else
                {
                    if (continuousMouseMode == 1)
                    {
                        //reset to full life. this is a lil hacky, should probably clean it up
                        ps.Stop();
                        ps.Clear();
                        var main = ps.main;
                        main.startSpeedMultiplier = DEFAULT_PART_SPEED_MULT;
                        var emission = ps.emission;
                        emission.rateOverTime = DEFAULT_PART_EMIT_RATE;
                        mouseKillTimer = KILL_MOUSEOVER_TIME;
                        rend.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    }
                }
            }

            if (clickKillCount <= 0 || mouseKillTimer <= 0)
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

    void LateUpdate()
    {
        hitMe = false; //glory. halleujah
    }


    public int ReturnPoints()
    {
        if (type == 0)
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
}
