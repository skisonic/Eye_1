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
/// </summary>

public class GameManagerLL : MonoBehaviour {


    private float timer, seconds, startTime, roundTime;
    int sphereRand;
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

    Text pointText, leftText;

    // Use this for initialization
    void Start()
    {

        Application.targetFrameRate = 30;

        timer = 15;
        roundTime = 10; //game over time
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

    // Update is called once per frame
    void Update () {
        if (seconds >= roundTime)
        {
            //end game
            //EndGame();
            Invoke("EndGame", 1.0f);
        }
        else
        {

            if (Mathf.Floor(Time.time) >= seconds)
            {
                timer -= timer - seconds;

                seconds++;
                Debug.Log("seconds = " + seconds);
                if (randChance < randInterval && isTargetOn == false)
                {
                    StopAllCoroutines();
                    StartCoroutine(TargetOn());
                }
                randChance = Random.Range(0, 0.4f);
            }

            if (isTargetOn)
            {

                if (sphereRand == 0 && Input.GetKeyDown(KeyCode.Alpha1))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    Debug.Log("points = " + points);
                    Debug.Log("moues = " + Input.mousePosition);
                    Physics.Raycast(ray, out hit);
                    if (hit.collider.gameObject.name == "SphereL") //simulate gaze with raycast
                    {
                        points++;
                        score += points;
                        hit.collider.gameObject.GetComponent<Renderer>().material = materials[0];
                        Debug.Log("nailed sphere L");
                    }
                }
                if (sphereRand == 1 && (Input.GetKeyDown(KeyCode.Alpha5) || Input.GetKeyDown(KeyCode.Alpha6) || Input.GetKeyDown(KeyCode.Alpha2)))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    Debug.Log("points = " + points);
                    Debug.Log("moues = " + Input.mousePosition);
                    Physics.Raycast(ray, out hit);
                    if (hit.collider.gameObject.name == "SphereM") //simulate gaze with raycast
                    {
                        points++;
                        score += points;
                        hit.collider.gameObject.GetComponent<Renderer>().material = materials[0];
                        Debug.Log("nailed sphere M");
                    }
                }
                if (sphereRand == 2 && (Input.GetKeyDown(KeyCode.Alpha0) || Input.GetKeyDown(KeyCode.Alpha3)))
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;
                    Debug.Log("points = " + points);
                    Debug.Log("moues = " + Input.mousePosition);
                    Physics.Raycast(ray, out hit);
                    if (hit.collider.gameObject.name == "SphereR") //simulate gaze with raycast
                    {
                        points++;
                        score += points;
                        hit.collider.gameObject.GetComponent<Renderer>().material = materials[0];
                        Debug.Log("nailed sphere R");
                    }
                }
                //timer -= timer - Time.time;
                pointText.text = score.ToString();
                //leftText.text = timer.ToString();
                leftText.text = (roundTime - seconds).ToString(); 
            }
        }
    }

    IEnumerator TargetOn()
    {
        isTargetOn = true;
        spheres[sphereRand].GetComponent<MeshRenderer>().enabled = true;
        spheres[sphereRand].GetComponent<Renderer>().material = materials[1];
        yield return new WaitForSeconds(randDuration);
        //spheres[sphereRand].GetComponent<Renderer>().material = materials[1];
        spheres[sphereRand].GetComponent<MeshRenderer>().enabled = false;
        points = 0;

        sphereRand = Random.Range(0, 3);
        randChance = Random.Range(0, 0.4f);
        isTargetOn = false;
        yield return null;
    }

    void EndGame()
    {
        StopAllCoroutines();
        leftText.text = "GG. Score: ";
        pointText.text = score.ToString();

    }
}
