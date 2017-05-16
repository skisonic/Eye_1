using System.Collections;
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

    const float KILL_GAZE_TIME = 0.75f; // we're using 3/4s rn
    const float KILL_MOUSEOVER_TIME = 1.0f; //number of seconds * KILL_CLICK_COUNT = death (hacky. fix.)
    const int KILL_CLICK_COUNT = 3; //number of mouesd clicks required to kill a mouser
    const float GAZE_TOLERANCE = 0.2f;  //time allowed for gaze to fall off a gazer without reset

    int rand;
    int value;
    public bool counted = false;
    SpriteRenderer rend;

    int mouseInputMode = 1; //0:click, 1:mouseover
    float mouseKillTimer = KILL_MOUSEOVER_TIME; // number of seconds * KILL_CLICK_COUNT = death (hacky. fix.)
    int continuousGazeMode; //if 1 gaze moves off gazer, restart timer (requires continues gaze)
    int continuousMouseMode; //if 1 mouse moves off moueser, restart timer (requires continues mouseover)

    private GM_Attention gm;
    private ParticleSystem ps;
    public bool hitMe;
    // Use this for initialization
    void Start () {

        gm = GameObject.Find("GameManager").GetComponent<GM_Attention>();
        ps = gameObject.GetComponentInChildren<ParticleSystem>();
        continuousGazeMode = 1; 
        continuousMouseMode = 1;
        hitMe = false;

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
                var emission = ps.emission;
                emission.rateOverTime = emission.rateOverTime.constant + 18.0f/(1.0f/gazeKillTimer);
                rend.color += new Color(0.11f, 0.11f, 0.11f);
                //Debug.Log("emissionrate = " + emission.rateOverTime.constant);
            }
            else
            {
                if (continuousGazeMode == 1)
                {
                    //reset to full life. this is all hacky, should probably clean it up
                    //it is casting a bunch of rays... pretty inefficient but working.
                    gazeTolTimer -= Time.deltaTime;
                    if (gazeTolTimer > 0) //run down tolerance before reset
                    {
                        //Debug.Log("gaze tol timer = " + gazeTolTimer);
                        gazeKillTimer -= Time.deltaTime;
                        rend.color += new Color(0.11f, 0.11f, 0.11f);
                        //rend.color += new Color(0.0085f, 0.0085f, 0.0085f);
                    }
                    else //reset gazer to full.
                    {
                        ps.Stop();
                        ps.Clear();
                        var emission = ps.emission;
                        emission.rateOverTime = 10.0f;
                        gazeKillTimer = KILL_GAZE_TIME;
                        rend.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    }
                }
            }
            /*****************/

            /*
            if (gazeHandler.hasGaze)
            {
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
            */

            /******/

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
            else if (mouseInputMode == 1) // 1:mouseover moude
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

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

                // Casts the ray and get all objects hit
                //success, to be implemented later along with gazer
                //needs a bool and some sort of overall check or something

                /*
                RaycastHit[] hits;
                bool isHit = false;
                hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition), 30.0F);
                for (int i = 0; i < hits.Length; i++)
                {
                    if (hits[i].collider.gameObject == gameObject)
                    {
                        isHit = true;
                    }
                    else
                    {
                        isHit = false;
                    }
                }
                */

                    //should get overlapping mouser
                    if (hitMe)
                    {
                        ps.Play();
                        mouseKillTimer -= Time.deltaTime;

                        if (mouseKillTimer <= 0)
                        {
                            var emission = ps.emission;
                            emission.rateOverTime = emission.rateOverTime.constant * 2.0f;
                            clickKillCount--;
                            mouseKillTimer = KILL_MOUSEOVER_TIME;
                            rend.color += new Color(0.15f, 0.15f, 0.15f);
                        }
                    }
                    else
                    {
                        if (continuousMouseMode == 1)
                        {
                            //reset to full life. this is all hacky, should probably clean it up
                            //it is casting a bunch of rays... pretty inefficient but working.
                            ps.Stop();
                            ps.Clear();
                            var emission = ps.emission;
                            emission.rateOverTime = 10.0f;
                            mouseKillTimer = KILL_MOUSEOVER_TIME;
                            clickKillCount = KILL_CLICK_COUNT;
                            rend.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                        }
                    }
 

                // Casts the ray and get the first game object hit
                /*
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject == gameObject)
                    {
                        ps.Play();
                        mouseKillTimer -= Time.deltaTime;
                        if (mouseKillTimer <= 0)
                        {
                            var emission = ps.emission;
                            emission.rateOverTime = emission.rateOverTime.constant * 2.0f;
                            clickKillCount--;
                            mouseKillTimer = KILL_MOUSEOVER_TIME;
                            rend.color += new Color(0.15f, 0.15f, 0.15f);
                        }
                    }
                }
                else
                {
                    if(continuousMouseMode == 1)
                    {
                        //reset to full life. this is all hacky, should probably clean it up
                        //it is casting a bunch of rays... pretty inefficient but working.
                        ps.Stop();
                        ps.Clear();
                        var emission = ps.emission;
                        emission.rateOverTime = 10.0f;
                        mouseKillTimer = KILL_MOUSEOVER_TIME;
                        clickKillCount = KILL_CLICK_COUNT;
                        rend.color = new Color(0.5f, 0.5f, 0.5f, 1f);
                    }
                }
                */
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

    void LateUpdate()
    {
        hitMe = false;
    }
}
