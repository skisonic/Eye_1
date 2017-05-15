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
/// 
/// actually none of that is true anymore pretty much - 04/26/2017
/// </summary>

public class GM_Attention : MonoBehaviour {


    private float timer, startTime, myTimer, roundTimer, roundStartTime;
    float roundCompleteTime;
    public Material[] materials;

    protected GameObject sphere_pf, target_pf;
    private GameObject[] spheresCntnr = new GameObject[MAX_TARGETS]; //target container

    float points, score;
    int numTargets, numGazers, numClickers; //number of active targets, and of each type.
    int killedGazers, killedMousers; //counters for number of killed targets for a round
    int totalTargets; //total number of targets during round

    int round_count; // counter for number of rounds completed (current round)
    int start_round = 5; //starting round -1

    bool gameRunning = false; 

    const int MAX_TARGETS = 7; //limit on max targets for a round (not implented yet)
    const int MAX_GAZERS = 4;
    [Tooltip("Total game time")]
    public int GAME_TIME = 45;
    const int ROUND_TIME = 60; //amount of time per round (not implemented yet)

    Text leftText, rightText, timerText, scoreText, pointText;
    float left, right, top, bottom; //boundaries 

    bool debugControls = true; //debug stuff found in update
    // Use this for initialization
    void Start()
    {        
        Application.targetFrameRate = 30;

        myTimer = GAME_TIME; //time before game over occurs
        startTime = Time.time;
        totalTargets = 1; //initial value for total number of targets per round

        score = 0;
        points = 0;
        round_count = start_round; //gets incremented at first NewRound call
        numTargets = numGazers = numClickers = 0;

        target_pf = Resources.Load<GameObject>("prefabs/Attn_Target");
        sphere_pf = Resources.Load<GameObject>("prefabs/Sphere_pf");
        leftText = GameObject.Find("Text").GetComponent<Text>();
        rightText = GameObject.Find("Text (1)").GetComponent<Text>();
        timerText = GameObject.Find("Timer").GetComponent<Text>();
        scoreText = GameObject.Find("Score").GetComponent<Text>();

        scoreText.text = score.ToString();

        myInit();
        NewRound();
    }

    void myInit()
    {
        //bookingkeeping initation function
 
    }

    void NewRound() //starts new round including first round
    {
        round_count++;
        roundCompleteTime = 0;
        roundStartTime = 0;
        leftText.text = "GAZE";

        Debug.Log("NewRound(): round count " + round_count);
        getBoundaries();
        //CreateSpheres();
        //PlaceSpheres();
        newCreateSpheres();
        newPlaceSpheres();
        gameRunning = true;
    }


