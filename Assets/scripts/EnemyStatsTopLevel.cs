using UnityEngine;
using System.Collections;

public class EnemyStatsTopLevel : MonoBehaviour
{

    float lifetime;

    public enum gunColor { red, green, blue, yellow };
    public Material[] gunMats;
    public Material[] spriteMats;
    public gunColor ec; //enemy color

    const float LIFE_TIME = 15.0f;
    int hp = 1;
    Color start_color;

    public GameObject enemy_death_pf;
    public GameObject home;
    public GameObject spawner;
    public bool isDead;
    public bool killed;

    // lerp size as dropping?
    // Use this for initialization
    void Start()
    {
        //Rigidbody rb;
        SpriteRenderer srend;
        lifetime = LIFE_TIME;
        //rb = GetComponent<Rigidbody>();
        GetComponent<MeshRenderer>().material = gunMats[(int)ec];
        gameObject.name += "_" + ec.ToString();
        hp *= (int)GetComponent<Rigidbody>().drag / 2;
        if (hp <= 0)
            hp = 1;
        start_color = GetComponent<Renderer>().material.color;


        if (gameObject.name.Contains("3D_coll"))
        {
            srend = transform.parent.GetComponentInChildren<SpriteRenderer>();
        }
        else
        {
            srend = GetComponentInChildren<SpriteRenderer>();
        }
        srend.sprite = spawner.GetComponent<SpawnEnemies>().enemySprites[(int)ec];
        //Debug.Log("drag = " + GetComponent<Rigidbody>().drag + "hp = " + hp);

        //propogate some of this shit to children.
        Rigidbody[] rbs = GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody rb in rbs)
        {
            rb.drag = GetComponent<Rigidbody>().drag;
        }

        myInit();
    }

    public void myInit()
    {
        SpriteRenderer srend;
        srend = GetComponentInChildren<SpriteRenderer>();

        isDead = false;
        killed = false;
        srend.sprite = spawner.GetComponent<SpawnEnemies>().enemySprites[(int)ec];
        //based off type:
        //hp
        //size
        //speed
        //
        //  enemies[i].GetComponent<Rigidbody>().isKinematic = false;
        //enemies[i].GetComponent<Rigidbody>().useGravity = false;
        //enemies[i].GetComponent<Collider>().enabled = true;
        //sprite rendcolor
    }

    // Update is called once per frame
    void Update()
    {
        lifetime -= Time.deltaTime;

        if (hp <= 0 && !(isDead))
        {
            Die();
        }

        //disabling lifetime for now not necessary.
        /*
        if (lifetime <= 0) 
        {
            Die();
            //Destroy(gameObject);
        }*/
    }

    public void addLifetime(float addLife)
    {
        lifetime += addLife;
    }

    public int returnLife()
    {
        return hp;
    }

    public void addHP(int addHP)
    {
        hp += addHP;
    }

    public void TakeDamage()
    {
        hp--;
        Debug.Log("takedamge");
        StopCoroutine(FlashWhite());
        StartCoroutine(FlashWhite());
        //Debug.Log("drag = " + GetComponent<Rigidbody>().drag + "hp = " + hp);
    }

    public void Die()
    {
        isDead = true;
        SpriteRenderer rend = GetComponentInChildren<SpriteRenderer>();
        Collider coll = GetComponent<Collider>();
        home.GetComponent<Home_Damage_Follow>().score++;
        Debug.Log("Score incremented - EnemyStatsTopLevel");
        //Destroy(gameObject);
        //gameObject.SetActive(false);
        rend.enabled = false;
        coll.enabled = false;
        if (killed)
        {
            GameObject death = Instantiate(enemy_death_pf, transform.position, Quaternion.identity) as GameObject;
        }
    }


    IEnumerator FlashWhite()
    {
        Renderer rend;
        SpriteRenderer srend;
        rend = GetComponent<Renderer>();
        srend = GetComponentInChildren<SpriteRenderer>();
        if (rend.enabled)
        {
            rend.material.color = Color.white;
            yield return new WaitForSeconds(0.02f);
            rend.material.color = start_color;
            yield return new WaitForSeconds(0.01f);
            rend.material.color = Color.white;
            yield return new WaitForSeconds(0.02f);
            rend.material.color = start_color;
            yield return null;
        }
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