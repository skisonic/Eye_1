﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;


/// <summary>
/// Randomly make any number of 3 lights visible. Gaze spheres to light them and get points.
/// permutattions on difficulty include:
/// multiple targets 
/// -- different point values vs different visibilities. 
/// cursor control vs gaze - use cursor to score "blind" while a longer gaze = more points
/// 
/// 
///  create 2 types of targets - those to look at and those to click for points
/// 
/// </summary>

public class GM_Attention : MonoBehaviour {


    private float timer, seconds, startTime;
    int sphereRand, sphereRand2;
    float randInterval, randChance, randDuration;
    public Material[] materials;


    GameObject sphereL, sphereM, sphereR;
    protected GameObject sphere_pf, target_pf;
    private GameObject[] spheresCntnr = new GameObject[maxTargets];


    public GameObject[] spheres;
    bool isTargetOn;
    float points, score;
    int numTargets, numGazers, numClickers = 0;
    int totalTargets = 5;
    
    const int maxTargets = 7;
    float pointGrowthRateGaze = 1.05f;
    bool gameRunning = false;

    Text pointText, leftText, timerText, scoreText;

    float left, right, top, bottom;
    const int MAX_GAZERS = 3;
    const int ROUND_TIME = 30;


    // Use this for initialization

    void Start()
    {        
        Application.targetFrameRate = 30;

        timer = ROUND_TIME; //seconds before game over occurs
        startTime = Time.time;
        seconds = 0; //actual timer

        randInterval = Random.Range(0.3f, 0.6f);
        randChance = Random.Range(0, 0.3f);
        randDuration = Random.Range(1.3f, 2.0f);

        score = 0;
        points = 0;

        target_pf = Resources.Load<GameObject>("prefabs/Attn_Target");
        sphere_pf = Resources.Load<GameObject>("prefabs/Sphere_pf");
        leftText = GameObject.Find("Text").GetComponent<Text>();
        pointText = GameObject.Find("Text (1)").GetComponent<Text>();
        timerText = GameObject.Find("Timer").GetComponent<Text>();
        scoreText = GameObject.Find("Score").GetComponent<Text>();

        sphereRand = Random.Range(0, 2);
        sphereRand2 = Random.Range(0, 2);


        getBoundaries();
        CreateSpheres();
        PlaceSpheres();
        gameRunning = true;
    }

    void getBoundaries()
    {
        left = GetLimits().Left;
        right = GetLimits().Right;
        top = GetLimits().Top;
        bottom = GetLimits().Bottom;
    }

    void myInit()
    {
        //bookingkeeping initation function
        //ideally removed eventually.


        spheres[0].GetComponent<MeshRenderer>().enabled = false;
        spheres[1].GetComponent<MeshRenderer>().enabled = false;
        spheres[2].GetComponent<MeshRenderer>().enabled = false;
    }

    void CreateSpheres()
    {
        int rand, type;
        int minGazers;

        numTargets = 0;
        minGazers = totalTargets / 3;


        for (int i = 0; i < maxTargets; i++)
        {

            spheresCntnr[i] = ((GameObject)Instantiate(target_pf, new Vector3(Random.Range(left, right),
                                                                               Random.Range(bottom, top)), Quaternion.identity));

            spheresCntnr[i].GetComponent<HandleCollisionLL>().current = true;
            spheresCntnr[i].GetComponent<HandleCollisionLL>().index = i + 1;
            numTargets++;


            rand = Random.Range(0, 2);

            if (minGazers <= 0)
            {

                //ensure that minimum number of gazers are up.
                type = 0;
                numGazers++;
                minGazers--;
            }
            else if (rand == 0)
            {
                type = 0;
                numGazers++;
                //Debug.Log("numtargets = " + numTargets + " num gazers count " + numGazers + "type = " + type);
            }
            else if (rand == 1)
            {
                type = 1;
                numClickers++;
                //Debug.Log("numtargets = " + numTargets + " num clickers count " + numClickers + "type = " + type);
            }
            else
            {
                type = 1;
                numClickers++;
                Debug.Log("numtargets = " + numTargets + " num clickers count " + numClickers + "type = " + type);
                Debug.Log("Error Assigning Type: defaulting to 1");
            }


            if (numGazers >= MAX_GAZERS) //limit total gazers
            {
                type = 1; 
                numGazers--;
            }
            spheresCntnr[i].GetComponent<Targets_Container_Attn>().Init(type);


            if (spheresCntnr[i].GetComponent<HandleCollisionLL>().sphere_coll == true)
            {
                Debug.Log("bonk");
                //i--;
            }
            else
            {
                //Debug.Log("bink");
                //spheresCntnr[i].GetComponent<Rigidbody>().isKinematic = true;
            }

            // need to move them if they overlap
            //spheresCntnr[i] = Instantiate<GameObject>(sphere_pf);
            spheresCntnr[i].GetComponent<HandleCollisionLL>().current = false;
            //instantiate target
        }

    }

