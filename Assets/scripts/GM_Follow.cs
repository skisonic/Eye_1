using UnityEngine;
using System.Collections;
using Tobii.EyeTracking;
using UnityEngine.UI;

/// <summary>
/// conversaion from cannons to this gaze following game. 
/// 4 surrounding columns will act as ink wells or w/e, where you must gaze to change your targets
/// affinity. 
/// Must use the correct affinity to kill a target 
/// </summary>
public class GM_Follow : MonoBehaviour
{

    public GameObject[] quads, cannons;
    public GameObject home, spawner;

    private float seconds, startTime, myTimer;
    int sphereRand;
    float randInterval, randInterval2, randChance, randDuration;
    public Material[] materials;

    bool isTargetOn;
    int points;

    public Text score_text, timer_text, hp_label, hp_text;

    public int gameState; //0=start, 1=active, 2=ending, 3=ended
    public const int GAMETIME = 60;
    bool debugControls = true; 

    // Use this for initialization
    void Start()
    {
        
        Application.targetFrameRate = 30;
        startTime = Time.time;
        myTimer = GAMETIME;
        seconds = 0;
        randInterval = Random.Range(0.3f, 0.6f);
        randChance = Random.Range(0, 0.3f);

        randDuration = Random.Range(1.3f, 2.0f);
        points = 0;

        gameState = 0; //start game

        sphereRand = Random.Range(0, 2);
    }

    // Update is called once per frame
    void Update()
    {
        if (debugControls)
        {

            if (Input.GetKeyDown(KeyCode.Home)) // Quick Restart
            {
                ResetGame();
            }


            if (Input.GetKeyDown(KeyCode.PageUp)) // Quick add 5s
            {
                myTimer += 5.0f;
            }

            if (Input.GetKeyDown(KeyCode.F1)) // Quick add 5HP
            {
                home.GetComponent<Home_Damage_Follow>().hp += 5;
            }

            if (Input.GetKeyDown(KeyCode.End)) //Quick Drop an enemy
            {
                spawner.GetComponent<SpawnEnemies>().DropEnemiesDebug();
            }
        }

        if (myTimer <= 0 || home.GetComponent<Home_Damage_Follow>().hp <= 0)
        {
            gameState = 1; //ending game
            EndGame();

        }
        else
        {
            myTimer -= (Time.deltaTime);
            timer_text.text = Mathf.FloorToInt(myTimer).ToString();
            score_text.text = home.GetComponent<Home_Damage_Follow>().score.ToString();
        }

        if (Mathf.Floor(Time.time) >= seconds)
        {
            seconds++;
            //Debug.Log("seconds = " + seconds);
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

        /*
        if (home.GetComponent<GazeAware>().HasGazeFocus)
        {
            home.GetComponent<Renderer>().material = materials[0];
        }
        else
        {
            home.GetComponent<Renderer>().material = materials[1];
        }*/

    }

    public void EndGame()
    {
        gameState = 2;
        spawner.GetComponent<SpawnEnemies>().enabled = false;
        hp_label.color = Color.red;
        hp_label.text = "Game\nOver";
        hp_text.color = Color.red;
        hp_text.text = "0";
        home.GetComponent<Home_Damage_Follow>().enabled = false;
    }

    public void ResetGame()
    {
        
        spawner.GetComponent<SpawnEnemies>().enabled = true;
        home.GetComponent<Home_Damage_Follow>().enabled = enabled;
        home.GetComponent<Home_Damage_Follow>().hp = home.GetComponent<Home_Damage_Follow>().max_hp;
        hp_label.color = Color.white;
        hp_label.text = "HP";
        hp_text.color = Color.green;
        hp_text.text = home.GetComponent<Home_Damage_Follow>().hp.ToString();
        foreach (GameObject enemy_cube in (GameObject.FindGameObjectsWithTag("Enemy_Cube")))
        {
            Destroy(enemy_cube);
        }
        foreach (GameObject bullet in (GameObject.FindGameObjectsWithTag("Bullet")))
        {
            Destroy(bullet);
        }
        home.GetComponent<Home_Damage_Follow>().score = 0;
        myTimer = GAMETIME + 1;
        foreach (GameObject cannon in cannons)
        {
            cannon.GetComponent<CannonStats>().PowerOn();
        }
    }
}
    