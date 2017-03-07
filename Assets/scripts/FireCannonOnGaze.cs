using UnityEngine;
using Tobii.EyeTracking;


/// <summary>
/// Fire gaze aware cannons with keyboard inputs        . uses direct calls. needs rewrite. 
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

        if ((Input.GetKeyDown(KeyCode.Alpha1)))
        {
            //Vector3 shotPath = Vector3.Distance(gameObject.transform.position, cannon.transform.position);
            //bullet = Instantiate(bullet_pf, cannonL.transform.position, Quaternion.Euler(new Vector3(0, 0, 90f + transform.localRotation.z * 90.0f))) as GameObject;
            bullet = Instantiate(bullet_pf, transform.position , Quaternion.identity) as GameObject;
            BulletStats bs = bullet.GetComponent<BulletStats>();
            switch (GetComponent<CannonStats>().gc)
            {
                case CannonStats.gunColor.red:
                    bs.bc = BulletStats.gunColor.red;
                    break;
                case CannonStats.gunColor.blue:
                    bs.bc = BulletStats.gunColor.blue;
                    break;
                case CannonStats.gunColor.green:
                    bs.bc = BulletStats.gunColor.green;
                    break;
                case CannonStats.gunColor.yellow:
                    bs.bc = BulletStats.gunColor.yellow;
                    break;
                default:
                    Debug.Log("error no gun color assigned");
                    break;
            }
            bs.Init(); 
            //Debug.Log("bullet position = " + bullet.gameObject.transform.position);
            //bullet.transform.parent = gameObject.transform;
            bullet.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * 10f,ForceMode.Impulse);
            //bullet.transform.parent = null;
            //gazePoint = EyeTracking.GetGazePoint();
            //Debug.Log("found it");
        }

        /*
        if ((_gazeAwareL.HasGazeFocus && gameObject.name == "SphereL" && Input.GetKey(KeyCode.Space)) || Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha8))
        {
            //Vector3 shotPath = Vector3.Distance(gameObject.transform.position, cannon.transform.position);
            //bullet = Instantiate(bullet_pf, cannonL.transform.position, Quaternion.Euler(new Vector3(0, 0, 90f + transform.localRotation.z * 90.0f))) as GameObject;
            bullet = Instantiate(bullet_pf, transform.position, Quaternion.identity) as GameObject;
            //bullet.transform.parent = gameObject.transform;
            bullet.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * 750f);
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
       */
 
   }
}