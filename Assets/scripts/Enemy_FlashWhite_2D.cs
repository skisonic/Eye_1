using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_FlashWhite_2D : MonoBehaviour {

    Material[] spriteMats;


    // Use this for initialization
    void Start () {
        spriteMats = transform.parent.GetComponentInChildren<EnemyStats>().spriteMats;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator FlashWhite()
    {

        SpriteRenderer srend;
        srend = GetComponent<SpriteRenderer>();

        if (srend.enabled)
        {
            srend.material = spriteMats[1];
            yield return new WaitForSeconds(0.02f);
            srend.material = spriteMats[0];
            yield return new WaitForSeconds(0.01f);
            srend.material = spriteMats[1];
            yield return new WaitForSeconds(0.02f);
            srend.material = spriteMats[0];
            yield return null;
        }
    }
}
