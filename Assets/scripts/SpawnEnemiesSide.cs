using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpawnEnemiesSide : MonoBehaviour
{

    public GameObject enemy_pf;
    float interval;
    int numEnemies, colEnemies;
    int count;
    float drag, drag_minor, scale;
    public Text dragText, scaleText;

    public Sprite[] enemySprites;
    GameObject[] enemies; //container for active enemies 
    int max_enemies = 3; //limit on live enemies

    // Use this for initialization
    void Start()
    {
        enemies = new GameObject[max_enemies];
        interval = 4.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (interval >= 0)
        {
            interval -= Time.deltaTime;
        }
        else
        {
            StartCoroutine(FireEnemies());
            interval = 7.5f;
        }
    }

    IEnumerator FireEnemies()
    {
        numEnemies = Random.Range(1, 1); //number of enemies that will drop
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
            enemy.GetComponent<EnemyStats>().addLifetime(15.0f);
            count++;
            enemies[i] = enemy.gameObject;
            //begin side specific code
            enemy.GetComponent<Enemy_Move_Twd_Home>().enabled = true;
            enemy.GetComponent<EnemyMovement_Follow>().enabled = false;
            enemy.GetComponent<Rigidbody>().useGravity = false;
            //end side specific code
            pauseTime = Random.Range(1.0f, 5.0f);
            yield return new WaitForSeconds(.1f * pauseTime);
        }
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
            //create an enemy from the side that moves towards base
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
