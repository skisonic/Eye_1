using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpawnEnemies : MonoBehaviour {

    public GameObject enemy_pf;
    float interval;
    int numEnemies,colEnemies;
    int count;
    float drag, drag_minor, scale;
    public Text dragText, scaleText;

    // Use this for initialization
    void Start () {
        interval = 3.0f;
	}
	
	// Update is called once per frame
	void Update () {
        if (interval >= 0)
        {
            interval -= Time.deltaTime;
        }
        else
        {
            StartCoroutine(DropEnemies());
            interval = 5.5f;
        }
    }

    IEnumerator DropEnemies()
    {
        numEnemies = Random.Range(1, 3); //number of enemies that will drop
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
            yield return new WaitForSeconds(.1f * pauseTime);
        }
    }
}
