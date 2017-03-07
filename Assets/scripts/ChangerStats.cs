using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Tobii.EyeTracking;
public class ChangerStats : MonoBehaviour {



    public enum quadColor { red, green, blue, yellow, none };
    public Material[] quadMats;

    public quadColor qc = quadColor.none; //quad color

    // Use this for initialization
    void Start () {

        MeshRenderer mesh = GetComponent<MeshRenderer>();
        switch (qc)
        {
            case quadColor.red:
                mesh.material = quadMats[0];
                break;
            case quadColor.green:
                mesh.material = quadMats[1];
                break;
            case quadColor.blue:
                mesh.material = quadMats[2];
                break;
            case quadColor.yellow:
                mesh.material = quadMats[3];
                break;
            default:
                Debug.Log("error no quad color assigned");
                break;
        }
    }
	
	// Update is called once per frame
	void Update () {
    }
}
