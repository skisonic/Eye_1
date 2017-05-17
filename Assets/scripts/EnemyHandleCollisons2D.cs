using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandleCollisons2D : MonoBehaviour {

    public GameObject enemy_death_pf;
    GameObject home;

    // Use this for initialization
    void Start () {
        home = GameObject.Find("Home");

    }

    // Update is called once per frame
    void Update () {
		
	}


    void OnCollisionEnter2D(Collision2D coll)
    {
        //Debug.Log("2D Collision recorded");
        //HERES WHERE THE ENEMY DIES   
        if (coll.gameObject.CompareTag("Bullet"))
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
        if (coll.gameObject.CompareTag("Player_Gaze"))
        {
            //Follow_Gaze_Stats fs = coll.gameObject.GetComponent<Follow_Gaze_Stats>();
            Follow_Gaze_Stats fs = coll.gameObject.GetComponentInParent<Follow_Gaze_Stats>();

            if (GetComponentInParent<EnemyStats>().ec.ToString() == fs.gaze_color.ToString())
            {
                GameObject death = Instantiate(enemy_death_pf, transform.position, Quaternion.identity) as GameObject;
                home.GetComponent<TakeDamage>().score++;
                //Debug.Log("EnemHandColl: takedmg");
                Destroy(gameObject);
            }
        }

    }
}
