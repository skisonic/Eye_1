using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TakeDamage : MonoBehaviour {

    Text hp_label, hp_text;
    int hp;
    float shrinkDmg_wait;
    bool shrinkWait;

    GameObject gm;

	// Use this for initialization
	void Start () {
        hp_label = GameObject.Find("HP_Label_Text").GetComponent<Text>();
        hp_text = GameObject.Find("HP_Text").GetComponent<Text>();
        hp = 3;
        shrinkDmg_wait = 3.0f;
        shrinkWait = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (hp >= 0)
        {
            hp_text.text = hp.ToString();
        }
        else
        {
            hp_label.text = "Game Over";
            hp_text.text = "0";
        }
        if (transform.localScale.x < 0.25f && !shrinkWait)
        {
            hp--;
            StartCoroutine(ShrinkWaitPeriod());
        }
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Enemy_Cube")
        {
            hp--;
        }
    }

    IEnumerator ShrinkWaitPeriod()
    {
        if (shrinkDmg_wait >= 0)
        {
            shrinkDmg_wait -= Time.deltaTime / 60.0f;
            shrinkWait = true;
        }
        else
        {
            shrinkDmg_wait = 3.0f;
            shrinkWait = false;
            yield return null;
        }
    }
}