    void PlaceSpheres() //this function moves the spheres (for some reason) but it used to ensure no overlaps
    {

        getBoundaries();

        for (int i = 0; i < maxTargets; i++) //this bit of code generates a spehere and checks that it doesnt collide with the last sphere created.
        {

            spheresCntnr[i].transform.position = new Vector3(Random.Range(left, right), Random.Range(bottom, top));

            spheresCntnr[i].GetComponent<HandleCollisionLL>().current = true;
            spheresCntnr[i].GetComponent<HandleCollisionLL>().index = i + 1; 

            if (spheresCntnr[i].GetComponent<HandleCollisionLL>().sphere_coll == true)
            {
                Debug.Log("bonk");
                //i--;
            }
            else
            {
                //Debug.Log("bink");
                //spheresCntnr[i].GetComponent<Rigidbody>().isKinematic = true;
            }
            // need to move them if they overlap
            spheresCntnr[i].GetComponent<HandleCollisionLL>().current = false;
        }

        for (int i = 0; i < maxTargets; i++)
        {
            if (spheresCntnr[i].GetComponent<HandleCollisionLL>().sphere_coll == true)
            {
                //find new placement
                Debug.Log("bonk");
                spheresCntnr[i].transform.position = new Vector3(Random.Range(left,right), Random.Range(bottom, top));
                spheresCntnr[i].GetComponent<HandleCollisionLL>().sphere_coll = false;
                //i--;
            }
            else 
            {
                //i--;
            }
        }
    }

    // Update is called once per frame
    void Update () {

        if (Input.GetKeyDown(KeyCode.Delete)) //Quick Restart
        {
            EndGame();
            RestartGame();
            Debug.Log("restart ");

        }

        if (Input.GetKeyDown(KeyCode.Backspace)) //Quick Restart
        {
            getBoundaries();
        }



        if (seconds >= timer && gameRunning)
        {
            //end game
            //EndGame();
            gameRunning = false;
            Invoke("EndGame", 1.0f);
        }
        else
        {
            if (gameRunning)
            {
                if ( (Mathf.Floor(Time.time) - startTime ) >= seconds)
                {

                    seconds++;
                    /*
                    //Debug.Log("seconds = " + seconds + "timer = " + timer);
                    if (randChance < randInterval && isTargetOn == false)
                    {
                        StopAllCoroutines();
                        if (seconds <= 3) //hacky way of starting phase 2
                        {
                            //StartCoroutine(TargetOn());
                        }else
                        {
                            //StartCoroutine(TargetsOn());
                        }
                    }
                    randChance = Random.Range(0, 0.4f);
                    */
                }

                for(int i = 0; i < maxTargets; i++)
                {
                    Targets_Container_Attn target = spheresCntnr[i].GetComponent<Targets_Container_Attn>();
                    if (!spheresCntnr[i].gameObject.activeInHierarchy && target.counted == false)
                    {
                        points += target.ReturnPoints();
                        scoreText.text = points.ToString();
                        target.counted = true;
                        numTargets--;
                        Debug.Log("one down. numtargetrs = " + numTargets);
                    }
                }

                //check if gameobject is destroyed and sub 1 from the stack and add a point. 
                //check if all gameobejcts are destroyed and start a new round.
            }
        }

        timerText.text = (timer - seconds).ToString();
        if(numTargets <= 0)
        {
            RestartRound();
        }
        //timerText.text = timer.ToString();
    }

