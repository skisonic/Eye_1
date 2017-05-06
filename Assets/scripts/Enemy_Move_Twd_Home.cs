using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Move_Twd_Home : MonoBehaviour {


    public float MoveSpeed = 1.0f;

    public float frequency = 0.0f;  // Speed of sine movement
    public float magnitude = 0.0f;   // Size of sine movement
    public GameObject home;
    private Vector3 axis;

    Transform target;
    private Vector3 pos;

    // Use this for initialization
    void Start()
    {

        MoveSpeed = 1.0f;
        frequency = 10.0f;
        magnitude = 1.0f;   // Size of sine movement

        MoveSpeed = MoveSpeed / gameObject.transform.localScale.x * Random.Range(1.0f, 1.8f);
        frequency = MoveSpeed * 2.0f;
        target = home.transform;
        //magnitude += MoveSpeed / Random.Range(1.5f, 2.8f);

        pos = transform.position;
        axis = transform.right;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 xPos = pos + axis * Mathf.Sin(Time.time * frequency) * magnitude;
        Vector3 finalPos = new Vector3(xPos.x, transform.position.y, transform.position.z);

        float step = MoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);

        if (Input.GetKey(KeyCode.UpArrow)) //debug to raise movement magnitude 
        {
            magnitude += 0.5f;
        }
    }
}
