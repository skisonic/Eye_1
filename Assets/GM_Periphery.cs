using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Tobii.EyeTracking;


public class GM_Periphery : MonoBehaviour {

    public GameObject[] spheres, quads, cannons;
    public GameObject home;

    private float timer, seconds, startTime, myTimer;
    int sphereRand;
    float randInterval, randInterval2, randChance, randDuration;
    public Material[] materials;

    bool isTargetOn;
    int points;

    
    GazeAware home_gaze;

    public Text score_text, timer_text, hp_label, hp_text;

    public int gameState; //0=start, 1=active, 2=ending, 3=ended
    public const int GAMETIME = 30;

    MeshRenderer rend;
    float hideTimer, showTimer, gazeDmgTimer, targetsHideTimer, targetsShowTimer;
    const float HIDE_TIME = 1.0f;
    const float SHOW_TIME = 3.0f;
    const float GAZE_ERR_TIME = 2.0f;
    const float HIDE_TARGETS_TIME = 2.0f;
    const float SHOW_TARGETS_TIME = 3.0f;


    public int hp, max_hp;
    float shrinkDmg_wait;
    bool shrinkWait;

    const int MAX_HP = 20;

    int prototype_var; //0 = cube, 1 = cannons
    public int score = 0;
    Color og_text;
    bool targetsShowing = false;

    // Use this for initialization
    void Start () {
        rend = home.GetComponent<MeshRenderer>();
        rend.enabled = false;
        for(int i = 0; i <= 1; i++)
        {
            spheres[i].gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        hideTimer = HIDE_TIME;
        showTimer = SHOW_TIME;
        gazeDmgTimer = GAZE_ERR_TIME;
        targetsHideTimer = HIDE_TARGETS_TIME;
        targetsShowTimer = SHOW_TARGETS_TIME;

        startTime = Time.time;
        timer = 25;
        myTimer = GAMETIME;
        seconds = 0;
        hp = MAX_HP;
        randInterval = Random.Range(0.3f, 0.6f);
        randChance = Random.Range(0, 0.3f);

        randDuration = Random.Range(1.3f, 2.0f);
        points = 0;

        gameState = 0; //start game

        home_gaze = home.GetComponent<GazeAware>();


        hp_text.text = hp.ToString();
        og_text = hp_text.color;

    }

    // Update is called once per frame
    void Update () {
        //dec hide timer 
        if (myTimer <= 0 || hp <= 0)
        {
            gameState = 1; //ending game
            //EndGame();
        }
        else
        {
            myTimer -= (Time.deltaTime);
            if (targetsShowing == false)
            {
                targetsShowTimer--;
            }else
            {
                targetsHideTimer--;
            }
            timer_text.text = Mathf.FloorToInt(myTimer).ToString();
        }

        if (rend.enabled == false)
        {
            hideTimer -= Time.deltaTime;
        }
        else
        {
            showTimer -= Time.deltaTime;
            if (home_gaze.HasGazeFocus == false)
            {
                gazeDmgTimer -= Time.deltaTime;
            }
        }

        if (hideTimer <= 0)
        {
            MoveHome();
        }
        if(showTimer <= 0)
        {
            HideHome();
        }
        if(gazeDmgTimer <= 0)
        {
            DamageHome();
        }
        if(targetsHideTimer <= 0)
        {
            MoveTargets();
        }
        if(targetsShowTimer <= 0)
        {
            HideTargets();
        }

	}

    void MoveHome()
    {
        float xPos, yPos, zPos;
        float orthScale; 

        xPos = Random.Range(-9.0f, 9.0f);
        yPos = Random.Range(-9.0f, 9.0f);
        zPos = Random.Range(1.0f, 4.0f);
        orthScale = Random.Range(1.5f, 4.0f);
        Vector3 newPos = new Vector3(xPos, yPos, -zPos*zPos);

        if (Camera.main.orthographic == true)
        {
            home.transform.localScale *= orthScale*orthScale;
        }

        rend.enabled = true;

        home.transform.position = newPos;
        hideTimer = HIDE_TIME;
    }

    void MoveTargets()
    {
        float xPos, yPos, zPos;
        float orthScale;


        for (int i = 0; i <= 1; i++)
        {
            xPos = Random.Range(-9.0f, 9.0f);
            yPos = Random.Range(-9.0f, 9.0f);
            zPos = Random.Range(1.0f, 4.0f);
            orthScale = Random.Range(1.5f, 3.0f);
            Vector3 newPos = new Vector3(xPos, yPos, -zPos * zPos);

            if (Camera.main.orthographic == true)
            {
                spheres[i].transform.localScale *= orthScale * orthScale;
            }

            spheres[i].gameObject.GetComponent<MeshRenderer>().enabled = true;
            

            spheres[i].transform.position = newPos;
        }
        targetsShowing = true;
        targetsHideTimer = HIDE_TARGETS_TIME;

    }

    void HideHome()
    {
        rend.enabled = false;
        if (Camera.main.orthographic)
        {
            home.transform.localScale = Vector3.one;
        }
        showTimer = SHOW_TIME;
    }

    void HideTargets()
    {
        for (int i = 0; i <= 1; i++)
        {
            if (Camera.main.orthographic)
            {
                spheres[i].transform.localScale = Vector3.one;
            }
            spheres[i].gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        targetsShowTimer = SHOW_TARGETS_TIME;
        targetsShowing = false;
    }

    void DamageHome()
    {
        hp--;
        hp_text.text = hp.ToString();
        gazeDmgTimer = GAZE_ERR_TIME;
        StartCoroutine(FlashHPTextRed());
    }

    IEnumerator FlashHPTextRed()
    {
        StopCoroutine(FlashHPTextRed());
        hp_text.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        hp_text.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        hp_text.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        hp_text.color = og_text;
    }
}
