﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandleCollisions : MonoBehaviour
{

    // this script increments score right now, should be fixed 
    public GameObject enemy_death_pf;

    GameObject home;
    private EnemyStatsTopLevel stats;
    // Use this for initialization
    void Start()
    {
        home = GameObject.Find("Home");
        //stats = GetComponent<EnemyStats>();        
        stats = GetComponent<EnemyStatsTopLevel>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision coll)
    {
        //Debug.Log("3D collision detected");
        if (coll.gameObject.CompareTag("Bullet")) //  take damage from by bullet.
        {
            BulletStats bs = coll.gameObject.GetComponent<BulletStats>();

            if (stats.ec.ToString() == bs.bc.ToString())
            {
                GameObject death = Instantiate(enemy_death_pf, transform.position, Quaternion.identity) as GameObject;
                home.GetComponent<TakeDamage>().score++;
                Debug.Log("this shouldnt be EHCollisions");
                //Debug.Log("somebody died");
                Destroy(gameObject);
            }
        }
        if (coll.gameObject.CompareTag("Player_Gaze")) // take damage from gaze body
        {
            Debug.Log("Player Gaze");

            Follow_Gaze_Stats fs = coll.gameObject.GetComponent<Follow_Gaze_Stats>();

            if (stats.ec.ToString() == fs.gaze_color.ToString())
            {
                stats.TakeDamage();
                if(stats.returnLife() == 0)
                {
                    stats.killed = true;
                }
            }
        }
        if (coll.gameObject.CompareTag("Floor")) // hits the ground.
        {
            GetComponent<EnemyMovement_Follow>().enabled = false;
            Destroy(gameObject, 0.5f);
        }

    }

    void OnTriggerEnter(Collider coll)  //*(eworking) take damage from gaze trigger
    {
        Follow_Gaze_Stats fs = coll.gameObject.GetComponent<Follow_Gaze_Stats>();

        Debug.Log("3D collision trigger");

        Debug.Log("stats.ec.Tostring() " + stats.ec + " fs.gaze_color.ToString() " + fs.gaze_color);

        //if (GetComponent<EnemyStats>().ec.ToString() == fs.gaze_color.ToString())
        if (stats.ec.ToString() == fs.gaze_color.ToString())
        {
            stats.TakeDamage();
            if (stats.returnLife() == 0)
            {
                stats.killed = true;
            }
        }
    }
}