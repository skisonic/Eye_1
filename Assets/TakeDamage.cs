using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class TakeDamage : MonoBehaviour {

    Text hp_label, hp_text;
    public int hp, max_hp;
    float shrinkDmg_wait;
    bool shrinkWait;

    GameObject gm;
    const int MAX_HP = 1;

    int prototype_var; //0 = cube, 1 = cannons
	// Use this for initialization
	void Start () {

        prototype_var = 1;

        hp_label = GameObject.Find("HP_Label_Text").GetComponent<Text>();
        hp_text = GameObject.Find("HP_Text").GetComponent<Text>();
        hp = MAX_HP;
        max_hp = MAX_HP;
        hp_text.color = Color.green;
        shrinkDmg_wait = 3.0f;
        shrinkWait = false;

        if (prototype_var == 0)
        {
            gm = GameObject.Find("GM_CubeControl");
        }
        else if (prototype_var == 1)
        {
            gm = GameObject.Find("GM_Cannons");
        }
    }
	
	// Update is called once per frame
	void Update () {
        hp_text.text = hp.ToString();
        if (hp > 0)
        {

            if (hp < MAX_HP && hp >= MAX_HP / 1.7f)
            {
                hp_text.color = Color.yellow;
            }
            else if (hp < MAX_HP / 1.7f && hp >= MAX_HP / 3.4f)
            {
                hp_text.color = Color.yellow;
            }
            else if (hp < MAX_HP / 3.4f)
            {
                hp_text.color = Color.red;
            }
            if (transform.localScale.x < 0.25f && !shrinkWait)
            {
                hp--;
                StartCoroutine(ShrinkWaitPeriod());
            }
        }
        else
        {
            //endgame
            if (prototype_var == 0)
            {
                //gm.GetComponent<GM_CubeControl>().EndGame();
            }
            else if (prototype_var == 1)
            {
                gm.GetComponent<GM_Cannons>().EndGame();
            }
        }


    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == "Enemy_Cube")
        {
            hp--;
        }
    }

    public void ReceiveDamage(int dmg)
    {
        hp -= dmg;
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
