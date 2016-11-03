using UnityEngine;
using Tobii.EyeTracking;


/// <summary>
/// Fire gaze aware cannons. uses direct calls. needs rewrite. 
/// -ski 
/// </summary>
[RequireComponent(typeof(GazeAware))]
public class FireCannonOnGaze : MonoBehaviour
{
    private GazeAware _gazeAware;
    private GazeAware _gazeAwareL, _gazeAwareR;
    public GameObject bullet_pf;
    GameObject cannon;
    GameObject cannonL, cannonR;
    GameObject sphereL, sphereR;
    GazePoint gazePoint;

    GameObject bullet;
    
    void Start()
    {
        _gazeAware = GetComponent<GazeAware>();

        cannonL = GameObject.Find("CannonL");
        _gazeAwareL = cannonL.GetComponentInParent<GazeAware>();
        cannonR = GameObject.Find("CannonR");
        _gazeAwareR = cannonR.GetComponentInParent<GazeAware>();
    }

    void Update()
    {
        if ((_gazeAwareL.HasGazeFocus && gameObject.name == "SphereL" && Input.GetKey(KeyCode.Space)) || Input.GetKeyDown(KeyCode.Alpha1))
        {
            //Vector3 shotPath = Vector3.Distance(gameObject.transform.position, cannon.transform.position);
            bullet = Instantiate(bullet_pf, cannonL.transform.position, Quaternion.Euler(new Vector3(0, 0, 90f + transform.localRotation.z * 90.0f))) as GameObject;
            //bullet.transform.parent = gameObject.transform;
            bullet.GetComponent<Rigidbody>().AddForce(gameObject.transform.right * 1000);
            //bullet.transform.parent = null;
            //gazePoint = EyeTracking.GetGazePoint();
            //Debug.Log("found it");
        }
        else if ((_gazeAwareR.HasGazeFocus && gameObject.name == "SphereR" && Input.GetKey(KeyCode.Space)) || Input.GetKeyDown(KeyCode.Alpha0))
        {
            //Vector3 shotPath = Vector3.Distance(gameObject.transform.position, cannon.transform.position);
            bullet = Instantiate(bullet_pf, cannonR.transform.position, Quaternion.Euler(new Vector3(0,0,90.0f + transform.localRotation.z * 90.0f))) as GameObject;
            //bullet.transform.parent = gameObject.transform;
            bullet.GetComponent<Rigidbody>().AddForce(cannonR.transform.right * -1000);
            //bullet.transform.parent = null;
            //gazePoint = EyeTracking.GetGazePoint();
            //Debug.Log("found it");
       }
 
   }
}