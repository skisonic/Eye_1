using UnityEngine;
using System.Collections;
using Tobii.EyeTracking;
using UnityEngine.UI;

public class GM_CubeControl : MonoBehaviour
{

    public GameObject[] spheres;
    public GameObject cube;

    private float timer, seconds;
    int sphereRand;
    float randInterval, randInterval2, randChance, randDuration;
    public Material[] materials;

    public GameObject sphereL, sphereM, sphereR;

    bool isTargetOn;
    int points;

    private GazeAware _gazeAware_sp1, _gazeAware_sp2, _gazeAware_sp3;
    GazePoint gazePoint_sp1, gazePoint_sp2, gazePoint_sp3;

    GameObject home;
    Text hp_label, hp_text;

    // Use this for initialization
    void Start()
    {

        Application.targetFrameRate = 60;

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

        

        _gazeAware_sp1 = sphereL.GetComponent<GazeAware>();
        _gazeAware_sp2 = sphereM.GetComponent<GazeAware>();
        _gazeAware_sp3 = sphereR.GetComponent<GazeAware>();

        home = GameObject.Find("Home");
        hp_label = GameObject.Find("HP_Label_Text").GetComponent<Text>();
        hp_text = GameObject.Find("HP_Text").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time >= timer)
        {
            //end

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
            Debug.Log("found it");
            //gazePoint_sp1 = EyeTracking.GetGazePoint();
            //transform.Rotate(Vector3.forward);
            cube.transform.Rotate(Vector3.right);
            sphereL.GetComponent<Renderer>().material = materials[0];

        }
        else
        {

            spheres[0].GetComponent<Renderer>().material = materials[1];
         
        }


        if (_gazeAware_sp1.HasGazeFocus)
        {
            Debug.Log("found it");
            //gazePoint_sp1 = EyeTracking.GetGazePoint();
            //transform.Rotate(Vector3.forward);
            cube.transform.Rotate(Vector3.forward);
            spheres[0].GetComponent<Renderer>().material = materials[0];
        }
        else
        {

            spheres[0].GetComponent<Renderer>().material = materials[1];
        }

        if (_gazeAware_sp2.HasGazeFocus)
        {
            Debug.Log("found it");
            //gazePoint_sp1 = EyeTracking.GetGazePoint();
            //transform.Rotate(Vector3.forward);
            cube.transform.Rotate(Vector3.right);
            spheres[1].GetComponent<Renderer>().material = materials[0];
        }
        else
        {

            spheres[1].GetComponent<Renderer>().material = materials[1];
        }


        if (_gazeAware_sp3.HasGazeFocus)
        {
            Debug.Log("found it");
            //gazePoint_sp1 = EyeTracking.GetGazePoint();
            //transform.Rotate(Vector3.forward);
            cube.transform.Rotate(Vector3.down);
            spheres[2].GetComponent<Renderer>().material = materials[0];
        }
        else
        {

            spheres[2].GetComponent<Renderer>().material = materials[1];
        }



        if (cube.GetComponent<GazeAware>().HasGazeFocus)
        {
            cube.GetComponent<Renderer>().material = materials[0];
        }
        else
        {
            cube.GetComponent<Renderer>().material = materials[1];
        }

    }

    void EndGame()
    {
        hp_label.text = "Game Over";
        hp_text.text = "0";
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