    void newCreateSpheres()
    {
        int rand, type;
        int minGazers;
        Vector3 location;

        numTargets = 0; //incremental counter for number Of targets
        numGazers = 0; //incremental counter for number Of gazer targets
        numClickers = 0; //incremental counter for number Of mouser targets

        location = new Vector3(0, 0);

        if (round_count <= MAX_TARGETS) //limit num targetse to same as round or less than max lame
        {
            switch (round_count)
            {
                case 1:
                    totalTargets = 2;
                    break;
                case 2:
                    totalTargets = 2;
                    break;
                case 3:
                    totalTargets = 2;
                    break;
                case 4:
                    totalTargets = 2;
                    break;
                case 5:
                    totalTargets = 3;
                    break;
                case 6:
                    totalTargets = 4;
                    break;
                case 7:
                    totalTargets = 5;
                    break;
                case 8:
                    totalTargets = 6;
                    break;
                default:
                    totalTargets = MAX_TARGETS;
                    break;
            }
        }
        else
        {
            totalTargets = MAX_TARGETS;
        }

        minGazers = totalTargets / 2;

        for (int i = 0; i < totalTargets; i++)
        {

            spheresCntnr[i] = ((GameObject)Instantiate(target_pf, location, Quaternion.identity));

            spheresCntnr[i].GetComponent<HandleCollisionLL>().current = true;
            spheresCntnr[i].GetComponent<HandleCollisionLL>().index = i + 1;
            spheresCntnr[i].gameObject.name = "Attn_Target_" + i.ToString();
            numTargets++;


            rand = Random.Range(0, 2);

            if (numGazers <= minGazers)
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
                //Debug.Log("else if 1" + "numtargets = " + numTargets + " num gazers count " + numGazers + "type = " + type);
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
                //THIS NEEDS TO BE ADDRESSED 
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
    void CreateSpheres()
    {
        int rand, type;
        int minGazers;
        Vector3 location;

        numTargets = 0; //incremental counter for number Of targets
        numGazers = 0; //incremental counter for number Of gazer targets
        numClickers = 0; //incremental counter for number Of mouser targets

        location = new Vector3(0, 0);

        if (round_count <= MAX_TARGETS) //limit num targetse to same as round or less than max lame
        {
            switch (round_count)
            {
                case 1:
                    totalTargets = 2;
                    break;
                default:
                    totalTargets = round_count;
                    break;
            }
        }
        else
        {
            totalTargets = MAX_TARGETS;
        }

        minGazers = totalTargets / 3;

        for (int i = 0; i < totalTargets; i++)
        {
   
            spheresCntnr[i] = ((GameObject)Instantiate(target_pf, location, Quaternion.identity));

            spheresCntnr[i].GetComponent<HandleCollisionLL>().current = true;
            spheresCntnr[i].GetComponent<HandleCollisionLL>().index = i + 1;
            spheresCntnr[i].gameObject.name = "Attn_Target_" + i.ToString();
            numTargets++;


            rand = Random.Range(0, 2);

            if (numGazers <= minGazers)
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
                //Debug.Log("else if 1" + "numtargets = " + numTargets + " num gazers count " + numGazers + "type = " + type);
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
                //THIS NEEDS TO BE ADDRESSED 
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


    void newPlaceSpheres() //place spheres non randomly
    {
        Vector3[] locations = new Vector3[totalTargets];

        switch (round_count)
        {
            case 1: //totalTargets = 2
                locations[0] = new Vector3(0, -1.5f);
                locations[1] = new Vector3(0, 1.5f);
                break;
            case 2: //totalTargets = 2
                locations[0] = new Vector3(3, 0);
                locations[1] = new Vector3(-3, 0);
                break;
            case 3: //totalTargets = 2
                locations[0] = new Vector3(0, 3.5f);
                locations[1] = new Vector3(0, -3.5f);
                break;
            case 4: //totalTargets = 2
                locations[0] = new Vector3(7.5f, 0);
                locations[1] = new Vector3(-7.5f, 0);
                break;
            case 5: //totalTargets = 3
                locations[0] = new Vector3(0, 4.5f);
                locations[1] = new Vector3(-8.0f, -3.0f);
                locations[2] = new Vector3(8.0f, -3.0f);
                break;
            case 6: //totalTargets = 3
                locations[0] = new Vector3(8.0f, 4.5f);
                locations[1] = new Vector3(-8.0f, -3.0f);
                locations[2] = new Vector3(8.0f, -3.0f);
                locations[3] = new Vector3(-8.0f, 4.5f);
                break;
            default: //totalTargets >= 7
                for (int i = 0; i < totalTargets; i++)
                {
                    locations[i] = new Vector3(Random.Range(left, right), Random.Range(bottom, top));
                }
                break;
        }

        for(int i = 0; i < totalTargets; i++)
        {
            spheresCntnr[i].transform.position = locations[i];
        }

    }

    void PlaceSpheres() //this function moves the spheres (for some reason) but it used to ensure no overlaps
    {
        for (int i = 0; i < totalTargets; i++) //this bit of code generates a spehere and checks that it doesnt collide with the last sphere created.
        {

            Vector3 location;

            location = new Vector3(0, 0);

            switch (round_count)
            {
                default:
                    location = new Vector3(Random.Range(left, right), Random.Range(bottom, top));
                    break;
            }


            spheresCntnr[i].transform.position = location;

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

        for (int i = 0; i < totalTargets; i++) //find new placement to avoid collisions
        {
            if (spheresCntnr[i].GetComponent<HandleCollisionLL>().sphere_coll == true)
            {
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


    void DestroySpheres()
    {
        for (int i = 0; i < totalTargets; i++)
        {
            spheresCntnr[i].gameObject.SetActive(false);
            numTargets--;
        }
        //destroy or deactivate all the spherres w/e im currently doing.
    }

    // Update is called once per frame
    void Update () {

        if (debugControls)
        {
            if (Input.GetKeyDown(KeyCode.PageUp)) // Quick add 5second to timer
            {
                myTimer += 5.0f;
                Debug.Log("Added 5s to timer. Timer = " + myTimer);
            }
            if (Input.GetKeyDown(KeyCode.Delete)) //Quick Restart
            {
                EndGame();
                RestartGame();
            }
            if (Input.GetKeyDown(KeyCode.Escape)) //Quick startnewround
            {
                StartCoroutine("StartNewRound");
            }
        }

        if (myTimer <= 0 && gameRunning)
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
                if (numTargets <= 0) //if all gameobejcts are destroyed and start a new round.
                {
                    //roundCompleteTime = myTimer;
                    Debug.Log("Completed round " + round_count + " in " + roundCompleteTime.ToString("F4") + "seconds");
                    NewRound();
                }
                myTimer -= Time.deltaTime;
                roundCompleteTime += Time.deltaTime;
                timerText.text = myTimer.ToString("F1");
            }
        }
    }

    public void UpdateOnTargetDeath(int value) // public function for target to talkback to GM (ugly but w/e)
    {
        points += value;
        scoreText.text = points.ToString();
        numTargets--;
        if (value == 1)
        {
            killedMousers += 1;
        }
        else if (value == 3)
        {
            killedGazers += 3;
        }
        else
        {
            Debug.Log("Problem updating killed target type");
        }
        //Debug.Log("points = " + points + "value was + " + value);
    }

    IEnumerator StartNewRound() //not yet implemented
    {
        Debug.Log("StartNewRound: round complete time " + roundCompleteTime);
        gameRunning = false;
        leftText.text = "Round " + round_count + " completed in " + roundCompleteTime.ToString("F1") + " seconds.";
        //pause the game.
        //diplay text for just long enough to read it.
        //clear that text
        yield return new WaitForSeconds(1.5f);
        NewRound();
    }


    void EndGame() //Ends the game
    {
        Debug.Log("EndGame(): entered EndGame()");
        DestroySpheres();
        StopAllCoroutines();
        gameRunning = false;
        leftText.text = "GAME";
        rightText.text = "OVER";
    }

    void RestartGame() //Quick Restart Game. Probably needs to be fixed
    {
        Debug.Log("RestartGame: restart...");
        StopAllCoroutines();

        myTimer = GAME_TIME; //game over time
        startTime = Time.time;

        score = 0;
        points = 0;


        leftText.text = "GAZE";
        rightText.text = "CLICK";
        scoreText.text = "0";

        timerText.text = myTimer.ToString();
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
