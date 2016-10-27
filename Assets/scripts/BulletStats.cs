using UnityEngine;
using System.Collections;

public class BulletStats : MonoBehaviour {

    float lifetime;

	// Use this for initialization
	void Start () {
        lifetime = 5.0f;
    }

    // Update is called once per frame
    void Update () {
        if (lifetime >= 0 && transform.position.y <= 7.0f)
        {
            lifetime -= Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
