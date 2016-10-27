using UnityEngine;
using System.Collections;

public class EnemyStats : MonoBehaviour {

    float lifetime;
    float drag;

    // lerp size as dropping?
    Rigidbody rb;
    // Use this for initialization
    void Start()
    {
        lifetime = 10.0f;
        rb = GetComponent<Rigidbody>();
        drag = Random.Range(1.0f, 8.0f);
        rb.drag += drag;
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
            //Destroy(gameObject);
        }
    }
}