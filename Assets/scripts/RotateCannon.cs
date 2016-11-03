using UnityEngine; 
using System.Collections;


/// <summary>
///  Rotates Cannons automatically automatically. Requires rewriting, is NOT modular. Uses direct calls.
/// </summary>
public class RotateCannon : MonoBehaviour {


    public GameObject sphereL, sphereR;

    bool isShrinkingL, isShrinkingR;
    float shrinkRange = 100.0f;
    float shrinkFactorL = 100.0f;
    float shrinkFactorR = 100.0f;
    float shrinkRateL = 3.0f;
    float shrinkRateR = 1.5f;
    // Use this for initialization
    void Start () {
        isShrinkingL = true;
        isShrinkingR = true;
    }

    // Update is called once per frame
    void FixedUpdate () {

        if (isShrinkingL)
        {
            sphereL.transform.Rotate(Vector3.forward, -shrinkRateL);
            shrinkFactorL = shrinkFactorL - shrinkRateL;
        }
        else if (!isShrinkingL)
        {
            sphereL.transform.Rotate(Vector3.forward, shrinkRateL);
            shrinkFactorL = shrinkFactorL + shrinkRateL;
        }


        if (isShrinkingR)
        {
            sphereR.transform.Rotate(Vector3.forward, -shrinkRateR);
            shrinkFactorR = shrinkFactorR - shrinkRateR;
        }
        else if (!isShrinkingR)
        {
            sphereR.transform.Rotate(Vector3.forward, shrinkRateR);
            shrinkFactorR = shrinkFactorR + shrinkRateR;
        }

        if (shrinkFactorL >= shrinkRange)
        {
            isShrinkingL = true;
        }
        else if (shrinkFactorL <= 0)
        {
            isShrinkingL = false;
        }

        if (shrinkFactorR >= shrinkRange)
        {
            isShrinkingR = true;
        }
        else if (shrinkFactorR <= 0)
        {
            isShrinkingR = false;
        }
    }
}
    