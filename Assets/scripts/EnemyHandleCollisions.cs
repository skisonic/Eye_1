using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandleCollisions : MonoBehaviour
{

    // this script increments score right now, should be fixed 
    public GameObject enemy_death_pf;
    GameObject home;
    private EnemyStats stats;
    // Use this for initialization
    void Start()
    {
        home = GameObject.Find("Home");
        stats = GetComponent<EnemyStats>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision coll)
    {
        //Debug.Log("3D collision detected");
        if (coll.gameObject.tag == "Bullet") //  take damage from by bullet.
        {
            BulletStats bs = coll.gameObject.GetComponent<BulletStats>();

            if (stats.ec.ToString() == bs.bc.ToString())
            {
                GameObject death = Instantiate(enemy_death_pf, transform.position, Quaternion.identity) as GameObject;
                home.GetComponent<TakeDamage>().score++;
                //Debug.Log("somebody died");
                Destroy(gameObject);
            }
        }
        if (coll.gameObject.tag == "Player_Gaze") // take damage from gaze body
        {
            Debug.Log("Player Gaze");

            Follow_Gaze_Stats fs = coll.gameObject.GetComponent<Follow_Gaze_Stats>();

            if (stats.ec.ToString() == fs.gaze_color.ToString())
            {
                stats.TakeDamage();
                /*
                GameObject death = Instantiate(enemy_death_pf, transform.position, Quaternion.identity) as GameObject;
                home.GetComponent<TakeDamage>().score++;
                Destroy(gameObject);
                */
            }
        }
        if (coll.gameObject.tag == "Floor") // hits the ground.
        {
            GetComponent<EnemyMovement_Follow>().enabled = false;
            Destroy(gameObject, 0.5f);
        }

    }

    void OnTriggerEnter(Collider coll)  //*(eworking) take damage from gaze trigger
    {
        Follow_Gaze_Stats fs = coll.gameObject.GetComponent<Follow_Gaze_Stats>();

        //Debug.Log("3D collision trigger");

        //Debug.Log("stats.ec.Tostring() " + stats.ec + " fs.gaze_color.ToString() " + fs.gaze_color);

        if (GetComponent<EnemyStats>().ec.ToString() == fs.gaze_color.ToString())
        {
            stats.TakeDamage();
            /*
            GameObject death = Instantiate(enemy_death_pf, transform.position, Quaternion.identity) as GameObject;
            home.GetComponent<TakeDamage>().score++;
            Debug.Log("somebody died");
            Destroy(gameObject);
            */
        }
    }
}