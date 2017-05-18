using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class Home_Damage_Follow : MonoBehaviour
{

    Text hp_text;
    public int hp, max_hp;
    float shrinkDmg_wait;
    bool shrinkWait;

    GameObject gm;
    const int MAX_HP = 10;

    public int score = 0;
    Color start_color;
    // Use this for initialization
    void Start()
    {
        Text hp_label;

        hp_label = GameObject.Find("HP_Label_Text").GetComponent<Text>();
        hp_text = GameObject.Find("HP_Text").GetComponent<Text>();
        hp = MAX_HP;
        max_hp = MAX_HP;
        hp_text.color = Color.green;
        shrinkDmg_wait = 3.0f;
        shrinkWait = false;
        gm = GameObject.Find("GM_Follow");
        start_color = GetComponent<Renderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
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
            gm.GetComponent<GM_Follow>().EndGame();
        }


    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.CompareTag("Enemy_Cube"))
        {
            ReceiveDamage(1);
            //Destroy(coll.gameObject.transform.parent.gameObject);
            //coll.gameObject.transform.parent.gameObject.GetComponent<EnemyStatsTopLevel>().Die();
            coll.gameObject.GetComponent<EnemyStatsTopLevel>().Die();
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.CompareTag("Enemy_Cube"))
        {
            ReceiveDamage(1);
            //Destroy(coll.gameObject.transform.parent.gameObject);
            //coll.gameObject.transform.parent.gameObject.GetComponent<EnemyStatsTopLevel>().Die();
            coll.gameObject.GetComponent<EnemyStatsTopLevel>().Die();
        }
    }

    public void ReceiveDamage(int dmg)
    {
        hp -= dmg;
        StopCoroutine(FlashWhite());
        StartCoroutine(FlashWhite());
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

    IEnumerator FlashWhite()
    {
        Renderer rend;
        rend = GetComponent<Renderer>();
        rend.material.color = Color.white;
        yield return new WaitForSeconds(0.05f);
        rend.material.color = start_color;
        yield return new WaitForSeconds(0.01f);
        rend.material.color = Color.white;
        yield return new WaitForSeconds(0.05f);
        rend.material.color = start_color;
        yield return null;
    }

}
