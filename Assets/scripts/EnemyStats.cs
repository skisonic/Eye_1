using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour {

    float lifetime;

    public enum gunColor { red, green, blue, yellow };
    public Material[] gunMats;
    public gunColor ec; //enemy color

    public GameObject home;

    // lerp size as dropping?
    Rigidbody rb;
    // Use this for initialization
    void Start()
    {
        lifetime = 10.0f;
        rb = GetComponent<Rigidbody>();
        GetComponent<MeshRenderer>().material = gunMats[(int)ec];
    }

    // Update is called once per frame
    void Update()
    {
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