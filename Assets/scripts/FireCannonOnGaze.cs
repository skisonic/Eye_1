using UnityEngine;
using Tobii.EyeTracking;

[RequireComponent(typeof(GazeAware))]
public class FireCannonOnGaze : MonoBehaviour
{
    private GazeAware _gazeAware;
    public GameObject bullet_pf;
    GameObject cannon;
    GazePoint gazePoint;

    GameObject bullet;

    void Start()
    {
        _gazeAware = GetComponent<GazeAware>();
        cannon = GameObject.Find("CannonL");
    }

    void Update()
    {
        if (_gazeAware.HasGazeFocus || Input.GetKeyDown(KeyCode.Alpha0))
        {
            bullet = Instantiate(bullet_pf, transform.position, Quaternion.Euler(new Vector3(0,0,90f + transform.localRotation.z))) as GameObject;
            //bullet.transform.parent = gameObject.transform;
            bullet.GetComponent<Rigidbody>().AddForce(Vector3.right * 1000);
            //bullet.transform.parent = null;
            //gazePoint = EyeTracking.GetGazePoint();
            //Debug.Log("found it");
        }
    }
}