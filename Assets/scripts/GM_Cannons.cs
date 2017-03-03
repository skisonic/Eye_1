using UnityEngine;
using System.Collections;
using Tobii.EyeTracking;
using UnityEngine.UI;

public class GM_Cannons : MonoBehaviour
{

    public GameObject[] spheres, quads, cannons;
    public GameObject home, spawner;

    private float timer, seconds, startTime, myTimer;
    int sphereRand;
    float randInterval, randInterval2, randChance, randDuration;
    public Material[] materials;

    public GameObject sphereL, sphereM, sphereR;

    bool isTargetOn;
    int points;

    private GazeAware _gazeAware_sp1, _gazeAware_sp2, _gazeAware_sp3;
    GazePoint gazePoint_sp1, gazePoint_sp2, gazePoint_sp3;

    public Text score_text, timer_text, hp_label, hp_text;

    public int gameState; //0=start, 1=active, 2=ending, 3=ended

    // Use this for initialization
    void Start()
    {
        
        Application.targetFrameRate = 30;
        startTime = Time.time;
        timer = 25;
        myTimer = 30;
        seconds = 0;
        randInterval = Random.Range(0.3f, 0.6f);
        randChance = Random.Range(0, 0.3f);

        randDuration = Random.Range(1.3f, 2.0f);
        points = 0;

        gameState = 0; //start game

        /*
        sphereL = GameObject.Find("SphereL");
        sphereM = GameObject.Find("SphereM");
        sphereR = GameObject.Find("SphereR");

        spheres[0] = sphereL;
        spheres[1] = sphereM;
        spheres[2] = sphereR;

        _gazeAware_sp1 = sphereL.GetComponent<GazeAware>();
        _gazeAware_sp2 = sphereM.GetComponent<GazeAware>();
        _gazeAware_sp3 = sphereR.GetComponent<GazeAware>();

        */

        sphereRand = Random.Range(0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        myTimer -= (Time.deltaTime);
        timer_text.text = Mathf.FloorToInt(myTimer).ToString();
        score_text.text = home.GetComponent<TakeDamage>().score.ToString();


        if (myTimer <= 0 || home.GetComponent<TakeDamage>().hp <= 0)
        {
            gameState = 1; //ending game
            EndGame();

        }

        if (Mathf.Floor(Time.time) >= seconds)
        {
            seconds++;
            //Debug.Log("seconds = " + seconds);
            /*
            if (randChance < randInterval && isTargetOn == false)
            {
                StopAllCoroutines();
                StartCoroutine(TargetOn());
            }
            */
            randChance = Random.Range(0, 0.4f);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (quads[0].GetComponent<GazeAware>().HasGazeFocus)
            {
                for (int i = 0; i < 4; i++)
                {
                    CannonStats cs = cannons[i].GetComponent<CannonStats>();
                    if (cs.powerOn)
                    {
                        cs.gc = CannonStats.gunColor.red;
                        cs.SwitchColor();
                    }
                    //check if active, if so change color. 
                }
            }
            else if (quads[1].GetComponent<GazeAware>().HasGazeFocus)
            {

                for (int i = 0; i < 4; i++)
                {
                    CannonStats cs = cannons[i].GetComponent<CannonStats>();
                    if (cs.powerOn)
                    {
                        cs.gc = CannonStats.gunColor.green;
                        cs.SwitchColor();
                    }
                    //check if active, if so change color. 
                }
            }
            else if (quads[2].GetComponent<GazeAware>().HasGazeFocus)
            {
                for (int i = 0; i < 4; i++)
                {
                    CannonStats cs = cannons[i].GetComponent<CannonStats>();
                    if (cs.powerOn)
                    {
                        cs.gc = CannonStats.gunColor.blue;
                        cs.SwitchColor();
                    }
                    //check if active, if so change color. 
                }
            }
            else if (quads[3].GetComponent<GazeAware>().HasGazeFocus)
            {
                for (int i = 0; i < 4; i++)
                {
                    CannonStats cs = cannons[i].GetComponent<CannonStats>();
                    if (cs.powerOn)
                    {
                        cs.gc = CannonStats.gunColor.yellow;
                        cs.SwitchColor();
                    }
                    //check if active, if so change color. 
                }
            }


        }


        // sphere specific stuff
        /*

        
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


        if (sphereL.GetComponent<GazeAware>().HasGazeFocus)
        {
            //Debug.Log("found it");
            //gazePoint_sp1 = EyeTracking.GetGazePoint();
            //transform.Rotate(Vector3.forward);
            //home.transform.Rotate(Vector3.right);
            sphereL.GetComponent<Renderer>().material = materials[0];

        }
        else
        {

            spheres[0].GetComponent<Renderer>().material = materials[1];
         
        }

        if (_gazeAware_sp1.HasGazeFocus)
        {
            //Debug.Log("found it");
            //gazePoint_sp1 = EyeTracking.GetGazePoint();
            //transform.Rotate(Vector3.forward);
            //home.transform.Rotate(Vector3.forward);
            spheres[0].GetComponent<Renderer>().material = materials[0];
        }
        else
        {

            spheres[0].GetComponent<Renderer>().material = materials[1];
        }

        if (_gazeAware_sp2.HasGazeFocus)
        {
            //Debug.Log("found it");
            //gazePoint_sp1 = EyeTracking.GetGazePoint();
            //transform.Rotate(Vector3.forward);
            //home.transform.Rotate(Vector3.right);
            spheres[1].GetComponent<Renderer>().material = materials[0];
        }
        else
        {

            spheres[1].GetComponent<Renderer>().material = materials[1];
        }


        if (_gazeAware_sp3.HasGazeFocus)
        {
            //Debug.Log("found it");
            //gazePoint_sp1 = EyeTracking.GetGazePoint();
            //transform.Rotate(Vector3.forward);
            //home.transform.Rotate(Vector3.down);
            spheres[2].GetComponent<Renderer>().material = materials[0];
        }
        else
        {

            spheres[2].GetComponent<Renderer>().material = materials[1];
        }
        */


        if (home.GetComponent<GazeAware>().HasGazeFocus)
        {
            home.GetComponent<Renderer>().material = materials[0];
        }
        else
        {
            home.GetComponent<Renderer>().material = materials[1];
        }

        if (Input.GetKeyDown(KeyCode.Home))
        {
            ResetGame();
        }

    }

    public void EndGame()
    {
        gameState = 2;
        spawner.GetComponent<SpawnEnemies>().enabled = false;
        hp_label.text = "Game\nOver";
        hp_text.text = "0";
        home.GetComponent<TakeDamage>().enabled = false;
    }

    public void ResetGame()
    {
        
        spawner.GetComponent<SpawnEnemies>().enabled = true;
        home.GetComponent<TakeDamage>().enabled = enabled;
        home.GetComponent<TakeDamage>().hp = home.GetComponent<TakeDamage>().max_hp;
        hp_label.text = "HP";
        hp_text.text = home.GetComponent<TakeDamage>().hp.ToString();
        foreach(GameObject enemy_cube in (GameObject.FindGameObjectsWithTag("Enemy_Cube")))
        {
            Destroy(enemy_cube);
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
    