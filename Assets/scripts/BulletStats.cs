using UnityEngine;
using System.Collections;

public class BulletStats : MonoBehaviour {

    public float lifetime;


    public enum gunColor { red, green, blue, yellow, none };
    public Material[] gunMats;

    public gunColor bc = gunColor.none; //bullet color

    void Start () {
    }


    // Use this for initialization
    public void Init()
    {
        GetComponent<MeshRenderer>().material = gunMats[(int)bc];
    }

    // Update is called once per frame
    void Update () {
        if (lifetime >= 0)
        {
            lifetime -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
