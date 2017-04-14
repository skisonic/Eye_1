using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour {

    float lifetime;

    public enum gunColor { red, green, blue, yellow };
    public Material[] gunMats;
    public gunColor ec; //enemy color

    public GameObject home;
    const float LIFE_TIME = 15.0f;

    // lerp size as dropping?
    // Use this for initialization
    void Start()
    {
        Rigidbody rb;

        lifetime = LIFE_TIME;
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