using UnityEngine;
using System.Collections;
using UnityEngine.UI;
//using System;

public class SpawnEnemies : MonoBehaviour {

    public GameObject enemy_pf, new_enemy_pf;
    float interval;
    int numEnemies,colEnemies;
    int count;
    int type;
    float drag, drag_minor, scale;
    public Text dragText, scaleText;

    public Sprite[] enemySprites;
    public GameObject[] enemies = new GameObject[3];
    bool allDead;
    

    // Use this for initialization
    void Start () {
        interval = 1.0f;
        allDead = true;
        CreateEnemies();
    }

    // Update is called once per frame
    void Update () {

        //Debug.Log("interval = " + interval);

        if (allDead) {
            if (interval >= 0)
            {
                interval -= Time.deltaTime;
            }
            else
            {
                StartCoroutine(newDropEnemies());
                interval = 1.5f;
            }
        }

        allDead = true;
        for (int i = 0; i < enemies.Length; i++)
        {
            //find one thats not dead to toggle false
            if (!(enemies[i].gameObject.GetComponent<EnemyStatsTopLevel>().isDead))
            {
                //if one isnt dead theytre aLL not dead
                allDead = false;
            }
        }

    }

    void CreateEnemies()
    {
        numEnemies = 3; //number of enemies that will drop
        colEnemies = Random.Range(0, 3); // enemy spawn color 
        scale = Random.Range(1.0f, 3.0f);
        //drag = Random.Range(1.0f, 3.0f) * (scale * 1.25f); //reduce drag inversely by proportion so bigger = slower
        //Debug.Log("SpawnEnemies:DropEnemies:: numEnemies:" + numEnemies + " colEnemies:" + colEnemies + " drag:" + drag + " scale:" + scale);
        for (int i = 0; i < numEnemies; i++)
        {
            //drop some enemies
            drag_minor = Random.Range(0.0f, 0.5f);
            type = Random.Range(0, 2);
            //GameObject enemy = Instantiate(new_enemy_pf, transform.position + new Vector3(i * 0.1f, -2.0f, 0), Quaternion.identity) as GameObject;
            enemies[i] = Instantiate(new_enemy_pf, transform.position + new Vector3(i * 0.1f, -2.0f, 0), Quaternion.identity) as GameObject;
            //enemy.GetComponent<Rigidbody>().isKinematic = true;
            enemies[i].GetComponent<Rigidbody>().isKinematic = false;
            enemies[i].GetComponent<Rigidbody>().useGravity = false;
            //enemy.GetComponentsInChildren<Rigidbody>()[1].isKinematic = true;
            enemies[i].GetComponent<EnemyStatsTopLevel>().ec = (EnemyStatsTopLevel.gunColor)colEnemies;
            //enemies[i].transform.localScale *= scale;
            enemies[i].GetComponent<EnemyStatsTopLevel>().isDead = false;
            count++;
        }

    }
    IEnumerator DropEnemies()
    {
        /*
        numEnemies = Random.Range(1, 3); //number of enemies that will drop
        colEnemies = Random.Range(0, 3); // enemy spawn color 
        scale = Random.Range(1.0f, 3.0f);
        drag = Random.Range(1.0f, 3.0f) * (scale * 1.25f); //reduce drag inversely by proportion so bigger = slower
        dragText.text = "drag =" + drag.ToString();
        scaleText.text = "scale = " + scale.ToString();
        float pauseTime;
        //Debug.Log("SpawnEnemies:DropEnemies:: numEnemies:" + numEnemies + " colEnemies:" + colEnemies + " drag:" + drag + " scale:" + scale);
        for (int i = 0; i < numEnemies; i++)
        {
            //drop some enemies
            drag_minor = Random.Range(0.0f, 0.5f);
            GameObject enemy = Instantiate(enemy_pf, transform.position + new Vector3(i * 0.1f, -2.0f, 0), Quaternion.identity) as GameObject;
            enemy.GetComponent<EnemyStats>().ec = (EnemyStats.gunColor)colEnemies;
            enemy.GetComponent<Rigidbody>().drag += drag + drag_minor;
            enemy.transform.localScale *= scale;
            count++;
            enemies[i] = enemy;
            allDead = false;
            pauseTime = Random.Range(1.0f, 5.0f);
            yield return new WaitForSeconds(.1f * pauseTime);
        }
        */
        yield return null;
    }

