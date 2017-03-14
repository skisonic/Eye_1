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
    const float HIDE_TIME = 0.5f;
    const float SHOW_TIME = 3.0f;
    const float GAZE_ERR_TIME = 2.0f;
    const float HIDE_TARGETS_TIME = 2.0f;
    const float SHOW_TARGETS_TIME = 3.0f;


    public int hp, max_hp;
    float shrinkDmg_wait;
    bool shrinkWait;

    const int MAX_HP = 10;

    int prototype_var; //0 = cube, 1 = cannons
    public int score = 0;
    Color og_text;
    bool targetsShowing = false;

    Vector3 newPos;

    private float journeyLength;
    public Transform startMarker;
    public Transform endMarker;
    public float speed = 1.0F;

    float left, right, top, bottom;

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

        startTime = Time.time;
        
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
            if (targetsShowing)
            {
                targetsShowTimer -= (Time.deltaTime);
            }else
            {
                targetsHideTimer -= (Time.deltaTime);
            }
            timer_text.text = Mathf.FloorToInt(myTimer).ToString();
        }

        if (rend.enabled == false)
        {
            hideTimer -= Time.deltaTime; //counts down while home base is not visible
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
            MoveAndShowHome();
            //GenNewHomePos();
        }

        if (showTimer <= 0)
        {
            HideHome();
        }

        if (gazeDmgTimer <= 0)
        {
            DamageHome();
        }

        if (targetsHideTimer <= 0)
        {
            MoveTargets();
        }
        else if(targetsShowTimer <= 0)
        {
            HideTargets();
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (targetsShowing)
            {
                points++;
                score_text.text = points.ToString();
            }
            else
            {
                points--;
                score_text.text = points.ToString();
                StartCoroutine(FlashScoreTextRed());
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


    void GenNewHomePos()
    {
        float xPos, yPos, zPos;
        float orthScale;

        xPos = Random.Range(-9.0f, 9.0f);
        yPos = Random.Range(-9.0f, 9.0f);
        zPos = Random.Range(1.0f, 4.0f);
        orthScale = Random.Range(1.5f, 3.0f);
        Vector3 newPos = new Vector3(xPos, yPos, -zPos * zPos);
        rend.enabled = true;
    }

    void MoveAndShowHome()
    {
        float xPos, yPos, zPos;
        float orthScale;


        left = GetLimits().Left;
        right = GetLimits().Right;
        top = GetLimits().Top;
        bottom = GetLimits().Bottom;

        xPos = Random.Range(-left, right);
        yPos = Random.Range(-bottom, top);
        zPos = Random.Range(1.0f, 4.0f);
        orthScale = Random.Range(1.5f, 3.0f);
        newPos = new Vector3(xPos, yPos, -zPos*zPos);

        //Debug.Log("ilimits: Left " + GetLimits().Left + " R: " + GetLimits().Right);

        if (Camera.main.orthographic == true)
        {
            home.transform.localScale *= orthScale*orthScale;
        }

        rend.enabled = true;


        //home.transform.position = newPos;
        StartCoroutine(MoveTwdHomePos());
        hideTimer = HIDE_TIME;

    }


    IEnumerator MoveTwdHomePos()
    {
        float distBetween = Vector2.Distance((Vector2)newPos, (Vector2)home.transform.position);
        while ((distBetween > 0.0f))
        {
            //home.transform.position = Vector3.MoveTowards(home.transform.position, newPos, 0.3f);
            home.transform.position = Vector3.MoveTowards(home.transform.position, newPos, Mathf.Sqrt(distBetween)/40.0f);
            yield return null;
        }
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
            orthScale = Random.Range(1.2f, 2.0f);
            float lightChance = Random.Range(0, 1.0f);
            Vector3 newPos = new Vector3(xPos, yPos, -zPos * zPos);

            if (Camera.main.orthographic == true)
            {
                if (rend.enabled) //if home enabled 
                {
                    //Debug.Log("distance between sphere " + i + " and home = " + Vector2.Distance((Vector2)newPos, (Vector2)home.transform.position));
                }

                if (Vector2.Distance((Vector2)newPos, (Vector2)home.transform.position) > 7.0f)
                {
                    spheres[i].transform.localScale = Vector3.one * ( orthScale * orthScale);
                    spheres[i].gameObject.GetComponent<MeshRenderer>().enabled = true;
                    if (lightChance <= .5f)
                    {
                        spheres[i].gameObject.GetComponent<MeshRenderer>().material = materials[1];
                        targetsShowing = true;
                    }
                    else
                    {
                        spheres[i].gameObject.GetComponent<MeshRenderer>().material = materials[0];
                    }
                    spheres[i].transform.position = newPos;
                }
                else
                {
                    i--;//try again to find a farther spot ugh.
                }

            }
        }
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
            spheres[i].gameObject.GetComponent<MeshRenderer>().material = materials[0];
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

    IEnumerator FlashScoreTextRed()
    {
        StopCoroutine(FlashScoreTextRed());
        score_text.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        score_text.color = Color.white;
        yield return new WaitForSeconds(0.1f);
        score_text.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        score_text.color = og_text;
    }
}
