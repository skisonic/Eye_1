using UnityEngine;
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


    private float timer, seconds, startTime, roundTime;
    int sphereRand, sphereRand2;
    float randInterval, randInterval2, randChance, randDuration;
    public Material[] materials;


    GameObject sphereL, sphereM, sphereR;
    public GameObject[] spheres;
    bool isTargetOn;
    float points, score;
    int numTargets;

    protected GameObject sphere_pf;

    const int maxTargets = 10;
    private GameObject[] spheresCntnr = new GameObject[maxTargets];
    float xBound = 5.0f;
    float yBound = 4.0f;
    float pointGrowthRateGaze = 1.05f;
    bool gameRunning = false;

    Text pointText, leftText;


    // Use this for initialization
    
    void Start()
    {

        Application.targetFrameRate = 30;

        timer = 45;
        roundTime = 20; //game over time
        startTime = Time.time;
        seconds = 0;
        randInterval = Random.Range(0.3f, 0.6f);
        randChance = Random.Range(0, 0.3f);

        randDuration = Random.Range(1.3f, 2.0f);
        score = 0;
        points = 1;

        sphere_pf = Resources.Load<GameObject>("prefabs/Sphere_pf");
        pointText = GameObject.Find("Text (1)").GetComponent<Text>();
        leftText = GameObject.Find("Text").GetComponent<Text>();


        /*
        sphereL = GameObject.Find("SphereL");
        sphereM = GameObject.Find("SphereM");
        sphereR = GameObject.Find("SphereR");

        spheres[0] = sphereL;
        spheres[1] = sphereM;
        spheres[2] = sphereR;
        */

        spheres[0].GetComponent<MeshRenderer>().enabled = false;
        spheres[1].GetComponent<MeshRenderer>().enabled = false;
        spheres[2].GetComponent<MeshRenderer>().enabled = false;

        sphereRand = Random.Range(0, 2);
        sphereRand2 = Random.Range(0, 2);


        CreateSpheres();
        PlaceSpheres();
        gameRunning = true;

    }

    void CreateSpheres()
    {
        for (int i = 0; i < maxTargets; i++)
        {
            spheresCntnr[i] = ((GameObject)Instantiate(sphere_pf, new Vector3(Random.Range(-xBound, xBound),
                                                                               Random.Range(-2.0f, yBound)), Quaternion.identity));

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
            //spheresCntnr[i] = Instantiate<GameObject>(sphere_pf);
            spheresCntnr[i].GetComponent<HandleCollisionLL>().current = false;
            //instantiate sphere 
        }

        for (int i = 0; i < maxTargets; i++)
        {
            if (spheresCntnr[i].GetComponent<HandleCollisionLL>().sphere_coll == true)
            {
                //find new placement
                Debug.Log("bonk");
                spheresCntnr[i].transform.position = new Vector3(Random.Range(-xBound, xBound), Random.Range(-2.0f, yBound));
                spheresCntnr[i].GetComponent<HandleCollisionLL>().sphere_coll = false;
                //i--;
            }
            else
            {
                //i--;
            }

        }
    }

    private Limits GetLimits()
    {
        Limits val = new Limits();
        Vector3 lowerLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
        Vector3 upperRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        val.Left = lowerLeft.x + 5f;
        val.Right = upperRight.x - 5f;
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



    void PlaceSpheres()
    {

        spheres[0].GetComponent<MeshRenderer>().enabled = false;
        spheres[1].GetComponent<MeshRenderer>().enabled = false;
        spheres[2].GetComponent<MeshRenderer>().enabled = false;

        sphereRand = Random.Range(0, 2);

        for (int i = 0; i < maxTargets; i++) //this bit of code generates a spehere and checks that it doesnt collide with the last sphere created.
        {

            spheresCntnr[i].transform.position = new Vector3(Random.Range(-xBound, xBound), Random.Range(-2.0f, yBound));

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
            //spheresCntnr[i] = Instantiate<GameObject>(sphere_pf);
            spheresCntnr[i].GetComponent<HandleCollisionLL>().current = false;
            //instantiate sphere 
        }

        for (int i = 0; i < maxTargets; i++)
        {
            if (spheresCntnr[i].GetComponent<HandleCollisionLL>().sphere_coll == true)
            {
                //find new placement
                Debug.Log("bonk");
                spheresCntnr[i].transform.position = new Vector3(Random.Range(-xBound, xBound), Random.Range(-2.0f, yBound));
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

        if (Input.GetKeyDown(KeyCode.Delete))
        {
            EndGame();
            RestartGame();
            Debug.Log("restart ");
        }

        if (seconds >= roundTime)
        {
            //end game
            //EndGame();
            Invoke("EndGame", 1.0f);
        }
        else
        {
            if (gameRunning)
            {
                if ( (Mathf.Floor(Time.time) - startTime ) >= seconds)
                {
                    timer -= timer - seconds;

                    seconds++;
                    Debug.Log("seconds = " + seconds + "roundtime = " + roundTime);
                    if (randChance < randInterval && isTargetOn == false)
                    {
                        StopAllCoroutines();
                        if (seconds <= 3) //hacky way of starting phase 2
                        {
                            StartCoroutine(TargetOn());
                        }else
                        {
                            StartCoroutine(TargetsOn());
                        }
                    }
                    randChance = Random.Range(0, 0.4f);
                }

                if (isTargetOn)
                {

                    //if (sphereRand == 0 && (Input.GetKeyDown(KeyCode.Alpha1)))
                    if ((sphereRand == 0 || sphereRand2 == 0) && (Input.GetKeyDown(KeyCode.Mouse0))) //click
                    {
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hit;
                        Debug.Log("points = " + points);
                        Debug.Log("moues = " + Input.mousePosition);
                        Physics.Raycast(ray, out hit);
                        if (hit.collider.gameObject.name == "SphereL") //simulate gaze with raycast
                        {
                            points++;
                            if (points == 1) score += 5; // extra points for actually clicking
                            score += points;
                            hit.collider.gameObject.GetComponent<Renderer>().material = materials[0];
                            spheres[0].GetComponent<Renderer>().material.color += new Color(points/10.0f, 0, 0);
                            Debug.Log("nailed sphere L");
                        }
                    }

                    if (spheres[0].GetComponent<HandleGazeLL>().hasGaze)
                    {
                        if (spheres[0].GetComponent<HandleGazeLL>().hasGaze) //simulate gaze with raycast
                        {
                            points *= pointGrowthRateGaze;
                            score += Mathf.CeilToInt(points);
                            spheres[0].GetComponent<Renderer>().material = materials[1];
                            spheres[0].GetComponent<Renderer>().material.color += new Color(points/10.0f, 0, 0);
                            Debug.Log("nailed sphere L");
                        }
                    }

                    //if (sphereRand == 1 && (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Alpha2))) //middle sphere
                    if ((sphereRand == 1 || sphereRand2 == 1) && (Input.GetKeyDown(KeyCode.Mouse0)))
                    {
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hit;
                        Debug.Log("points = " + points);
                        Debug.Log("moues = " + Input.mousePosition);
                        Physics.Raycast(ray, out hit);
                        if (hit.collider.gameObject.name == "SphereM" || spheres[1].GetComponent<HandleGazeLL>().hasGaze) //simulate gaze with raycast
                        {
                            points++;
                            if (points == 1) score += 5; // extra points for actually clicking
                            score += points;
                            hit.collider.gameObject.GetComponent<Renderer>().material = materials[0];
                            spheres[1].GetComponent<Renderer>().material.color += new Color(points / 10.0f, 0, 0);
                            Debug.Log("nailed sphere M");
                        }
                    }

                    if (spheres[1].GetComponent<HandleGazeLL>().hasGaze) 
                    {
                        if (spheres[1].GetComponent<HandleGazeLL>().hasGaze) 
                        {
                            points++;
                            score += Mathf.CeilToInt(points);
                            spheres[1].GetComponent<Renderer>().material = materials[0];
                            spheres[1].GetComponent<Renderer>().material.color += new Color(points / 10.0f, 0, 0);
                            Debug.Log("nailed sphere L");
                        }
                    }

                    //if (sphereRand == 2 && (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Alpha3)))
                    if ((sphereRand == 2 || sphereRand2 == 2) && (Input.GetKeyDown(KeyCode.Mouse0)))
                    {
                        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                        RaycastHit hit;
                        Debug.Log("points = " + points);
                        Debug.Log("moues = " + Input.mousePosition);
                        Physics.Raycast(ray, out hit);
                        if (hit.collider.gameObject.name == "SphereR" || spheres[2].GetComponent<HandleGazeLL>().hasGaze) //simulate gaze with raycast
                        {
                            points++;
                            score += points;
                            if (points == 1) score += 5; // extra points for actually clicking
                            hit.collider.gameObject.GetComponent<Renderer>().material = materials[0];
                            spheres[2].GetComponent<Renderer>().material.color += new Color(points / 10.0f, 0, 0);
                            Debug.Log("nailed sphere R");
                        }
                    }

                    if (spheres[2].GetComponent<HandleGazeLL>().hasGaze)
                    {
                        if (spheres[2].GetComponent<HandleGazeLL>().hasGaze) 
                        {
                            points++;
                            score += Mathf.CeilToInt(points);
                            spheres[2].GetComponent<Renderer>().material = materials[0];
                            spheres[2].GetComponent<Renderer>().material.color += new Color(points / 10.0f, 0, 0);
                            Debug.Log("nailed sphere L");
                        }
                    }


                    //timer -= timer - Time.time;
                    pointText.text = score.ToString();
                    //leftText.text = timer.ToString();
                    leftText.text = (roundTime - seconds).ToString();
                }
            }
        }
    }

    IEnumerator TargetOn()
    {
        Debug.Log("entered Target on");


        spheres[sphereRand].transform.position = new Vector3(Random.Range(-xBound, xBound), Random.Range(-2.0f, yBound));
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

    IEnumerator TargetsOn()
    {
        int target_index;
        Debug.Log("entered targets on");


        spheres[sphereRand].transform.position = new Vector3(Random.Range(-xBound, xBound), Random.Range(-2.0f, yBound));
        isTargetOn = true;
        spheres[sphereRand].GetComponent<MeshRenderer>().enabled = true;
        spheres[sphereRand].GetComponent<Renderer>().material = materials[2];


        spheres[sphereRand2].transform.position = new Vector3(Random.Range(-xBound, xBound), Random.Range(-2.0f, yBound));
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
        Debug.Log("entered endgame");

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

        timer = 15;
        roundTime = 20; //game over time
        startTime = Time.time;
        seconds = 0;
        randInterval = Random.Range(0.3f, 0.6f);
        randChance = Random.Range(0, 0.3f);

        randDuration = Random.Range(1.3f, 2.0f);
        score = 0;
        points = 1;


        leftText.text = roundTime.ToString();
        PlaceSpheres();
        Debug.Log("placed sphere");
        gameRunning = true;
    }
}