    IEnumerator TargetOn()
    {
        Debug.Log("entered TargetOn()");


        spheres[sphereRand].transform.position = new Vector3(Random.Range(left, right), Random.Range(bottom, top));
        isTargetOn = true;
        spheres[sphereRand].GetComponent<MeshRenderer>().enabled = true;
        spheres[sphereRand].GetComponent<Renderer>().material = materials[2];
        yield return new WaitForSeconds(randDuration);
        //spheres[sphereRand].GetComponent<Renderer>().material = materials[1];
        spheres[sphereRand].GetComponent<MeshRenderer>().enabled = false;
        points = 0;

        sphereRand = Random.Range(0, 3);
        randChance = Random.Range(0, 0.4f);
        isTargetOn = false;
        yield return null;
    }

    IEnumerator TargetsOn() //create 2 spheres in random location 
    {
        Debug.Log("entered TargetsOn()");


        spheres[sphereRand].transform.position = new Vector3(Random.Range(left,right), Random.Range(bottom, top));
        isTargetOn = true;
        spheres[sphereRand].GetComponent<MeshRenderer>().enabled = true;
        spheres[sphereRand].GetComponent<Renderer>().material = materials[2];


        spheres[sphereRand2].transform.position = new Vector3(Random.Range(left, right), Random.Range(bottom, top));
        isTargetOn = true;
        spheres[sphereRand2].GetComponent<MeshRenderer>().enabled = true;
        spheres[sphereRand2].GetComponent<Renderer>().material = materials[2];
        yield return new WaitForSeconds(randDuration);


        //spheres[sphereRand].GetComponent<Renderer>().material = materials[1];
        spheres[sphereRand].GetComponent<MeshRenderer>().enabled = false;
        spheres[sphereRand2].GetComponent<MeshRenderer>().enabled = false;
        points = 0;

        sphereRand = Random.Range(0, 3);
        sphereRand2 = Random.Range(0, 3);
        randChance = Random.Range(0, 0.4f);
        isTargetOn = false;
        yield return null;
    }

    void EndGame()
    {
        Debug.Log("entered EndGame()");

        StopAllCoroutines();
        leftText.text = "GG. Score: ";
        pointText.text = score.ToString();
        isTargetOn = false;
        gameRunning = false;
        seconds = 0;

    }

    void RestartGame()
    {
        StopAllCoroutines();

        timer = ROUND_TIME; //game over time
        startTime = Time.time;
        seconds = 0;

        score = 0;
        points = 0;


        randInterval = Random.Range(0.3f, 0.6f);
        randChance = Random.Range(0, 0.3f);
        randDuration = Random.Range(1.3f, 2.0f);

        leftText.text = "GAZE";
        pointText.text = "CLICK";
        timerText.text = timer.ToString();
        PlaceSpheres();
        gameRunning = true;
    }

    void RestartRound()
    {
        Debug.Log("entered: RestartRound()");
        getBoundaries();
        CreateSpheres();
        PlaceSpheres();
    }


    private Limits GetLimits()
    {
        Limits val = new Limits();
        Vector3 lowerLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, -Camera.main.transform.position.z));
        Vector3 upperRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, -Camera.main.transform.position.z));
        val.Left = lowerLeft.x;
        val.Right = upperRight.x;
        val.Top = upperRight.y - 5f;
        val.Bottom = lowerLeft.y + 5f;
        return val;
    }

    public class Limits
    {
        public float Left { get; set; }
        public float Right { get; set; }
        public float Top { get; set; }
        public float Bottom { get; set; }
    }

}