    IEnumerator newDropEnemies() //move enemy to top and drop em again
    {
        Debug.Log("newDropEnemies");
        numEnemies = Random.Range(1, 3); //number of enemies that will drop
        colEnemies = Random.Range(0, 3); // enemy spawn color 
        // drag = Random.Range(1.0f, 3.0f) * (scale * 1.25f); //reduce drag inversely by proportion so bigger = slower
        dragText.text = "drag =" + drag.ToString();
        scaleText.text = "scale = " + scale.ToString();
        float pauseTime;
        switch (type)
        {
            case 0:
                //small
                //faster
                //less lif5e
                break;
            case 1:
                break;
            case 2:
                break;
        }
        //Debug.Log("SpawnEnemies:DropEnemies:: numEnemies:" + numEnemies + " colEnemies:" + colEnemies + " drag:" + drag + " scale:" + scale);
        for (int i = 0; i < numEnemies; i++)
        {
            SpriteRenderer rend; 
            //drop some enemies
            drag_minor = Random.Range(0.0f, 0.5f);
            //GameObject enemy = Instantiate(enemy_pf, transform.position + new Vector3(i * 0.1f, -2.0f, 0), Quaternion.identity) as GameObject;
            enemies[i].transform.position = transform.position + new Vector3(i * 0.1f, -2.0f, 0);
            enemies[i].GetComponent<EnemyStatsTopLevel>().ec = (EnemyStatsTopLevel.gunColor)colEnemies;
            enemies[i].GetComponent<EnemyStatsTopLevel>().isDead = false;
            enemies[i].GetComponent<EnemyStatsTopLevel>().killed = false;
            enemies[i].GetComponent<EnemyStatsTopLevel>().addHP(1);
            enemies[i].GetComponent<Rigidbody>().isKinematic = false;
            enemies[i].GetComponent<Rigidbody>().useGravity = false;
            enemies[i].GetComponent<Collider>().enabled = true;
            rend = enemies[i].GetComponentInChildren<SpriteRenderer>();
            rend.enabled = true;
            rend.sprite = GetComponent<SpawnEnemies>().enemySprites[(int)colEnemies];

            //enemies[i].transform.localScale *= scale;
            count++;
            pauseTime = Random.Range(1.0f, 5.0f);
            yield return new WaitForSeconds(.1f * pauseTime);
        }
        allDead = false;
    }

    public void DropEnemiesDebug()
    {
        numEnemies = 1; //number of enemies that will drop
        colEnemies = Random.Range(0, 3); // enemy spawn color 
        scale = Random.Range(1.0f, 3.0f);
        drag = Random.Range(1.0f, 3.0f) * (scale * 1.25f); //reduce drag inversely by proportion so bigger = slower
        dragText.text = "drag =" + drag.ToString();
        scaleText.text = "scale = " + scale.ToString();
        float pauseTime;
        //Debug.Log("SpawnEnemies:DropEnemies:: numEnemies:" + numEnemies + " colEnemies:" + colEnemies + " drag:" + drag + " scale:" + scale);
        for (int i = 0; i <= numEnemies; i++)
        {
            //drop some enemies
            drag_minor = Random.Range(0.0f, 0.5f);
            GameObject enemy = Instantiate(enemy_pf, transform.position + new Vector3(i * 0.1f, -2.0f, 0), Quaternion.identity) as GameObject;
            enemy.GetComponent<EnemyStats>().ec = (EnemyStats.gunColor)colEnemies;
            enemy.GetComponent<Rigidbody>().drag += drag + drag_minor;
            enemy.transform.localScale *= scale;
            count++;
            pauseTime = Random.Range(1.0f, 5.0f);
        }
    }

}
