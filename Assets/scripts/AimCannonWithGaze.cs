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
public class AimCannonWithGaze : MonoBehaviour
{


    private GazeAware _gazeAware;
    public GameObject bullet_pf;
    public GameObject cannon;
    GameObject sphereL, sphereR;
    GazePoint gazePoint;
    Vector3 gazePoint3, gazePoint3_vp, gazePoint3_scr;

    GameObject bullet;
    int aim_interval;
    const int AIM_INT = 10;
    float start_time,current_time;


    Ray ray, ray2, ray3;
    RaycastHit hit;
    CannonStats cs;

    void Start()
    {
        _gazeAware = GetComponent<GazeAware>();
        aim_interval = AIM_INT;
        start_time = Time.time;
        current_time = Time.time;
        cs = GetComponent<CannonStats>();
    }

    // Update is called once per frame
    void Update ()
    {

        current_time = Time.time;

        gazePoint = EyeTracking.GetGazePoint();
        gazePoint3 = new Vector3(gazePoint.Viewport.x, gazePoint.Viewport.y, 0);
        gazePoint3_scr = new Vector3(gazePoint.Screen.x, gazePoint.Screen.y, 0);

        ray = new Ray(Camera.main.transform.position, Camera.main.ViewportToWorldPoint(gazePoint3));
        ray = new Ray(Camera.main.transform.position, Vector3.forward);

        //ray2 = Camera.main.ViewportPointToRay(gazePoint3);
        ray2 = new Ray(Camera.main.ViewportToWorldPoint(gazePoint3), Vector3.forward);

        ray3 = Camera.main.ViewportPointToRay(gazePoint3);

        /*
        Debug.Log("gazePoint = " + gazePoint);
        Debug.Log("gazePoint3 = " + gazePoint3);
        Debug.Log("(Camera.main.ViewportToWorldPoint(gazePoint3)= " + (Camera.main.ViewportToWorldPoint(gazePoint3)));
        Debug.Log("(Camera.main.ScreenToWorldPoint(gazePoint3_scr)= " + (Camera.main.ScreenToWorldPoint(gazePoint3_scr)));
        */



        /*
        if (Physics.Raycast(ray, out hit, 1000f))
        {
            //Debug.DrawRay(ray.origin, ray.direction * 1000f, Color.blue);
            //Debug.Log("ray1: hit = " + hit.collider.gameObject.name + " collision = " + hit.point);
            //transform.LookAt(new Vector3(hit.point.x, hit.point.y,0));
            //transform.LookAt(new Vector3(Camera.main.ViewportToWorldPoint(gazePoint3).x, Camera.main.ViewportToWorldPoint(gazePoint3).y, 0));
            //transform.LookAt(new Vector3(Camera.main.ScreenToWorldPoint(gazePoint3).x, Camera.main.ScreenToWorldPoint(gazePoint3).y, 0));
        }

        if (Physics.Raycast(ray2, out hit, 1000f))
        {
            //Debug.DrawRay(ray2.origin, ray2.direction * 1000f, Color.red);
            //Debug.Log("RAY2: hit = " + hit.collider.gameObject.name + " collision = " + hit.point);
            //transform.LookAt(new Vector3(hit.point.x, hit.point.y, 0));
        }
        */

        if (_gazeAware.HasGazeFocus)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                cs.PowerOn();

            }
        }
        else
        {
            if (Physics.Raycast(ray3, out hit, 1000f))
            {
                Debug.DrawRay(ray3.origin, ray3.direction * 500f, Color.yellow);
                //Debug.Log("ray3 : hit = " + hit.collider.gameObject.name + " collision = " + hit.point);
                transform.LookAt(new Vector3(hit.point.x, hit.point.y, 0));
            }
        }


        //Debug.DrawRay(ray2.origin, ray2.direction + new Vector3(0, 0, 25f));
        /*
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
        */
        
    /*

        if (current_time >= start_time + 10.0f)
        {
            aim_interval--;
            start_time = Time.time;
            Debug.Log("aim _interval = " + aim_interval);
        }
        */
    }
}
