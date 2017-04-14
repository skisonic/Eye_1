using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandleCollisons2D : MonoBehaviour {

    public GameObject enemy_death_pf, home;

    // Use this for initialization
    void Start () {
        home = GameObject.Find("Home");

    }

    // Update is called once per frame
    void Update () {
		
	}


    void OnCollisionEnter2D(Collision coll)
    {
        Debug.Log("2D Collision recorded");
        //HERES WHERE THE ENEMY DIES   
        if (coll.gameObject.tag == "Bullet")
        {
            BulletStats bs = coll.gameObject.GetComponent<BulletStats>();

            if (GetComponentInParent<EnemyStats>().ec.ToString() == bs.bc.ToString())
            {
                GameObject death = Instantiate(enemy_death_pf, transform.position, Quaternion.identity) as GameObject;
                home.GetComponent<TakeDamage>().score++;
                //Debug.Log("somebody died");
                Destroy(gameObject);
            }
        }
        if (coll.gameObject.tag == "Player_Gaze")
        {
            Follow_Gaze_Stats fs = coll.gameObject.GetComponent<Follow_Gaze_Stats>();

            if (GetComponentInParent<EnemyStats>().ec.ToString() == fs.gaze_color.ToString())
            {
                GameObject death = Instantiate(enemy_death_pf, transform.position, Quaternion.identity) as GameObject;
                home.GetComponent<TakeDamage>().score++;
                //Debug.Log("somebody died");
                Destroy(gameObject);
            }
        }

    }
}
