using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Move_Twd_Home : MonoBehaviour {


    public float MoveSpeed = 1.0f;

    public float frequency = 0.0f;  // Speed of sine movement
    public float magnitude = 0.0f;   // Size of sine movement
    //public GameObject home;
    GameObject home;
    private Vector3 axis;

    Transform target;
    private Vector3 pos;

    // Use this for initialization
    void Start()
    {
        home = GameObject.Find("Home");

        MoveSpeed = 1.0f;

        //scale ranges from .2 - .6
   
        MoveSpeed = gameObject.transform.localScale.x * Random.Range(3.0f, 4.0f);
        target = home.transform;
        //magnitude += MoveSpeed / Random.Range(1.5f, 2.8f);

        pos = transform.position;
        axis = transform.right;
    }

    // Update is called once per frame
    void Update()
    {
        target = home.transform;
        float step = MoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, step);
    }
}
