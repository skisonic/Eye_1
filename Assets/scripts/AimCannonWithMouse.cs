using UnityEngine;
using System.Collections;
using Tobii.EyeTracking;


/// <summary>
/// look at the camera to activate it.
/// times out after a certain period of time
/// 1 fire key?
/// lets run that shit.
/// USE THIS FOR CANNONS_2
/// </summary>

[RequireComponent(typeof(GazeAware))]
public class AimCannonWithMouse : MonoBehaviour
{


    private GazeAware _gazeAware;
    public GameObject bullet_pf;
    public GameObject cannon;
    GameObject sphereL, sphereR;
    GazePoint gazePoint;
    Vector3 gazePoint3;

    GameObject bullet;
    GameObject cannonL;
    int aim_interval;
    const int AIM_INT = 10;
    float start_time, current_time;


    Ray ray, ray2;
    RaycastHit hit;

    void Start()
    {
        _gazeAware = GetComponent<GazeAware>();
        aim_interval = AIM_INT;
        start_time = Time.time;
        current_time = Time.time;
        cannonL = GameObject.Find("CannonL");
    }

    // Update is called once per frame
    void Update()
    {

        current_time = Time.time;

        // Rotate the camera every frame so it keeps looking at the target 
        gazePoint = EyeTracking.GetGazePoint();
        gazePoint3 = new Vector3(gazePoint.Viewport.x, gazePoint.Viewport.y, 0);

        /*
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        Physics.Raycast(ray, out hit);
        */

        //ray = new Ray(transform.position, Input.mousePosition);
        ray = new Ray(transform.position, transform.right);
        //ray2 = new Ray(Camera.main.transform.position, Camera.main.WorldToScreenPoint(Input.mousePosition));
        ray2 = new Ray(Camera.main.transform.position, Input.mousePosition);
        Debug.Log("Camera.main.ScreenToWorldPoint(Input.mousePosition) " + Camera.main.ScreenPointToRay(Input.mousePosition));
        //Debug.DrawRay(ray.origin, ray.direction + transform.right * 100f);
        // Debug.DrawRay(ray2.origin, ray2.direction + new Vector3(0, 0, 25f));

        if (Physics.Raycast(ray, out hit, 1000f))
        {
            transform.LookAt(hit.point);
            Debug.Log("hit = " + hit.collider.gameObject.name);
        }
        /*

        if (hit.collider.gameObject.name == "Rear Wall") //simulate gaze with raycast
        {
            Debug.Log("hit info" + hit.point);
            //transform.LookAt(Camera.main.ScreenPointToRay(Input.mousePosition) - transform.position));
            transform.LookAt(hit.point);
            Debug.Log("Moues position on screen" + Input.mousePosition);
        }
        */



        if (_gazeAware.HasGazeFocus)
        {
            aim_interval--;
            gazePoint = EyeTracking.GetGazePoint();
            gazePoint3 = new Vector3(gazePoint.Viewport.x, gazePoint.Viewport.y, 0);

            if (aim_interval == 0)
            {
                gameObject.transform.Rotate(Vector3.forward, Vector3.Angle(gameObject.transform.position, gazePoint3));

                Vector3 targetDir = gameObject.transform.position - gazePoint3;
                float angle = Vector3.Angle(targetDir, transform.forward);

                Debug.Log("game object posisiton " + gameObject.transform.position + "gazepoint position " + gazePoint3 + Vector3.Angle(gameObject.transform.position, gazePoint3));
                Debug.Log("Corrected? " + angle);
                aim_interval = AIM_INT;
            }
            //Debug.Log("found it");
        }

        if (current_time >= start_time + 10.0f)
        {
            aim_interval--;
            start_time = Time.time;
            Debug.Log("aim _interval = " + aim_interval);
        }
    }
}
