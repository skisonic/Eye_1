using UnityEngine;
using System.Collections;

public class SpawnEnemies : MonoBehaviour {

    public GameObject enemy_pf;
    float interval;
    int numEnemies;
    int count; 

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
        numEnemies = Random.Range(0, 5);
        float pauseTime;

        for (int i = 0; i <= numEnemies; i++)
        {
            //drop some enemies
            //GameObject enemy = Instantiate(enemy_pf, transform.position + new Vector3(i * 0.1f, -2.0f, 0), Quaternion.identity) as GameObject;
            count++;
            pauseTime = Random.Range(1.0f, 5.0f);
            yield return new WaitForSeconds(.1f * pauseTime);
        }
    }
}
