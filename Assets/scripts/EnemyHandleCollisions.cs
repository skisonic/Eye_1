using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHandleCollisions : MonoBehaviour {

    // this script increments score right now, should be fixed 
    public GameObject enemy_death_pf, home;
	// Use this for initialization
	void Start () {
        home = GameObject.Find("Home");
	}

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Bullet")
        {
            BulletStats bs = coll.gameObject.GetComponent<BulletStats>();

            if (GetComponent<EnemyStats>().ec.ToString() == bs.bc.ToString())
            {
                GameObject death = Instantiate(enemy_death_pf, transform.position, Quaternion.identity) as GameObject;
                home.GetComponent<TakeDamage>().score++;
                Debug.Log("somebody died");
                Destroy(gameObject);
            }
        }
    }
}
