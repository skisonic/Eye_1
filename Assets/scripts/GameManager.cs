using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {


    private float timer, seconds;
    int sphereRand;
    float randInterval, randInterval2, randChance, randDuration;
    public Material[] materials;


    GameObject sphereL, sphereM, sphereR;
    public GameObject[] spheres;
    bool isTargetOn;
    int points;

	// Use this for initialization
	void Start () {

        Application.targetFrameRate = 30;

        timer = 25;
        seconds = 0;
        randInterval = Random.Range(0.3f, 0.6f);
        randChance = Random.Range(0, 0.3f);

        randDuration = Random.Range(1.3f, 2.0f);
        points = 0;


        /*
        sphereL = GameObject.Find("SphereL");
        sphereM = GameObject.Find("SphereM");
        sphereR = GameObject.Find("SphereR");

        spheres[0] = sphereL;
        spheres[1] = sphereM;
        spheres[2] = sphereR;
        */

        sphereRand = Random.Range(0, 2);


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
        spheres[sphereRand].GetComponent<Renderer>().material = materials[0];
        yield return new WaitForSeconds(randDuration);
        spheres[sphereRand].GetComponent<Renderer>().material = materials[1];
        sphereRand = Random.Range(0, 2);
        randChance = Random.Range(0, 0.4f);
        isTargetOn = false;
        yield return null;
    }
}
