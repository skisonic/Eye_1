using UnityEngine;
using Tobii.EyeTracking;

[RequireComponent(typeof(GazeAware))]
public class PingPongOnGaze : MonoBehaviour
{
    private GazeAware _gazeAware;

    bool isShrinking; 


    void Start()
    {
        _gazeAware = GetComponent<GazeAware>();
        isShrinking = false;
    }

    void Update()
    {
        if (_gazeAware.HasGazeFocus)
        {
            GazePoint gazePoint;

            gazePoint = EyeTracking.GetGazePoint();



            if(isShrinking)
            {
                transform.position = new Vector3(transform.position.x,
                                                  transform.position.y,
                                                  transform.position.z + 0.2f);
            }else if(!isShrinking)
            {
                transform.position = new Vector3(transform.position.x,
                                    transform.position.y,
                                    transform.position.z - 0.2f);
            }


            if (transform.position.z <= -8.0f)
            {
                isShrinking = true;
            }
            else  if (transform.position.z >= 3.0f)
            {
                isShrinking = false;
            }

            /*

                        if (isShrinking)
                        {
                            if (transform.position.z >= -7.0f && transform.position.z <= 7.0f)
                            {
                                transform.position = new Vector3(transform.position.x,
                                                       transform.position.y,
                                                       transform.position.z + 0.2f);
                            }
                            if(transform.position.z >= 7.0f)
                            {
                                isShrinking = false;
                            }
                        }

                        if (!isShrinking)
                        {
                            if (transform.position.z < 7.0f && transform.position.z >= -7.0f)
                            {
                                transform.position = new Vector3(transform.position.x,
                                                       transform.position.y,
                                                       transform.position.z - 0.2f);
                            }
                            if (transform.position.z <= 7.0f)
                            {
                                isShrinking = true;
                            }
                        }
                        */

            //Debug.Log("found it");
        }
    }
}