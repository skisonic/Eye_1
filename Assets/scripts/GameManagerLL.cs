using UnityEngine;
using System.Collections;


/// <summary>
/// Randomly make any number of 3 lights visible. Gaze spheres to light them and get points.
/// </summary>

public class GameManagerLL : MonoBehaviour {


    private float timer, seconds;
    int sphereRand;
    float randInterval, randInterval2, randChance, randDuration;
    public Material[] materials;


    GameObject sphereL, sphereM, sphereR;
    public GameObject[] spheres;
    bool isTargetOn;
    int points;
    int numTargets;

    protected GameObject sphere_pf;

    const int maxTargets = 10;
    private GameObject[] spheresCntnr = new GameObject[maxTargets];
    float xBound = 5.0f;
    float yBound = 4.0f;

	// Use this for initialization
	void Start () {

        Application.targetFrameRate = 30;

        timer = 25;
        seconds = 0;
        randInterval = Random.Range(0.3f, 0.6f);
        randChance = Random.Range(0, 0.3f);

        randDuration = Random.Range(1.3f, 2.0f);
        points = 0;

        sphere_pf = Resources.Load<GameObject>("prefabs/Sphere_pf");


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

        for(int i = 0; i < maxTargets; i++)
        {
            spheresCntnr[i] = ((GameObject)Instantiate(sphere_pf, new Vector3 (Random.Range(-xBound,xBound),
                                                                               Random.Range(-2.0f,yBound)), Quaternion.identity));

            // need to move them if they overlap
            //spheresCntnr[i] = Instantiate<GameObject>(sphere_pf);

            //instantiate sphere 
        }

    }

    // Update is called once per frame
    void Update () {
	    if(Time.time >= timer)
        {
            //end

        }

        if (Mathf.Floor(Time.time) >= seconds)
        {
            seconds++;
            Debug.Log("seconds = " + seconds);
            if(randChance < randInterval  && isTargetOn == false)
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
                points++;
            }
            if (sphereRand == 1 && Input.GetKeyDown(KeyCode.Alpha6))
            {
                points++;
            }
            if (sphereRand == 2 && Input.GetKeyDown(KeyCode.Alpha0))
            {
                points++;
            }
        }
    }

    IEnumerator TargetOn()
    {
        isTargetOn = true;
        spheres[sphereRand].GetComponent<MeshRenderer>().enabled = true;
        //spheres[sphereRand].GetComponent<Renderer>().material = materials[0];
        yield return new WaitForSeconds(randDuration);
        //spheres[sphereRand].GetComponent<Renderer>().material = materials[1];
        spheres[sphereRand].GetComponent<MeshRenderer>().enabled = false;

        sphereRand = Random.Range(0, 3);
        randChance = Random.Range(0, 0.4f);
        isTargetOn = false;
        yield return null;
    }
}
